﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable
using System.Diagnostics;

namespace SelectiveConditionEvaluator.msbuild.Logging;

/// <summary>
/// Console configuration of current process Console.
/// </summary>
internal class InProcessConsoleConfiguration : IConsoleConfiguration
{
    /// <summary>
    /// When set, we'll try reading background color.
    /// </summary>
    private static bool s_supportReadingBackgroundColor = true;

    public int BufferWidth => Console.BufferWidth;

    public bool AcceptAnsiColorCodes
    {
        get
        {
            bool acceptAnsiColorCodes = false;
            if (NativeMethods.IsWindows && !Console.IsOutputRedirected)
            {
                try
                {
                    IntPtr stdOut = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE);
                    if (NativeMethods.GetConsoleMode(stdOut, out uint consoleMode))
                    {
                        acceptAnsiColorCodes = (consoleMode & NativeMethods.ENABLE_VIRTUAL_TERMINAL_PROCESSING) != 0;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Assert(false, $"MSBuild client warning: problem during enabling support for VT100: {ex}.");
                }
            }
            else
            {
                // On posix OSes we expect console always supports VT100 coloring unless it is redirected
                acceptAnsiColorCodes = !Console.IsOutputRedirected;
            }

            return acceptAnsiColorCodes;
        }
    }

    public ConsoleColor BackgroundColor
    {
        get
        {
            if (s_supportReadingBackgroundColor)
            {
                try
                {
                    return Console.BackgroundColor;
                }
                catch (PlatformNotSupportedException)
                {
                    s_supportReadingBackgroundColor = false;
                }
            }

            return ConsoleColor.Black;
        }
    }

    public bool OutputIsScreen
    {
        get
        {
            bool isScreen = false;

            if (NativeMethods.IsWindows)
            {
                // Get the std out handle
                IntPtr stdHandle = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE);

                if (stdHandle != NativeMethods.InvalidHandle)
                {
                    uint fileType = NativeMethods.GetFileType(stdHandle);

                    // The std out is a char type(LPT or Console)
                    isScreen = fileType == NativeMethods.FILE_TYPE_CHAR;
                }
            }
            else
            {
                isScreen = !Console.IsOutputRedirected;
            }

            return isScreen;
        }
    }
}
