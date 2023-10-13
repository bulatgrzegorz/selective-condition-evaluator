// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.msbuild
{
    /// <summary>
    /// This attribute is used to mark tasks that need to be run in their own app domains. The build engine will create a new app
    /// domain each time it needs to run such a task, and immediately unload it when the task is finished.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal sealed class LoadInSeparateAppDomainAttribute : Attribute
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoadInSeparateAppDomainAttribute()
        {
            // do nothing
        }
    }
}
