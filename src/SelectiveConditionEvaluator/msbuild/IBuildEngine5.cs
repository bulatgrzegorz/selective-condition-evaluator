// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.msbuild
{
    /// <summary>
    /// This interface extends IBuildEngine to log telemetry.
    /// </summary>
    internal interface IBuildEngine5 : IBuildEngine4
    {
        /// <summary>
        /// Logs telemetry.
        /// </summary>
        /// <param name="eventName">The event name.</param>
        /// <param name="properties">The event properties.</param>
        void LogTelemetry(string eventName, IDictionary<string, string> properties);
    }
}
