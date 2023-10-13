// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SelectiveConditionEvaluator.BackEnd.Components.Logging;

#nullable disable

namespace SelectiveConditionEvaluator.BackEnd.Components.SdkResolution
{
    /// <summary>
    /// An internal implementation of <see cref="SdkLogger"/>.
    /// </summary>
    internal class SdkLogger : Sdk.SdkLogger
    {
        private readonly LoggingContext _loggingContext;

        public SdkLogger(LoggingContext loggingContext)
        {
            _loggingContext = loggingContext;
        }

        public override void LogMessage(string message, MessageImportance messageImportance = MessageImportance.Low)
        {
            _loggingContext.LogCommentFromText(messageImportance, message);
        }
    }
}
