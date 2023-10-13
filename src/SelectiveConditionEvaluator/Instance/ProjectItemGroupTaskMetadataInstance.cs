// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Diagnostics;
using SelectiveConditionEvaluator.BackEnd;
using SelectiveConditionEvaluator.Shared;

namespace SelectiveConditionEvaluator.Instance
{
    /// <summary>
    /// Wraps an unevaluated metadatum under an item in an itemgroup in a target
    /// Immutable.
    /// </summary>
    [DebuggerDisplay("{_name} Value={_value} Condition={_condition}")]
    public class ProjectItemGroupTaskMetadataInstance : ITranslatable
    {
        /// <summary>
        /// Name of the metadatum
        /// </summary>
        private string _name;

        /// <summary>
        /// Unevaluated value
        /// </summary>
        private string _value;

        /// <summary>
        /// Unevaluated condition
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
        /// Constructor called by the Evaluator.
        /// </summary>
        internal ProjectItemGroupTaskMetadataInstance(string name, string value, string condition, ElementLocation.ElementLocation location, ElementLocation.ElementLocation conditionLocation)
        {
            ErrorUtilities.VerifyThrowInternalNull(name, nameof(name));
            ErrorUtilities.VerifyThrowInternalNull(value, nameof(value));
            ErrorUtilities.VerifyThrowInternalNull(condition, nameof(condition));
            ErrorUtilities.VerifyThrowInternalNull(location, nameof(location));

            _name = name;
            _value = value;
            _condition = condition;
            _location = location;
            _conditionLocation = conditionLocation;
        }

        /// <summary>
        /// Cloning constructor
        /// </summary>
        private ProjectItemGroupTaskMetadataInstance(ProjectItemGroupTaskMetadataInstance that)
        {
            // All fields are immutable
            _name = that._name;
            _value = that._value;
            _condition = that._condition;
        }

        private ProjectItemGroupTaskMetadataInstance()
        {
        }

        /// <summary>
        /// Name of the metadatum
        /// </summary>
        public string Name
        {
            [DebuggerStepThrough]
            get
            { return _name; }
        }

        /// <summary>
        /// Unevaluated value
        /// </summary>
        public string Value
        {
            [DebuggerStepThrough]
            get
            { return _value; }
        }

        /// <summary>
        /// Unevaluated condition value
        /// </summary>
        public string Condition
        {
            [DebuggerStepThrough]
            get
            { return _condition; }
        }

        /// <summary>
        /// Location of the element
        /// </summary>
        public ElementLocation.ElementLocation Location
        {
            [DebuggerStepThrough]
            get
            { return _location; }
        }

        /// <summary>
        /// Location of the condition attribute if any
        /// </summary>
        public ElementLocation.ElementLocation ConditionLocation
        {
            [DebuggerStepThrough]
            get
            { return _conditionLocation; }
        }

        /// <summary>
        /// Deep clone
        /// </summary>
        internal ProjectItemGroupTaskMetadataInstance DeepClone()
        {
            return new ProjectItemGroupTaskMetadataInstance(this);
        }

        void ITranslatable.Translate(ITranslator translator)
        {
            translator.Translate(ref _name);
            translator.Translate(ref _value);
            translator.Translate(ref _condition);
            translator.Translate(ref _location, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _conditionLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
        }

        internal static ProjectItemGroupTaskMetadataInstance FactoryForDeserialization(ITranslator translator)
        {
            var instance = new ProjectItemGroupTaskMetadataInstance();
            ((ITranslatable)instance).Translate(translator);

            return instance;
        }
    }
}
