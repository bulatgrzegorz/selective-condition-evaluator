﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SelectiveConditionEvaluator.msbuild.Logging.BinaryLogger
{
    /// <summary>
    /// An event args for <see cref="IBuildEventStringsReader.StringReadDone"/> callback.
    /// </summary>
    internal sealed class StringReadEventArgs : EventArgs
    {
        /// <summary>
        /// The original string that was read from the binary log.
        /// </summary>
        public string OriginalString { get; private set; }

        /// <summary>
        /// The adjusted string (or the original string of none subscriber replaced it) that will be used by the reader.
        /// </summary>
        public string StringToBeUsed { get; set; }

        public StringReadEventArgs(string str)
        {
            OriginalString = str;
            StringToBeUsed = str;
        }

        internal void Reuse(string newValue)
        {
            OriginalString = newValue;
            StringToBeUsed = newValue;
        }
    }
}
