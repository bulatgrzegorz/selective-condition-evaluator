// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Diagnostics;
using Microsoft.Build.Collections;
using Microsoft.Build.Shared;
using SelectiveConditionEvaluator.BackEnd;

namespace SelectiveConditionEvaluator.Instance
{
    /// <summary>
    /// Wraps an unevaluated propertygroup under a target.
    /// Immutable.
    /// </summary>
    [DebuggerDisplay("Condition={_condition}")]
    public class ProjectPropertyGroupTaskInstance : ProjectTargetInstanceChild, ITranslatable
    {
        /// <summary>
        /// Condition, if any
        /// </summary>
        private string _condition;

        /// <summary>
        /// Child properties.
        /// Not ProjectPropertyInstances, as these are evaluated during the build.
        /// </summary>
        private List<ProjectPropertyGroupTaskPropertyInstance> _properties;

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
        /// Assumes ProjectPropertyGroupTaskPropertyInstance is an immutable type.
        /// </summary>
        internal ProjectPropertyGroupTaskInstance(
            string condition,
            ElementLocation.ElementLocation location,
            ElementLocation.ElementLocation conditionLocation,
            List<ProjectPropertyGroupTaskPropertyInstance> properties)
        {
            ErrorUtilities.VerifyThrowInternalNull(condition, nameof(condition));
            ErrorUtilities.VerifyThrowInternalNull(location, nameof(location));
            ErrorUtilities.VerifyThrowInternalNull(properties, nameof(properties));

            _condition = condition;
            _location = location;
            _conditionLocation = conditionLocation;
            _properties = properties;
        }

        private ProjectPropertyGroupTaskInstance()
        {
        }

        /// <summary>
        /// Cloning constructor
        /// </summary>
        private ProjectPropertyGroupTaskInstance(ProjectPropertyGroupTaskInstance that)
        {
            // All members are immutable
            _condition = that._condition;
            _properties = that._properties;
        }

        /// <summary>
        /// Condition, if any.
        /// May be empty string.
        /// </summary>
        public override string Condition
        {
            [DebuggerStepThrough]
            get
            { return _condition; }
        }

        /// <summary>
        /// Child properties
        /// </summary>
        public ICollection<ProjectPropertyGroupTaskPropertyInstance> Properties
        {
            [DebuggerStepThrough]
            get
            {
                return (_properties == null) ?
                    (ICollection<ProjectPropertyGroupTaskPropertyInstance>)ReadOnlyEmptyCollection<ProjectPropertyGroupTaskPropertyInstance>.Instance :
                    new ReadOnlyCollection<ProjectPropertyGroupTaskPropertyInstance>(_properties);
            }
        }

        /// <summary>
        /// Location of the element itself
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
        /// Deep clone
        /// </summary>
        internal ProjectPropertyGroupTaskInstance DeepClone()
        {
            return new ProjectPropertyGroupTaskInstance(this);
        }
        void ITranslatable.Translate(ITranslator translator)
        {
            if (translator.Mode == TranslationDirection.WriteToStream)
            {
                var typeName = this.GetType().FullName;
                translator.Translate(ref typeName);
            }

            translator.Translate(ref _condition);
            translator.Translate(ref _properties, ProjectPropertyGroupTaskPropertyInstance.FactoryForDeserialization);
            translator.Translate(ref _location, ElementLocation.ElementLocation.FactoryForDeserialization);
            translator.Translate(ref _conditionLocation, ElementLocation.ElementLocation.FactoryForDeserialization);
        }
    }
}
