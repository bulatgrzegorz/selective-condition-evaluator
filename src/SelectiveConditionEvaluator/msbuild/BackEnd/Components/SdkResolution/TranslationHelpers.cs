﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Sdk;

namespace SelectiveConditionEvaluator.msbuild.BackEnd.Components.SdkResolution
{
    internal static class SdkResultTranslationHelpers
    {
        public static void Translate(this ITranslator t, ref SdkReference sdkReference)
        {
            string name = null;
            string version = null;
            string minimumVersion = null;

            if (t.Mode == TranslationDirection.WriteToStream)
            {
                name = sdkReference.Name;
                version = sdkReference.Version;
                minimumVersion = sdkReference.MinimumVersion;
            }

            t.Translate(ref name);
            t.Translate(ref version);
            t.Translate(ref minimumVersion);

            if (t.Mode == TranslationDirection.ReadFromStream)
            {
                sdkReference = new SdkReference(name, version, minimumVersion);
            }
        }

        public static void Translate(this ITranslator t, ref SdkResultItem item)
        {
            string itemSpec = null;
            Dictionary<string, string> metadata = null;

            if (t.Mode == TranslationDirection.WriteToStream)
            {
                itemSpec = item.ItemSpec;
                metadata = item.Metadata;
            }

            t.Translate(ref itemSpec);
            t.TranslateDictionary(ref metadata, StringComparer.InvariantCultureIgnoreCase);

            if (t.Mode == TranslationDirection.ReadFromStream)
            {
                item = new SdkResultItem(itemSpec, metadata);
            }
        }
    }
}
