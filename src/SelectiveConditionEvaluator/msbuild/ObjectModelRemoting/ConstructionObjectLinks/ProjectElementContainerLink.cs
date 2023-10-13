// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Construction;

namespace SelectiveConditionEvaluator.msbuild.ObjectModelRemoting.ConstructionObjectLinks
{
    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external construction objects derived from <see cref="ProjectElementContainer"/>
    /// </summary>
    internal abstract class ProjectElementContainerLink : ProjectElementLink
    {
        /// <summary>
        /// Access to remote <see cref="ProjectElementContainer.Count"/>.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Access to remote <see cref="ProjectElementContainer.FirstChild"/>.
        /// </summary>
        public abstract ProjectElement FirstChild { get; }

        /// <summary>
        /// Access to remote <see cref="ProjectElementContainer.LastChild"/>.
        /// </summary>
        public abstract ProjectElement LastChild { get; }

        /// <summary>
        /// Facilitate remoting the <see cref="ProjectElementContainer.InsertAfterChild"/>.
        /// </summary>
        public abstract void InsertAfterChild(ProjectElement child, ProjectElement reference);

        /// <summary>
        /// Facilitate remoting the <see cref="ProjectElementContainer.InsertBeforeChild"/>.
        /// </summary>
        public abstract void InsertBeforeChild(ProjectElement child, ProjectElement reference);

        /// <summary>
        /// Helps implementation of the <see cref="ProjectElementContainer.AppendChild"/>.
        /// </summary>
        public abstract void AddInitialChild(ProjectElement child);

        /// <summary>
        /// helps implementation the <see cref="ProjectElementContainer.DeepCopyFrom"/>.
        /// </summary>
        public abstract ProjectElementContainer DeepClone(ProjectRootElement factory, ProjectElementContainer parent);

        /// <summary>
        /// Facilitate remoting the <see cref="ProjectElementContainer.RemoveChild"/>.
        /// </summary>
        public abstract void RemoveChild(ProjectElement child);

        /// <summary>
        /// ExternalProjectsProvider helpers
        /// </summary>
        public static void AddInitialChild(ProjectElementContainer xml, ProjectElement child) => xml.AddInitialChild(child);
        public static ProjectElementContainer DeepClone(ProjectElementContainer xml, ProjectRootElement factory, ProjectElementContainer parent) => ProjectElementContainer.DeepClone(xml, factory, parent);
    }

    // the "equivalence" classes in cases when we don't need additional functionality,
    // but want to allow for such to be added in the future.

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectChooseElement"/>
    /// </summary>
    internal abstract class ProjectChooseElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectImportGroupElement"/>
    /// </summary>
    internal abstract class ProjectImportGroupElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectItemDefinitionElement"/>
    /// </summary>
    internal abstract class ProjectItemDefinitionElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectItemDefinitionGroupElement"/>
    /// </summary>
    internal abstract class ProjectItemDefinitionGroupElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectItemGroupElement"/>
    /// </summary>
    internal abstract class ProjectItemGroupElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectOtherwiseElement"/>
    /// </summary>
    internal abstract class ProjectOtherwiseElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectPropertyGroupElement"/>
    /// </summary>
    internal abstract class ProjectPropertyGroupElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectSdkElement"/>
    /// </summary>
    internal abstract class ProjectSdkElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectUsingTaskElement"/>
    /// </summary>
    internal abstract class ProjectUsingTaskElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="ProjectWhenElement"/>
    /// </summary>
    internal abstract class ProjectWhenElementLink : ProjectElementContainerLink { }

    /// <summary>
    /// External projects support.
    /// Allow for creating a local representation to external object of type <see cref="UsingTaskParameterGroupElement"/>
    /// </summary>
    internal abstract class UsingTaskParameterGroupElementLink : ProjectElementContainerLink { }
}
