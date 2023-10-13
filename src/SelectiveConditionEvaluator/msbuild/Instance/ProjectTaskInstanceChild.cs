// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.BackEnd.Components.Communications;
using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild.Instance
{
    /// <summary>
    /// Type for TaskOutputItem and TaskOutputProperty
    /// allowing them to be used in a single collection
    /// </summary>
    internal abstract class ProjectTaskInstanceChild : ITranslatable
    {
        /// <summary>
        /// Condition on the element
        /// </summary>
        public abstract string Condition
        {
            get;
        }

        /// <summary>
        /// Location of the original element
        /// </summary>
        public abstract ElementLocation.ElementLocation Location
        {
            get;
        }

        /// <summary>
        /// Location of the TaskParameter attribute
        /// </summary>
        public abstract ElementLocation.ElementLocation TaskParameterLocation
        {
            get;
        }

        /// <summary>
        /// Location of the original condition attribute, if any
        /// </summary>
        public abstract ElementLocation.ElementLocation ConditionLocation
        {
            get;
        }

        void ITranslatable.Translate(ITranslator translator)
        {
            // all subclasses should be translateable
            ErrorUtilities.ThrowInternalErrorUnreachable();
        }

        internal static ProjectTaskInstanceChild FactoryForDeserialization(ITranslator translator)
        {
            return translator.FactoryForDeserializingTypeWithName<ProjectTaskInstanceChild>();
        }
    }
}
