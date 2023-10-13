# SelectiveConditionEvaluator

SelectiveConditionEvaluator is a library build on top of [msBuild](https://github.com/dotnet/msbuild). It allows to evaluate [conditions](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-conditions) (from csproj attribute for example) in selective way, taking into consideration only given properties.

## Installing

SelectiveConditionEvaluator is available on nuget, you can install it simple by executing:
```cmd
dotnet add package SelectiveConditionEvaluator
```

## Usage

Library is exposing `SelectiveParser` class. You can initialize it, executing constructor with property/properties of choice:
```csharp
var parser = new SelectiveConditionEvaluator.SelectiveParser("TargetFramework", "netstandard1.6"); 
```

then, you can simply evaluate any condition, while only given property will be included into logic:
```csharp
var result = parser.EvaluateSelective("'$(TargetFramework)' == 'netstandard1.6'"); //true
```

## Internals

As parser is build on top of msBuild, it will deal with everything that would be normally accepted:
```csharp
var parser = new SelectiveConditionEvaluator.SelectiveParser("TargetFramework", "net45");
```
```csharp
var result = parser.EvaluateSelective("'$(TargetFramework.TrimEnd(`0123456789`))' == 'net'"); //true
var result = parser.EvaluateSelective("('$(TargetFramework)' == 'net7.0' and '$(OS)' == 'Windows_NT') or ('$(TargetFramework)' == 'net45' and '$(OS)' == 'Ubuntu')"); //true
```

as example above, condition can contain any other properties that are not given. Condition will still be evaluated correctly in context of properties of interest.