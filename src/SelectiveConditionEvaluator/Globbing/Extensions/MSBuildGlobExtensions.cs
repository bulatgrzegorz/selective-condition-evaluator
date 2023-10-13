﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.Globbing.Visitor;

namespace SelectiveConditionEvaluator.Globbing.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IMSBuildGlob"/>
    /// </summary>
    public static class MSBuildGlobExtensions
    {
        /// <summary>
        /// Retrieve all the <see cref="MSBuildGlob"/> objects from the given <paramref name="glob"/> composite.
        /// </summary>
        /// <param name="glob">A glob composite</param>
        /// <returns></returns>
        public static IEnumerable<MSBuildGlob> GetParsedGlobs(this IMSBuildGlob glob)
        {
            var parsedGlobVisitor = new ParsedGlobCollector();
            parsedGlobVisitor.Visit(glob);

            return parsedGlobVisitor.CollectedGlobs;
        }
    }
}
