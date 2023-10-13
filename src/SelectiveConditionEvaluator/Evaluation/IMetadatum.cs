// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.Build.Collections;
using SelectiveConditionEvaluator.Collections;

namespace SelectiveConditionEvaluator.Evaluation
{
    /// <summary>
    /// This interface represents a metadata object.
    /// </summary>
    internal interface IMetadatum : IKeyed, IValued
    {
    }
}
