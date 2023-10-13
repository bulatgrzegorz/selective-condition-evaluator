// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SelectiveConditionEvaluator.Sdk;
using SelectiveConditionEvaluator.Shared;
using SdkResolverContextBase = SelectiveConditionEvaluator.Sdk.SdkResolverContext;
using SdkResultBase = SelectiveConditionEvaluator.Sdk.SdkResult;
using SdkResultFactoryBase = SelectiveConditionEvaluator.Sdk.SdkResultFactory;

#nullable disable

namespace SelectiveConditionEvaluator.BackEnd.Components.SdkResolution
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
