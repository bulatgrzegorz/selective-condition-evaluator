// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild.Instance
{
    /// <summary>
    /// Wraps an output item element under a task element
    /// </summary>
    /// <remarks>
    /// Immutable.
    /// </remarks>
    internal sealed class ProjectTaskOutputItemInstance : ProjectTaskInstanceChild, ITranslatable
    {
        /// <summary>
        /// Name of the property to put the output in
        /// </summary>
        private string _itemType;

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
        /// Location of the original item type attribute
        /// </summary>
        private ElementLocation.ElementLocation _itemTypeLocation;

        /// <summary>
        /// Location of the original task parameter attribute
        /// </summary>
        private ElementLocation.ElementLocation _taskParameterLocation;

        /// <summary>
        /// Location of the original condition attribute
        /// </summary>
        private ElementLocation.ElementLocation _conditionLocation;

        /// <summary>
        /// Constructor called by evaluator
        /// </summary>
        internal ProjectTaskOutputItemInstance(string itemType, string taskParameter, string condition, ElementLocation.ElementLocation location, ElementLocation.ElementLocation itemTypeLocation, ElementLocation.ElementLocation taskParameterLocation, ElementLocation.ElementLocation conditionLocation)
        {
            ErrorUtilities.VerifyThrowInternalLength(itemType, nameof(itemType));
            ErrorUtilities.VerifyThrowInternalLength(taskParameter, nameof(taskParameter));
            ErrorUtilities.VerifyThrowInternalNull(location, nameof(location));
            ErrorUtilities.VerifyThrowInternalNull(itemTypeLocation, nameof(itemTypeLocation));
            ErrorUtilities.VerifyThrowInternalNull(taskParameterLocation, nameof(taskParameterLocation));

            _itemType = itemType;
            _taskParameter = taskParameter;
            _condition = condition;
            _location = location;
            _itemTypeLocation = itemTypeLocation;
            _taskParameterLocation = taskParameterLocation;
            _conditionLocation = conditionLocation;
        }

        private ProjectTaskOutputItemInstance()
        {
        }

        /// <summary>
        /// Name of the item type that the outputs go into
        /// </summary>
        public string ItemType
        {
            get { return _itemType; }
        }

        /// <summary>
        /// Property on the task class to retrieve the outputs from
        /// </summary>
        public string TaskParameter
        {
            get { return _taskParameter; }
        }

        /// <summary>
        /// Condition on the element.
        /// If there is no condition, returns empty string.
        /// </summary>
        public override string Condition
        {
            get { return _condition; }
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

        /// <summary>
        /// Location of the ItemType attribute
        /// </summary>
        public ElementLocation.ElementLocation ItemTypeLocation
        {
            get { return _itemTypeLocation; }
        }

        void ITranslatable.Translate(ITranslator translator)
        {
            if (translator.Mode == TranslationDirection.WriteToStream)
            {
                var typeName = this.GetType().FullName;
                translator.Translate(ref typeName);
            }

            translator.Translate(ref _itemType);
            translator.Translate(ref _taskParameter);
            translator.Translate(ref _condition);
            translator.Translate(ref _location, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _conditionLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _itemTypeLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _taskParameterLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
        }
    }
}
