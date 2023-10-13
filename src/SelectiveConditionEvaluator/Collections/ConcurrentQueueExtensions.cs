// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Collections.Concurrent;
using SelectiveConditionEvaluator.Shared;

namespace SelectiveConditionEvaluator.Collections
{
    /// <summary>
    /// The extensions class for ConcurrentQueue&lt;T&gt;
    /// </summary>
    internal static class ConcurrentQueueExtensions
    {
        /// <summary>
        /// The dequeue method.
        /// </summary>
        /// <typeparam name="T">The type contained within the queue</typeparam>
        public static T Dequeue<T>(this ConcurrentQueue<T> stack) where T : class
        {
            ErrorUtilities.VerifyThrow(stack.TryDequeue(out T result), "Unable to dequeue from queue");
            return result;
        }
    }
}
