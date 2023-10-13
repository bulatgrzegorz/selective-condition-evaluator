// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SelectiveConditionEvaluator.msbuild
{
    /// <summary>
    /// Arguments for the response file used event
    /// </summary>
    [Serializable]
    internal class ResponseFileUsedEventArgs : BuildMessageEventArgs
    {
        public ResponseFileUsedEventArgs()
        {
        }
        /// <summary>
        /// Initialize a new instance of the ResponseFileUsedEventArgs class.
        /// </summary>
        public ResponseFileUsedEventArgs(string? responseFilePath) : base()
        {
            ResponseFilePath = responseFilePath;
        }
        public string? ResponseFilePath { set; get; }
    }
}
