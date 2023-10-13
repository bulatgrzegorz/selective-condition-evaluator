﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Diagnostics;
using SelectiveConditionEvaluator.BackEnd.Components.Logging;
using SelectiveConditionEvaluator.Shared;

namespace SelectiveConditionEvaluator.Evaluation.Conditionals
{
    /// <summary>
    /// Performs logical OR on children
    /// Does not update conditioned properties table
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal sealed class OrExpressionNode : OperatorExpressionNode
    {
        /// <summary>
        /// Evaluate as boolean
        /// </summary>
        internal override bool BoolEvaluate(ConditionEvaluator.IConditionEvaluationState state, LoggingContext loggingContext = null)
        {
            if (!LeftChild.TryBoolEvaluate(state, out bool leftBool, loggingContext))
            {
                ProjectErrorUtilities.ThrowInvalidProject(
                    state.ElementLocation,
                    "ExpressionDoesNotEvaluateToBoolean",
                    LeftChild.GetUnexpandedValue(state),
                    LeftChild.GetExpandedValue(state, loggingContext),
                    state.Condition);
            }

            if (leftBool)
            {
                // Short circuit
                return true;
            }
            else
            {
                if (!RightChild.TryBoolEvaluate(state, out bool rightBool, loggingContext))
                {
                    ProjectErrorUtilities.ThrowInvalidProject(
                        state.ElementLocation,
                        "ExpressionDoesNotEvaluateToBoolean",
                        RightChild.GetUnexpandedValue(state),
                        RightChild.GetExpandedValue(state),
                        state.Condition);
                }

                return rightBool;
            }
        }

        internal override string DebuggerDisplay => $"(or {LeftChild.DebuggerDisplay} {RightChild.DebuggerDisplay})";

        #region REMOVE_COMPAT_WARNING
        private bool _possibleOrCollision = true;
        internal override bool PossibleOrCollision
        {
            set { _possibleOrCollision = value; }
            get { return _possibleOrCollision; }
        }
        #endregion
    }
}
