// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.ObjectModelRemoting.ConstructionObjectLinks
{
    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectUsingTaskParameterElement"/>
    /// </summary>
    public abstract class ProjectUsingTaskParameterElementLink : ProjectElementLink
    {
        /// <summary>
        /// Access to remote <see cref="ProjectUsingTaskParameterElementLink.Name"/>.
        /// </summary>
        public abstract string Name { get; set; }
    }
}
