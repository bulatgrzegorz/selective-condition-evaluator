﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.Shared.FileSystem
{
    /// <summary>
    /// Factory for <see cref="IFileSystem"/>
    /// </summary>
    internal static class FileSystems
    {
        public static IFileSystem Default = GetFileSystem();

        private static IFileSystem GetFileSystem()
        {
#if CLR2COMPATIBILITY
            return MSBuildTaskHostFileSystem.Singleton();
#else
            if (SelectiveConditionEvaluator.NativeMethods.IsWindows)
            {
                return MSBuildOnWindowsFileSystem.Singleton();
            }
            else
            {
                return ManagedFileSystem.Singleton();
            }
#endif
        }
    }
}
