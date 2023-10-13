// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.msbuild.ObjectModelRemoting.ConstructionObjectLinks
{
    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectTaskElement"/>
    /// </summary>
    internal abstract class ProjectTaskElementLink : ProjectElementContainerLink
    {
        /// <summary>
        /// Access to remote <see cref="ProjectTaskElement.Parameters"/>.
        /// </summary>
        public abstract IDictionary<string, string> Parameters { get; }

        /// <summary>
        /// Access to remote <see cref="ProjectTaskElement.ParameterLocations"/>.
        /// </summary>
        public abstract IEnumerable<KeyValuePair<string, ElementLocation.ElementLocation>> ParameterLocations { get; }

        /// <summary>
        /// Facilitate remoting the <see cref="ProjectTaskElement.GetParameter"/>.
        /// </summary>
        public abstract string GetParameter(string name);

        /// <summary>
        /// Facilitate remoting the <see cref="ProjectTaskElement.SetParameter"/>.
        /// </summary>
        public abstract void SetParameter(string name, string unevaluatedValue);

        /// <summary>
        /// Facilitate remoting the <see cref="ProjectTaskElement.RemoveParameter"/>.
        /// </summary>
        public abstract void RemoveParameter(string name);

        /// <summary>
        /// Facilitate remoting the <see cref="ProjectTaskElement.RemoveAllParameters"/>.
        /// </summary>
        public abstract void RemoveAllParameters();
    }
}
