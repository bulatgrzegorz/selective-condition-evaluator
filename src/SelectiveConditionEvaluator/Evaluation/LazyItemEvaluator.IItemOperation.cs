// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Collections.Immutable;

namespace SelectiveConditionEvaluator.Evaluation
{
    internal partial class LazyItemEvaluator<P, I, M, D>
    {
        internal interface IItemOperation
        {
            void Apply(OrderedItemDataCollection.Builder listBuilder, ImmutableHashSet<string> globsToIgnore);
        }
    }
}
