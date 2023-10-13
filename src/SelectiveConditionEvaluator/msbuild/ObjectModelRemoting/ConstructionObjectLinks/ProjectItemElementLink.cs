// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.msbuild.ObjectModelRemoting.ConstructionObjectLinks
{
    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectItemElement"/>
    /// </summary>
    internal abstract class ProjectItemElementLink : ProjectElementContainerLink
    {
        /// <summary>
        /// Help implement ItemType setter for remote objects.
        /// </summary>
        /// <param name="newType"></param>
        public abstract void ChangeItemType(string newType);
    }
}
