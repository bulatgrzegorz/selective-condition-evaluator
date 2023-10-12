// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Build.Collections;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Shared.FileSystem;

namespace SelectiveConditionEvaluator;

public class SelectiveParser
{
    private static readonly GenericExpressionNode s_trueNode = new StringExpressionNode("true", false);
    private readonly string[] _propertyPrefixQuery;
    private readonly PropertyDictionary<ProjectPropertyInstance> _projectPropertyInstances = new();

    public SelectiveParser()
    {
        _propertyPrefixQuery = Array.Empty<string>();
    }

    public SelectiveParser(string propertyName, string propertyValue)
    {
        _propertyPrefixQuery = new[] { $"$({propertyName}" };
        _projectPropertyInstances.Set(ProjectPropertyInstance.Create(propertyName, propertyValue));
    }

    public SelectiveParser(IDictionary<string, string> properties)
    {
        _propertyPrefixQuery = properties.Select(x => $"$({x.Key}").ToArray();
        foreach (var (name, value) in properties)
        {
            _projectPropertyInstances.Set(ProjectPropertyInstance.Create(name, value));
        }
    }

    public bool EvaluateSelective(string expression)
    {
        return EvaluateSelectiveInternal(expression, _projectPropertyInstances);
    }

    private bool EvaluateSelectiveInternal(string expression, PropertyDictionary<ProjectPropertyInstance> projectPropertyInstances)
    {
        IFileSystem? fileSystems = FileSystems.Default;
        string evaluationDirectory = Directory.GetCurrentDirectory();
        MockElementLocation elementLocation = MockElementLocation.Instance;
        ProjectRootElementCacheBase? projectRootElementCache = default;

        var expander = new Expander<ProjectPropertyInstance, ProjectItemInstance>(projectPropertyInstances, fileSystems);
        var parserOptions = ParserOptions.AllowAll;

        var conditionParser = new Parser();
        var parsedExpression = conditionParser.Parse(expression, parserOptions, elementLocation);

        var state = new ConditionEvaluator.ConditionEvaluationState<ProjectPropertyInstance, ProjectItemInstance>(
            expression,
            expander,
            ExpanderOptions.ExpandProperties,
            default,
            evaluationDirectory,
            elementLocation,
            fileSystems,
            projectRootElementCache);

        SimplifyNode(state, parsedExpression, out var simplifiedNode);

        return simplifiedNode.Evaluate(state);
    }

    private bool SimplifyNode(ConditionEvaluator.ConditionEvaluationState<ProjectPropertyInstance, ProjectItemInstance> state, GenericExpressionNode node, out GenericExpressionNode simplifiedNode)
    {
        if (node is FunctionCallExpressionNode functionCallNode)
        {
            if (functionCallNode._arguments.Count != 1)
            {
                throw new ArgumentException($"{nameof(FunctionCallExpressionNode)}: ({functionCallNode}) must have one argument.");
            }

            var argument = functionCallNode._arguments[0];
            if (SimplifyNode(state, argument, out _))
            {
                simplifiedNode = functionCallNode;
                return true;
            }

            // From all implemented functions, there are no that would accept property as parameter. We can just replace it
            simplifiedNode = s_trueNode;
            return false;
        }

        if (node is AndExpressionNode andNode)
        {
            simplifiedNode = new AndExpressionNode()
            {
                LeftChild = SimplifyNode(state, andNode.LeftChild, out var left) ? left : s_trueNode,
                RightChild = SimplifyNode(state, andNode.RightChild, out var right) ? right : s_trueNode,
                PossibleAndCollision = andNode.PossibleAndCollision,
                PossibleOrCollision = andNode.PossibleOrCollision
            };

            return true;
        }

        if (node is OrExpressionNode orNode)
        {
            simplifiedNode = new OrExpressionNode()
            {
                LeftChild = SimplifyNode(state, orNode.LeftChild, out var left) ? left : s_trueNode,
                RightChild = SimplifyNode(state, orNode.RightChild, out var right) ? right : s_trueNode,
                PossibleAndCollision = orNode.PossibleAndCollision,
                PossibleOrCollision = orNode.PossibleOrCollision
            };

            return true;
        }

        if (node is StringExpressionNode stringNode)
        {
            string? unexpandedValue = stringNode.GetUnexpandedValue(state);
            if (unexpandedValue is null)
            {
                simplifiedNode = s_trueNode;
                return false;
            }

            bool isExpansionProperty = unexpandedValue.StartsWith("$") || unexpandedValue.StartsWith("@") || unexpandedValue.StartsWith("%");
            if (!isExpansionProperty)
            {
                simplifiedNode = stringNode;
                return false;
            }

            if (!_propertyPrefixQuery.Any(x => unexpandedValue.StartsWith(x)))
            {
                simplifiedNode = s_trueNode;
                return false;
            }

            simplifiedNode = stringNode;
            return true;
        }

        if (node is OperatorExpressionNode operatorExpressionNode)
        {
            if (AnySelective(state, operatorExpressionNode))
            {
                simplifiedNode = operatorExpressionNode;
                return true;
            }

            simplifiedNode = s_trueNode;
            return false;
        }

        simplifiedNode = null!;
        return false;
    }

    private bool AnySelective(ConditionEvaluator.ConditionEvaluationState<ProjectPropertyInstance, ProjectItemInstance> state, OperatorExpressionNode node)
    {
        return SimplifyNode(state, node.LeftChild, out _) || SimplifyNode(state, node.LeftChild, out _);
    }
}
