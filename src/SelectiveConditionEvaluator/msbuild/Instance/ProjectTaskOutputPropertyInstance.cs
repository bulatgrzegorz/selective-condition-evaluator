// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild.Instance
{
    /// <summary>
    /// Represents an output property element beneath a task element
    /// </summary>
    /// <remarks>
    /// Immutable.
    /// </remarks>
    internal sealed class ProjectTaskOutputPropertyInstance : ProjectTaskInstanceChild, ITranslatable
    {
        /// <summary>
        /// Name of the property to put the output in
        /// </summary>
        private string _propertyName;

        /// <summary>
        /// Property on the task class to retrieve the output from
        /// </summary>
        private string _taskParameter;

        /// <summary>
        /// Condition on the output element
        /// </summary>
        private string _condition;

        /// <summary>
        /// Location of the original element
        /// </summary>
        private ElementLocation.ElementLocation _location;

        /// <summary>
        /// Location of the original property name attribute
        /// </summary>
        private ElementLocation.ElementLocation _propertyNameLocation;

        /// <summary>
        /// Location of the original task parameter attribute
        /// </summary>
        private ElementLocation.ElementLocation _taskParameterLocation;

        /// <summary>
        /// Location of the original condition attribute
        /// </summary>
        private ElementLocation.ElementLocation _conditionLocation;

        /// <summary>
        /// Constructor called by Evaluator
        /// </summary>
        internal ProjectTaskOutputPropertyInstance(string propertyName, string taskParameter, string condition, ElementLocation.ElementLocation location, ElementLocation.ElementLocation propertyNameLocation, ElementLocation.ElementLocation taskParameterLocation, ElementLocation.ElementLocation conditionLocation)
        {
            ErrorUtilities.VerifyThrowInternalLength(propertyName, nameof(propertyName));
            ErrorUtilities.VerifyThrowInternalLength(taskParameter, nameof(taskParameter));
            ErrorUtilities.VerifyThrowInternalNull(location, nameof(location));
            ErrorUtilities.VerifyThrowInternalNull(propertyNameLocation, nameof(propertyNameLocation));
            ErrorUtilities.VerifyThrowInternalNull(taskParameterLocation, nameof(taskParameterLocation));

            _propertyName = propertyName;
            _taskParameter = taskParameter;
            _condition = condition;
            _location = location;
            _propertyNameLocation = propertyNameLocation;
            _taskParameterLocation = taskParameterLocation;
            _conditionLocation = conditionLocation;
        }

        private ProjectTaskOutputPropertyInstance()
        {
        }

        /// <summary>
        /// Name of the property to put the output in
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
        }

        /// <summary>
        /// Property on the task class to retrieve the output from
        /// </summary>
        public string TaskParameter
        {
            get { return _taskParameter; }
        }

        /// <summary>
        /// Condition on the output element.
        /// If there is no condition, returns empty string.
        /// </summary>
        public override string Condition
        {
            get { return _condition; }
        }

        /// <summary>
        /// Location of the original PropertyName attribute
        /// </summary>
        public ElementLocation.ElementLocation PropertyNameLocation
        {
            get { return _propertyNameLocation; }
        }

        /// <summary>
        /// Location of the original element
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
        /// Location of the TaskParameter attribute
        /// </summary>
        public override ElementLocation.ElementLocation TaskParameterLocation
        {
            get { return _taskParameterLocation; }
        }

        void ITranslatable.Translate(ITranslator translator)
        {
            if (translator.Mode == TranslationDirection.WriteToStream)
            {
                var typeName = this.GetType().FullName;
                translator.Translate(ref typeName);
            }

            translator.Translate(ref _propertyName);
            translator.Translate(ref _taskParameter);
            translator.Translate(ref _condition);
            translator.Translate(ref _location, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _conditionLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _propertyNameLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _taskParameterLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
        }
    }
}
