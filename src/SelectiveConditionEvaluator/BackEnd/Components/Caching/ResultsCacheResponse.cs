// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


#nullable disable

using SelectiveConditionEvaluator.BackEnd.Shared;

namespace SelectiveConditionEvaluator.BackEnd.Components.Caching
{
    /// <summary>
    /// The type of response.
    /// </summary>
    internal enum ResultsCacheResponseType
    {
        /// <summary>
        /// There were no matching results, or some implicit targets need to be built.
        /// </summary>
        NotSatisfied,

        /// <summary>
        /// All explicit and implicit targets have results.
        /// </summary>
        Satisfied
    }

    /// <summary>
    /// Container for results of IResultsCache.SatisfyRequest
    /// </summary>
    internal struct ResultsCacheResponse
    {
        /// <summary>
        /// The results type.
        /// </summary>
        public ResultsCacheResponseType Type;

        /// <summary>
        /// The actual results, if the request was satisfied.
        /// </summary>
        public BuildResult Results;

        /// <summary>
        /// The subset of explicit targets which must be built because there are no results for them in the cache.
        /// </summary>
        public HashSet<string> ExplicitTargetsToBuild;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">The response type.</param>
        public ResultsCacheResponse(ResultsCacheResponseType type)
        {
            Type = type;
            Results = null;
            ExplicitTargetsToBuild = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
