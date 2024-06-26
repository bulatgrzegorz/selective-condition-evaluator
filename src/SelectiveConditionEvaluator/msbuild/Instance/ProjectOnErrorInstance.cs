﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Diagnostics;
using SelectiveConditionEvaluator.msbuild.BackEnd.Components.Communications;
using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild.Instance
{
    /// <summary>
    /// Wraps an onerror element
    /// </summary>
    /// <remarks>
    /// This is an immutable class
    /// </remarks>
    [DebuggerDisplay("ExecuteTargets={_executeTargets} Condition={_condition}")]
    internal sealed class ProjectOnErrorInstance : ProjectTargetInstanceChild, ITranslatable
    {
        /// <summary>
        /// Unevaluated executetargets value.
        /// </summary>
        private string _executeTargets;

        /// <summary>
        /// Condition on the element.
        /// </summary>
        private string _condition;

        /// <summary>
        /// Location of this element
        /// </summary>
        private ElementLocation.ElementLocation _location;

        /// <summary>
        /// Location of the condition, if any
        /// </summary>
        private ElementLocation.ElementLocation _conditionLocation;

        /// <summary>
        /// Location of the executeTargets attribute
        /// </summary>
        private ElementLocation.ElementLocation _executeTargetsLocation;

        /// <summary>
        /// Constructor called by Evaluator.
        /// All parameters are in the unevaluated state.
        /// </summary>
        internal ProjectOnErrorInstance(
            string executeTargets,
            string condition,
            ElementLocation.ElementLocation location,
            ElementLocation.ElementLocation executeTargetsLocation,
            ElementLocation.ElementLocation conditionLocation)
        {
            ErrorUtilities.VerifyThrowInternalLength(executeTargets, nameof(executeTargets));
            ErrorUtilities.VerifyThrowInternalNull(condition, nameof(condition));
            ErrorUtilities.VerifyThrowInternalNull(location, nameof(location));

            _executeTargets = executeTargets;
            _condition = condition;
            _location = location;
            _executeTargetsLocation = executeTargetsLocation;
            _conditionLocation = conditionLocation;
        }

        private ProjectOnErrorInstance()
        {
        }

        /// <summary>
        /// Unevaluated condition.
        /// May be empty string.
        /// </summary>
        public override string Condition
        {
            get { return _condition; }
        }

        /// <summary>
        /// Unevaluated ExecuteTargets value.
        /// May be empty string.
        /// </summary>
        public string ExecuteTargets
        {
            get { return _executeTargets; }
        }

        /// <summary>
        /// Location of the element
        /// </summary>
        public override ElementLocation.ElementLocation Location
        {
            get { return _location; }
        }

        /// <summary>
        /// Location of the condition, if any
        /// </summary>
        public override ElementLocation.ElementLocation ConditionLocation
        {
            get { return _conditionLocation; }
        }

        /// <summary>
        /// Location of the execute targets attribute, if any
        /// </summary>
        public ElementLocation.ElementLocation ExecuteTargetsLocation
        {
            get { return _executeTargetsLocation; }
        }

        void ITranslatable.Translate(ITranslator translator)
        {
            if (translator.Mode == TranslationDirection.WriteToStream)
            {
                var typeName = this.GetType().FullName;
                translator.Translate(ref typeName);
            }

            translator.Translate(ref _executeTargets);
            translator.Translate(ref _condition);
            translator.Translate(ref _location, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _conditionLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _executeTargetsLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
        }

        internal static new ProjectOnErrorInstance FactoryForDeserialization(ITranslator translator)
        {
            return translator.FactoryForDeserializingTypeWithName<ProjectOnErrorInstance>();
        }
    }
}
