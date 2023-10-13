// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Diagnostics.CodeAnalysis;

namespace SelectiveConditionEvaluator.msbuild
{
    /// <summary>
    /// This attribute is used to mark a task class as explicitly not being required to run in the STA for COM.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "MTA", Justification = "It is cased correctly.")]
    internal sealed class RunInMTAAttribute : Attribute
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RunInMTAAttribute()
        {
            // do nothing
        }
    }
}
