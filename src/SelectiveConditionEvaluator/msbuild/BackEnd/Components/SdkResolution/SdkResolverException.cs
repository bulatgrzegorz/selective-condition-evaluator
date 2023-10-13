// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.BuildException;
using SelectiveConditionEvaluator.msbuild.Sdk;
using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild.BackEnd.Components.SdkResolution
{
    /// <summary>
    /// Represents an exception that occurs when an SdkResolver throws an unhandled exception.
    /// </summary>
    internal class SdkResolverException : BuildExceptionBase
    {
        public SdkResolver Resolver { get; private set; }

        public SdkReference Sdk { get; private set; }

        public SdkResolverException(string resourceName, SdkResolver resolver, SdkReference sdk, Exception innerException, params string[] args)
            : base(string.Format(ResourceUtilities.GetResourceString(resourceName), args), innerException)
        {
            Resolver = resolver;
            Sdk = sdk;
        }

        // Do not remove - used by BuildExceptionSerializationHelper
        internal SdkResolverException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
