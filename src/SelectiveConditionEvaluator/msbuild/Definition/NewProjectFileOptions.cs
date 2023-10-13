﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

namespace SelectiveConditionEvaluator.msbuild.Definition
{
    /// <summary>
    /// Flags to control options when creating a new, in memory, project.
    /// </summary>
    [Flags]
    public enum NewProjectFileOptions
    {
        /// <summary>
        /// Do not include any options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Include the XML declaration element.
        /// </summary>
        IncludeXmlDeclaration = 1,

        /// <summary>
        /// Include the ToolsVersion attribute on the Project element.
        /// </summary>
        IncludeToolsVersion = 2,

        /// <summary>
        /// Include the default MSBuild namespace on the Project element.
        /// </summary>
        IncludeXmlNamespace = 4,

        /// <summary>
        /// Include all file options.
        /// </summary>
        IncludeAllOptions = ~0
    }
}
