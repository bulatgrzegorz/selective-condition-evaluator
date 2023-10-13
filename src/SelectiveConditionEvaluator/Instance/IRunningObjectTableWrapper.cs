// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.Instance
{
    internal interface IRunningObjectTableWrapper
    {
        object GetObject(string itemName);
    }
}
