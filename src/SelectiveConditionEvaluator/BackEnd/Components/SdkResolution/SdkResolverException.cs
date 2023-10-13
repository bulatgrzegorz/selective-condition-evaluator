// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.BuildException;
using SelectiveConditionEvaluator.Sdk;
using SelectiveConditionEvaluator.Shared;

namespace SelectiveConditionEvaluator.BackEnd.Components.SdkResolution
{
    /// <summary>
    /// Represents an exception that occurs when an SdkResolver throws an unhandled exception.
    /// </summary>
    public class SdkResolverException : BuildExceptionBase
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
