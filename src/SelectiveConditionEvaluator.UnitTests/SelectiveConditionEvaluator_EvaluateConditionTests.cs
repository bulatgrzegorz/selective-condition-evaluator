// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace SelectiveConditionEvaluator.UnitTests;

public class ConditionEvaluatorTests
{
    [Theory]
    [InlineData("'$(TargetFramework.TrimEnd(`0123456789`))' == 'net'", "TargetFramework", "net45", true)]
    [InlineData("'$(TargetFramework.TrimEnd(`0123456789`))' == 'net'", "TargetFramework", "net8.0", false)]
    [InlineData("'$(TargetFramework)' == 'net7.0'", "TargetFramework", "net8.0", false)]
    [InlineData("'$(TargetFramework)' == 'net7.0'", "TargetFramework", "net7.0", true)]
    [InlineData("$(TargetFramework.Contains('coreapp'))", "TargetFramework", "netcoreapp2.1", true)]
    [InlineData("$(TargetFramework.Contains('coreapp'))", "TargetFramework", "netstandard2.0", false)]
    [InlineData("$(TargetFramework.StartsWith('net4'))", "TargetFramework", "net461", true)]
    [InlineData("!($(TargetFramework.StartsWith('net8')))", "TargetFramework", "net461", true)]
    [InlineData("$(TargetFramework.TrimStart('net46')) > 1", "TargetFramework", "net462", true)]
    [InlineData("$(TargetFramework.TrimStart('net46')) < 2", "TargetFramework", "net462", false)]
    [InlineData("$(TargetFramework.TrimStart('net46')) >= 3", "TargetFramework", "net462", false)]
    [InlineData("$(TargetFramework.TrimStart('net46')) <= 2", "TargetFramework", "net462", true)]
    public void EvaluateSelective_JustTargetFramework(string condition, string propertyName, string propertyValue, bool expectedResult)
    {
        var parser = new SelectiveParser(propertyName, propertyValue);
        parser.EvaluateSelective(condition).ShouldBe(expectedResult, condition);
    }

    [Theory]
    [InlineData("$(TargetFramework.StartsWith('net4')) and '$(OS)' == 'Windows_NT'", "TargetFramework", "net461", true)]
    [InlineData("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(TargetFramework)' == 'net6.0' and '$(OS)' == 'Ubuntu')", "TargetFramework", "net6.0", true)]
    [InlineData("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(TargetFramework)' == 'net8.0' and '$(OS)' == 'Ubuntu')", "TargetFramework", "net6.0", false)]
    [InlineData("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') and ('$(TargetFramework)' == 'net8.0' and '$(OS)' == 'Ubuntu')", "TargetFramework", "net6.0", false)]
    [InlineData("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') and ('$(TargetFramework)' == 'net8.0' and '$(OS)' == 'Ubuntu')", "TargetFramework", "net7.0", false)]
    [InlineData("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') and ('$(TargetFramework)' == 'net8.0' and '$(OS)' == 'Ubuntu')", "TargetFramework", "net8.0", false)]
    [InlineData("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(TargetFramework)' == 'net8.0' and '$(OS)' == 'Ubuntu') or $(TargetFramework.TrimStart('net')) >= '6.0'", "TargetFramework", "net6.0", true)]
    public void EvaluateSelective_TargetFrameworkAndOthers(string condition, string propertyName, string propertyValue, bool expectedResult)
    {
        var parser = new SelectiveParser(propertyName, propertyValue);
        parser.EvaluateSelective(condition).ShouldBe(expectedResult, condition);
    }
    
    [Theory]
    [InlineData("'$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT'", "TargetFramework", "net6.0", false)]
    [InlineData("'$(TargetFramework)' == 'net7.0' or '$(OS)' == 'Windows_NT'", "TargetFramework", "net6.0", false)]
    [InlineData("'$(TargetFramework)' == 'net6.0' and '$(OS)' == 'Windows_NT'", "TargetFramework", "net6.0", true)]
    [InlineData("'$(TargetFramework)' == 'net6.0' or '$(OS)' == 'Windows_NT'", "TargetFramework", "net6.0", true)]
    public void EvaluateSelective_WillWorkCorrectlyWithBooleanOperations(string condition, string propertyName, string propertyValue, bool expectedResult)
    {
        var parser = new SelectiveParser(propertyName, propertyValue);
        parser.EvaluateSelective(condition).ShouldBe(expectedResult, condition);
    }

    [Theory]
    [InlineData("('$(prop1)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(prop2)' == 'net6.0' and '$(OS)' == 'Ubuntu')", "prop1", "0", "prop2", "0", false)]
    [InlineData("('$(prop1)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(prop2)' == 'net6.0' and '$(OS)' == 'Ubuntu')", "prop1", "net7.0", "prop2", "0", true)]
    [InlineData("('$(prop1)' == 'net7.0' and '$(OS)' == 'Windows_NT') and ('$(prop2)' == 'net6.0' and '$(OS)' == 'Ubuntu')", "prop1", "net7.0", "prop2", "0", false)]
    [InlineData("('$(prop1)' == 'net7.0' and '$(OS)' == 'Windows_NT') and ('$(prop2)' == 'net6.0' and '$(OS)' == 'Ubuntu')", "prop1", "net6.0", "prop2", "0", false)]
    public void EvaluateSelective_MultipleSelectiveProperties(string condition, string propertyName1, string propertyValue1, string propertyName2, string propertyValue2, bool expectedResult)
    {
        var parser = new SelectiveParser(new Dictionary<string, string>()
        {
            {propertyName1, propertyValue1},
            {propertyName2, propertyValue2},
        });
        parser.EvaluateSelective(condition).ShouldBe(expectedResult, condition);
    }
}
