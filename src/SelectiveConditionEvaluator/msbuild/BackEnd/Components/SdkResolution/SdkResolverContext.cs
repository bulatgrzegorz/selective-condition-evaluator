// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.msbuild.BackEnd.Components.SdkResolution
{
    /// <summary>
    /// An internal implementation of <see cref="Sdk.SdkResolverContext"/>.
    /// </summary>
    internal sealed class SdkResolverContext : Sdk.SdkResolverContext
    {
        public SdkResolverContext(Sdk.SdkLogger logger, string projectFilePath, string solutionPath, Version msBuildVersion, bool interactive, bool isRunningInVisualStudio)
        {
            Logger = logger;
            ProjectFilePath = projectFilePath;
            SolutionFilePath = solutionPath;
            MSBuildVersion = msBuildVersion;
            Interactive = interactive;
            IsRunningInVisualStudio = isRunningInVisualStudio;
        }
    }
}
