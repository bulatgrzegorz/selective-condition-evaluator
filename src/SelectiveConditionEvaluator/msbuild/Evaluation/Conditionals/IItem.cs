﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Construction;

namespace SelectiveConditionEvaluator.msbuild.Evaluation.Conditionals
{
    /// <summary>
    /// This interface represents an item without exposing its type.
    /// It's convenient to not genericise the base interface, to make it easier to use
    /// for the majority of code that doesn't call these methods.
    /// </summary>
    /// <typeparam name="M">Type of metadata object.</typeparam>
    internal interface IItem<M> : IItem
        where M : class, IMetadatum
    {
        /// <summary>
        /// Gets any existing metadatum on the item, or
        /// else any on an applicable item definition.
        /// </summary>
        M GetMetadata(string name);

        /// <summary>
        /// Sets the specified metadata.
        /// Predecessor is any preceding overridden metadata
        /// </summary>
        M SetMetadata(ProjectMetadataElement metadataElement, string evaluatedValue);

        IEnumerable<M> Metadata { get; }
    }
}
