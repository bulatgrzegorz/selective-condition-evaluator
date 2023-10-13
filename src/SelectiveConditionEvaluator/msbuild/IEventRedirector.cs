// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.msbuild
{
    /// <summary>
    /// This interface is used to forward events to another loggers
    /// </summary>
    internal interface IEventRedirector
    {
        /// <summary>
        /// This method is called by the node loggers to forward the events to central logger
        /// </summary>
        void ForwardEvent(BuildEventArgs buildEvent);
    }
}
