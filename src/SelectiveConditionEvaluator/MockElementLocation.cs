// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Build.Construction;

namespace SelectiveConditionEvaluator;

public class MockElementLocation : ElementLocation
{
    private MockElementLocation()
    {
    }

    public override string File => "mock.targets";
    public override int Line => 0;
    public override int Column => 1;
    internal static MockElementLocation Instance { get; } = new();
}
