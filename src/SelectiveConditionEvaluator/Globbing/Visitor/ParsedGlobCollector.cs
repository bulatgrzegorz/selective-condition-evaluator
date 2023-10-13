// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Collections.Immutable;

namespace SelectiveConditionEvaluator.Globbing.Visitor
{
    internal class ParsedGlobCollector : GlobVisitor
    {
        private readonly ImmutableList<MSBuildGlob>.Builder _collectedGlobs = ImmutableList.CreateBuilder<MSBuildGlob>();
        public ImmutableList<MSBuildGlob> CollectedGlobs => _collectedGlobs.ToImmutable();

        protected override void VisitMSBuildGlob(MSBuildGlob msbuildGlob)
        {
            _collectedGlobs.Add(msbuildGlob);
        }
    }
}
