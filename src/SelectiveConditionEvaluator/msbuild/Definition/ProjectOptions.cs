﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Evaluation.Context;
using SelectiveConditionEvaluator.msbuild.FileSystem;

namespace SelectiveConditionEvaluator.msbuild.Definition
{
    /// <summary>
    ///     Common <see cref="Project" /> constructor arguments.
    /// </summary>
    internal class ProjectOptions
    {
        /// <summary>
        /// Global properties to evaluate with.
        /// </summary>
        public IDictionary<string, string> GlobalProperties { get; set; }

        /// <summary>
        /// Tools version to evaluate with
        /// </summary>
        public string ToolsVersion { get; set; }

        /// <summary>
        /// Sub-toolset version to explicitly evaluate the toolset with.
        /// </summary>
        public string SubToolsetVersion { get; set; }

        /// <summary>
        /// The <see cref="ProjectCollection"/> the project is added to. Default is <see cref="ProjectCollection.GlobalProjectCollection"/>.
        /// </summary>
        public ProjectCollection ProjectCollection { get; set; }

        /// <summary>
        /// The <see cref="ProjectLoadSettings"/> to use for evaluation.
        /// </summary>
        public ProjectLoadSettings LoadSettings { get; set; } = ProjectLoadSettings.Default;

        /// <summary>
        /// The <see cref="EvaluationContext"/> to use for evaluation.
        /// </summary>
        public EvaluationContext EvaluationContext { get; set; }

        /// <summary>
        /// Provides <see cref="IDirectoryCache"/> to be used for evaluation.
        /// </summary>
        public IDirectoryCacheFactory DirectoryCacheFactory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if loading the project is allowed to interact with the user.
        /// </summary>
        public bool Interactive { get; set; }
    }
}
