using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Collections;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Shared.FileSystem;
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
    [InlineData("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(TargetFramework)' == 'net8.0' and '$(OS)' == 'Ubuntu') or $(TargetFramework.TrimStart('net')) >= '6.0'", "TargetFramework", "net6.0", true)]
    public void EvaluateSelective_TargetFrameworkAndOthers(string condition, string propertyName, string propertyValue, bool expectedResult)
    {
        var parser = new SelectiveParser(propertyName, propertyValue);
        parser.EvaluateSelective(condition).ShouldBe(expectedResult, condition);
    }

    [Theory]
    [InlineData("('$(prop1)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(prop2)' == 'net6.0' and '$(OS)' == 'Ubuntu')", "prop1", "0", "prop2", "0", false)]
    [InlineData("('$(prop1)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(prop2)' == 'net6.0' and '$(OS)' == 'Ubuntu')", "prop1", "net7.0", "prop2", "0", true)]
    public void EvaluateSelective_MultipleSelectiveProperties(string condition, string propertyName1, string propertyValue1, string propertyName2, string propertyValue2, bool expectedResult)
    {
        var parser = new SelectiveParser(new Dictionary<string, string>()
        {
            {propertyName1, propertyValue1},
            {propertyName2, propertyValue2},
        });
        parser.EvaluateSelective(condition).ShouldBe(expectedResult, condition);
    }

    [Theory]
    [MemberData(nameof(TrueTests))]
    public void EvaluateAVarietyOfTrueExpressions(string expression)
    {
        var parser = new SelectiveParser();
        parser.EvaluateSelective(expression).ShouldBe(true, expression);
    }

    [Theory]
    [MemberData(nameof(FalseTests))]
    public void EvaluateAVarietyOfFalseExpressions(string expression)
    {
        // Those tests should have evaluate to false in normal execution. In our situation, where we evaluating only selective property conditions they will end up as true (what is expected)
        var parser = new SelectiveParser();
        parser.EvaluateSelective(expression).ShouldBe(true, expression);
    }

    public static readonly IEnumerable<object[]> TrueTests = new[]
    {
            "true or (SHOULDNOTEVALTHIS)", // short circuit
            "(true and false) or true",
            "false or true or false",
            "(true) and (true)",
            "false or !false",
            "($(a) or true)",
            "('$(c)'==1 and (!false))",
            "@(z -> '%(filename).z', '$')=='xxx.z$yyy.z'",
            "@(w -> '%(definingprojectname).barproj') == 'foo.barproj'",
            "false or (false or (false or (false or (false or (true)))))",
            "!(true and false)",
            "$(and)=='and'",
            "0x1==1.0",
            "0xa==10",
            "0<0.1",
            "+4>-4",
            "'-$(c)'==-1",
            "$(a)==faLse",
            "$(a)==oFF",
            "$(a)==no",
            "$(a)!=true",
            "$(b)== True",
            "$(b)==on",
            "$(b)==yes",
            "$(b)!=1",
            "$(c)==1",
            "$(d)=='xxx'",
            "$(d)==$(e)",
            "$(d)=='$(e)'",
            "@(y)==$(d)",
            "'@(z)'=='xxx;yyy'",
            "$(a)==$(a)",
            "'1'=='1'",
            "'1'==1",
            "1\n==1",
            "1\t==\t\r\n1",
            "123=='0123.0'",
            "123==123",
            "123==0123",
            "123==0123.0",
            "123!=0123.01",
            "1.2.3<=1.2.3.0",
            "12.23.34==12.23.34",
            "0.8.0.0<8.0.0",
            "1.1.2>1.0.1.2",
            "8.1>8.0.16.23",
            "8.0.0>=8",
            "6<=6.0.0.1",
            "7>6.8.2",
            "4<5.9.9135.4",
            "3!=3.0.0",
            "1.2.3.4.5.6.7==1.2.3.4.5.6.7",
            "00==0",
            "0==0.0",
            "1\n\t==1",
            "+4==4",
            "44==+44.0 and -44==-44.0",
            "false==no",
            "true==yes",
            "true==!false",
            "yes!=no",
            "false!=1",
            "$(c)>0",
            "!$(a)",
            "$(b)",
            "($(d)==$(e))",
            "!true==false",
            "a_a==a_a",
            "a_a=='a_a'",
            "_a== _a",
            "@(y -> '%(filename)')=='xxx'",
            "@(z -> '%(filename)', '!')=='xxx!yyy'",
            "'xxx!yyy'==@(z -> '%(filename)', '!')",
            "'$(a)'==(false)",
            "('$(a)'==(false))",
            "1>0",
            "2<=2",
            "2<=3",
            "1>=1",
            "1>=-1",
            "-1==-1",
            "-1  <  0",
            "(1==1)and('a'=='a')",
            "(true) and ($(a)==off)",
            "(true) and ($(d)==xxx)",
            "(false)     or($(d)==xxx)",
            "!(false)and!(false)",
            "'and'=='AND'",
            "$(d)=='XxX'",
            "true or true or false",
            "false or true or !true or'1'",
            "$(a) or $(b)",
            "$(a) or true",
            "!!true",
            "'$(e)1@(y)'=='xxx1xxx'",
            "0x11==17",
            "0x01a==26",
            "0xa==0x0A",
            "@(x)",
            "1<=@(w)",
            "'%77'=='w'",
            "'%zz'=='%zz'",
            "true or 1",
            "true==!false",
            "(!(true))=='off'",
            "@(w)>0",
            "%(culture)=='FRENCH'",
            "'%(culture) fries' == 'FRENCH FRIES' ",
            @"'%(HintPath)' == ''",
            @"%(HintPath) != 'c:\myassemblies\foo.dll'",
            "exists('a')",
            "exists(a)",
            "exists('a%3bb')", /* semicolon */
            "exists('a%27b')", /* apostrophe */
            "exists('a;c')", /* items */
            "exists($(a_semi_c))",
            "exists($(a_escapedsemi_b))",
            "exists('$(a_escapedsemi_b)')",
            "exists($(a_escapedapos_b))",
            "exists('$(a_escapedapos_b)')",
            "exists($(a_apos_b))",
            "exists('$(a_apos_b)')",
            "exists(@(v))",
            "exists('@(v)')",
            "exists('%3b')",
            "exists('%27')",
            "exists('@(v);@(nonexistent)')",
            @"HASTRAILINGSLASH('foo\')",
            @"!HasTrailingSlash('foo')",
            @"HasTrailingSlash('foo/')",
            @"HasTrailingSlash($(has_trailing_slash))",
            "'59264.59264' == '59264.59264'",
            "1" + new String('0', 500) + "==" + "1" + new String('0', 500), /* too big for double, eval as string */
            "'1" + new String('0', 500) + "'=='" + "1" + new String('0', 500) + "'" /* too big for double, eval as string */
        }.Select(s => new[] { s });

        public static readonly IEnumerable<object[]> FalseTests = new[] {
            "false and SHOULDNOTEVALTHIS", // short circuit
            "$(a)!=no",
            "$(b)==1.1",
            "$(c)==$(a)",
            "$(d)!=$(e)",
            "!$(b)",
            "false or false or false",
            "false and !((true and false))",
            "on and off",
            "(true) and (false)",
            "false or (false or (false or (false or (false or (false)))))",
            "!$(b)and true",
            "1==a",
            "!($(d)==$(e))",
            "$(a) and true",
            "true==1",
            "false==0",
            "(!(true))=='x'",
            "oops==false",
            "oops==!false",
            "%(culture) == 'english'",
            "'%(culture) fries' == 'english fries' ",
            @"'%(HintPath)' == 'c:\myassemblies\foo.dll'",
            @"%(HintPath) == 'c:\myassemblies\foo.dll'",
            "exists('')",
            "exists(' ')",
            "exists($(nonexistent))",  // DDB #141195
            "exists('$(nonexistent)')",  // DDB #141195
            "exists(@(nonexistent))",  // DDB #141195
            "exists('@(nonexistent)')",  // DDB #141195
            "exists('\t')",
            "exists('@(u)')",
            "exists('$(foo_apos_foo)')",
            "!exists('a')",
            "!exists('a;c')",
            "!exists('$(a_semi_c)')",
            "exists('a;b;c')", /* a and c exist but b does not */
            "exists('b;c;a')",
            "exists('$(a_semi_c);b')",
            "!!!exists(a)",
            "exists('|||||')",
            @"hastrailingslash('foo')",
            @"hastrailingslash('')",
            @"HasTrailingSlash($(nonexistent))",
            "'59264.59264' == '59264.59265'",
            "1.2.0==1.2",
            "$(f)!=$(f)",
            "1.3.5.8>1.3.6.8",
            "0.8.0.0>=1.0",
            "8.0.0<=8.0",
            "8.1.2<8",
            "1" + new String('0', 500) + "==2", /* too big for double, eval as string */
            "'1" + new String('0', 500) + "'=='2'", /* too big for double, eval as string */
            "'1" + new String('0', 500) + "'=='01" + new String('0', 500) + "'" /* too big for double, eval as string */
        }.Select(s => new[] { s });
}
