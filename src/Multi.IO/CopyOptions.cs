using System;
using System.Collections.Generic;
using System.IO;

namespace Multi.IO
{
    /// <summary>
    /// Summary information of a file copy operation.
    /// </summary>
    public class CopyOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to overwrite existing files.
        /// </summary>
        /// <value>
        ///   <c>true</c> to overwrite existing files; otherwise, <c>false</c>.
        /// </value>
        public bool OverwriteFiles { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether to occupy space for the destination file before copying.
        /// </summary>
        /// <value>
        ///   <c>true</c> to occupy space for the destination file before copying; otherwise, <c>false</c>.
        /// </value>
        public bool PreOccupySpace { get; set; } = false;
        /// <summary>
        /// Gets or sets the maximum retry count.
        /// </summary>
        /// <value>
        /// The maximum retry count.
        /// </value>
        public int MaxRetryCount { get; set; } = 3;
    }
}
