// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SelectiveConditionEvaluator.msbuild.Sdk;
using SelectiveConditionEvaluator.msbuild.Shared;
using SdkResolverContextBase = SelectiveConditionEvaluator.msbuild.Sdk.SdkResolverContext;
using SdkResultBase = SelectiveConditionEvaluator.msbuild.Sdk.SdkResult;
using SdkResultFactoryBase = SelectiveConditionEvaluator.msbuild.Sdk.SdkResultFactory;

#nullable disable

namespace SelectiveConditionEvaluator.msbuild.BackEnd.Components.SdkResolution
{
    /// <summary>
    ///     Default SDK resolver for compatibility with VS2017 RTM.
    /// <remarks>
    ///     Default Sdk folder will to:
    ///         1) MSBuildSDKsPath environment variable if defined
    ///         2) When in Visual Studio, (VSRoot)\MSBuild\Sdks\
    ///         3) Outside of Visual Studio (MSBuild Root)\Sdks\
    /// </remarks>
    /// </summary>
    internal class DefaultSdkResolver : SdkResolver
    {
        public override string Name => "DefaultSdkResolver";

        public override int Priority => 10000;

        public override SdkResultBase Resolve(SdkReference sdk, SdkResolverContextBase context, SdkResultFactoryBase factory)
        {
            string sdkPath = Path.Combine(BuildEnvironmentHelper.Instance.MSBuildSDKsPath, sdk.Name, "Sdk");

            return FileUtilities.DirectoryExistsNoThrow(sdkPath)
                ? factory.IndicateSuccess(sdkPath, string.Empty)
                : factory.IndicateFailure(new string[] { ResourceUtilities.FormatResourceStringIgnoreCodeAndKeyword("DefaultSDKResolverError", sdk.Name, sdkPath) }, null);
        }
    }
}
