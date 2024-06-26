﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SelectiveConditionEvaluator.msbuild.BackEnd.Components.ProjectCache
{
    internal readonly struct FileAccessContext
    {
        public FileAccessContext(
            string projectFullPath,
            IReadOnlyDictionary<string, string> globalProperties,
            IReadOnlyList<string> targets)
        {
            ProjectFullPath = projectFullPath;
            GlobalProperties = globalProperties;
            Targets = targets;
        }

        public string ProjectFullPath { get; }

        public IReadOnlyDictionary<string, string> GlobalProperties { get; }

        public IReadOnlyList<string> Targets { get; }
    }
}
