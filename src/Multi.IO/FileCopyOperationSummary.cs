using System;
using System.Collections.Generic;
using System.IO;

namespace Multi.IO
{
    /// <summary>
    /// Summary information of a file copy operation.
    /// </summary>
    public class FileCopyOperationSummary
    {
        #region CONSTRUCTORS
        protected FileCopyOperationSummary()
        { }

        public FileCopyOperationSummary(FileInfo from, FileInfo to, CopyOptions options)
        {
            if (from == null)
                throw new ArgumentNullException(nameof(from));
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            this.Options = options ?? throw new ArgumentNullException(nameof(options));
            this.From = new Uri(Path.GetFullPath(from.FullName));
            this.To = new Uri(Path.GetFullPath(to.FullName));
        }

        public FileCopyOperationSummary(Uri from, FileInfo to, CopyOptions options)
        {
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            this.From = from ?? throw new ArgumentNullException(nameof(from));
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
            this.To = new Uri(Path.GetFullPath(to.FullName));
        }

        public FileCopyOperationSummary(FileInfo from, Uri to, CopyOptions options)
        {
            if (from == null)
                throw new ArgumentNullException(nameof(from));

            this.To = to ?? throw new ArgumentNullException(nameof(to));
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
            this.From = new Uri(Path.GetFullPath(from.FullName));
        }

        public FileCopyOperationSummary(Uri from, Uri to, CopyOptions options)
        {
            this.From = from ?? throw new ArgumentNullException(nameof(from));
            this.To = to ?? throw new ArgumentNullException(nameof(to));
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
        }
        #endregion CONSTRUCTORS

        /// <summary>
        /// Gets the Uri of the source file.
        /// </summary>
        /// <value>
        /// The Uri of the source file.
        /// </value>
        public Uri From { get; private set; }
        /// <summary>
        /// Gets the Uri of the destination file.
        /// </summary>
        /// <value>
        /// The Uri of the destination file.
        /// </value>
        public Uri To { get; private set; }
        /// <summary>
        /// Gets or sets the copy options used by this file copy operation.
        /// </summary>
        /// <value>
        /// The copy options used by this file copy operation.
        /// </value>
        public CopyOptions Options { get; private set; }

        /// <summary>
        /// Gets or sets the metadata of the source file.
        /// </summary>
        /// <value>
        /// The metadata of the source file.
        /// </value>
        public FileMetadata FromMetadata { get; set; }
        /// <summary>
        /// Gets or sets the metadata of the destination file.
        /// </summary>
        /// <value>
        /// The metadata of the destination file.
        /// </value>
        public FileMetadata ToMetadata { get; set; }
        /// <summary>
        /// Gets a value indicating whether the file copy was successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the file copy was successful; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; private set; }
        /// <summary>
        /// Gets or sets the error that caused this copy operation to fail.
        /// </summary>
        /// <value>
        /// The error causing this copy operation to fail.
        /// </value>
        public Exception Error { get; set; }
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan? Duration { get; set; }
        /// <summary>
        /// Gets the average speed calculated from <see cref="TransferredDataAmount"/> and <see cref="Duration"/>.
        /// </summary>
        /// <value>
        /// The average speed.
        /// </value>
        public DataTransferSpeed? AvgSpeed
        {
            get
            {
                var duration = this.Duration;
                if (duration == null)
                    return null;

                var size = this.TransferredDataAmount;
                if (size == null)
                    return null;

                return size / duration;
            }
        }
        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        /// <value>
        /// The retry count.
        /// </value>
        public int? RetryCount { get; set; }
        /// <summary>
        /// Gets or sets the amount of transferred data. This can differ from source file size (if copy was aborted or retried).
        /// </summary>
        /// <value>
        /// The amount of transferred data.
        /// </value>
        public DataSize? TransferredDataAmount { get; set; }
    }
}
