// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SelectiveConditionEvaluator.msbuild.BuildException;

/// <summary>
/// A catch-all type for remote exceptions that we don't know how to deserialize.
/// </summary>
internal sealed class GenericBuildTransferredException : BuildExceptionBase
{
    public GenericBuildTransferredException()
        : base()
    { }

    internal GenericBuildTransferredException(
        string message,
        Exception? inner)
        : base(message, inner)
    { }
}
