﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Definition;
using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild.Evaluation
{
    /// <summary>
    /// Event arguments for the <see cref="ProjectCollection.ProjectChanged"/> event.
    /// </summary>
    internal class ProjectChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectChangedEventArgs"/> class.
        /// </summary>
        /// <param name="project">The changed project.</param>
        internal ProjectChangedEventArgs(Project project)
        {
            ErrorUtilities.VerifyThrowArgumentNull(project, nameof(project));

            Project = project;
        }

        /// <summary>
        /// Gets the project that was marked dirty.
        /// </summary>
        /// <value>Never null.</value>
        public Project Project { get; private set; }
    }
}
