// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Runtime.Serialization;
using System.Xml;

namespace SelectiveConditionEvaluator.msbuild.Shared.AssemblyFolders.Serialization
{
    [DataContract(Name = "AssemblyFoldersConfig", Namespace = "")]
    internal class AssemblyFolderCollection
    {
        [DataMember]
        internal List<AssemblyFolderItem> AssemblyFolders { get; set; }

        /// <summary>
        /// Deserialize the file into an AssemblyFolderCollection.
        /// </summary>
        /// <param name="filePath">Path to the AssemblyFolder.config file.</param>
        /// <returns>New deserialized collection instance.</returns>
        internal static AssemblyFolderCollection Load(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read, FileShare.Read))
            using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyFolderCollection));
                return (AssemblyFolderCollection)serializer.ReadObject(reader, true);
            }
        }
    }
}
