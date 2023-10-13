// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Globalization;
using System.Text;

namespace SelectiveConditionEvaluator.msbuild.Shared
{
    /// <summary>
    /// StringWriter class that allows Encoding to be specified. In the standard StringWriter
    /// class only UTF16 is allowed.
    /// </summary>
    internal sealed class EncodingStringWriter : StringWriter
    {
        /// <summary>
        /// Default ctor (Encoding = UTF8)
        /// </summary>
        public EncodingStringWriter() : this(null)
        { }

        public EncodingStringWriter(Encoding encoding) : base(CultureInfo.InvariantCulture)
        {
            Encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// Overload to specify encoding.
        /// </summary>
        public override Encoding Encoding { get; }
    }
}
