// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using SelectiveConditionEvaluator.msbuild.Shared;

namespace SelectiveConditionEvaluator.msbuild
{
    /// <summary>
    /// Arguments for the metaproject generated event.
    /// </summary>
    [Serializable]
    internal class MetaprojectGeneratedEventArgs : BuildMessageEventArgs
    {
        /// <summary>
        /// Raw xml representing the metaproject.
        /// </summary>
        public string metaprojectXml;

        /// <summary>
        /// Initializes a new instance of the MetaprojectGeneratedEventArgs class.
        /// </summary>
        public MetaprojectGeneratedEventArgs(string metaprojectXml, string metaprojectPath, string message)
            : base(message, null, null, MessageImportance.Low, DateTime.UtcNow, metaprojectPath)
        {
            this.metaprojectXml = metaprojectXml;
            this.ProjectFile = metaprojectPath;
        }

        internal override void WriteToStream(BinaryWriter writer)
        {
            base.WriteToStream(writer);

            writer.WriteOptionalString(metaprojectXml);
        }

        internal override void CreateFromStream(BinaryReader reader, int version)
        {
            base.CreateFromStream(reader, version);

            metaprojectXml = reader.ReadOptionalString();
        }
    }
}
