// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Collections;
using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild.Evaluation
{
    /// <summary>
    /// This interface represents a metadata object.
    /// </summary>
    internal interface IMetadatum : IKeyed, IValued
    {
    }
}
