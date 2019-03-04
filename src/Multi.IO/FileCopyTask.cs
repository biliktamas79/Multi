using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multi.IO
{
    public class FileCopyTask : Task<FileCopyOperationSummary>
    {
        public FileCopyTask(CancellationToken cancellationToken = default(CancellationToken), CopyOptions options = null)
            : base(Execute, cancellationToken)
        {
            this.Options = options ?? new CopyOptions();
        }
        public FileCopyTask(TaskCreationOptions taskCreationOptions, CancellationToken cancellationToken = default(CancellationToken), CopyOptions options = null)
            : base(cancellationToken, taskCreationOptions)
        {
            this.Options = options ?? new CopyOptions();
        }

        public CopyOptions Options { get; private set; }
        public FileCopyOperationSummary Summary { get; private set; }

        public Uri From { get; set; }
        public Uri To { get; set; }
        public DataTransferSpeed? Speed { get; set; }

        private FileCopyOperationSummary Execute()
        {
            return null;
        }
    }
}
