﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Collections.Concurrent;
using SelectiveConditionEvaluator.Shared;

namespace SelectiveConditionEvaluator.Collections
{
    /// <summary>
    /// The extensions class for ConcurrentStack&lt;T&gt;
    /// </summary>
    internal static class ConcurrentStackExtensions
    {
        /// <summary>
        /// The peek method.
        /// </summary>
        /// <typeparam name="T">The type contained within the stack.</typeparam>
        public static T Peek<T>(this ConcurrentStack<T> stack) where T : class
        {
            ErrorUtilities.VerifyThrow(stack.TryPeek(out T result), "Unable to peek from stack");
            return result;
        }

        /// <summary>
        /// The pop method.
        /// </summary>
        /// <typeparam name="T">The type contained within the stack.</typeparam>
        public static T Pop<T>(this ConcurrentStack<T> stack) where T : class
        {
            ErrorUtilities.VerifyThrow(stack.TryPop(out T result), "Unable to pop from stack");
            return result;
        }
    }
}
