using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentFTP;

namespace Multi.IO.FTP.FluentFTP
{
    /// <summary>
    /// FluentFTP specific implementation of the <see cref="IAsyncFtpClient"/> interface.
    /// </summary>
    /// <seealso cref="Multi.IO.FTP.IAsyncFtpClient" />
    public class FluentFtpClient : IAsyncFtpClient
    {
        private readonly IFtpClient ftpClient;
        private readonly Uri baseUri;

        #region STATIC
        public static string GetValidatedServerPath(string serverPath)
        {
            if (serverPath == null)
                throw new ArgumentNullException(nameof(serverPath));
            if (string.IsNullOrWhiteSpace(serverPath))
                throw new ArgumentException("String is empty or contains whitespace characters only.", nameof(serverPath));
            return serverPath.Trim();
        }
        #endregion STATIC

        #region CONSTRUCTORS
        public FluentFtpClient(string host, NetworkCredential credentials)
        {
            this.baseUri = new Uri(host);
            this.ftpClient = new FtpClient(host, credentials);
        }

        public FluentFtpClient(string host, int port, NetworkCredential credentials)
        {
            this.baseUri = new UriBuilder(Uri.UriSchemeFtp, host, port).Uri;
            this.ftpClient = new FtpClient(host, port, credentials);
        }

        public FluentFtpClient(string host, string username, string password)
        {
            this.baseUri = new Uri(host);
            this.ftpClient = new FtpClient(host, username, password);
        }

        public FluentFtpClient(string host, int port, string username, string password)
        {
            this.baseUri = new UriBuilder(Uri.UriSchemeFtp, host, port).Uri;
            this.ftpClient = new FtpClient(host, port, username, password);
        }

        public FluentFtpClient(IFtpClient ftpClient)
        {
            this.ftpClient = ftpClient ?? throw new ArgumentNullException(nameof(ftpClient));
            this.baseUri = new Uri(ftpClient.Host);
        }
        #endregion CONSTRUCTORS

        /// <summary>
        /// Gets or sets the default download options.
        /// </summary>
        /// <value>
        /// The default download options.
        /// </value>
        public CopyOptions DefaultDownloadOptions { get; set; } = new CopyOptions() { MaxRetryCount = 3, OverwriteFiles = false, PreOccupySpace = false };

        /// <summary>
        /// Tries to connect to the FTP server.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if connected successfully</returns>
        public async Task<bool> TryConnect(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.ftpClient.ConnectAsync(cancellationToken);
            return this.ftpClient.IsConnected;
        }

        public async Task<bool> Exists(string serverPath, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.ftpClient.FileExistsAsync(serverPath, cancellationToken);
        }

        public async Task<FileMetadata> GetFileMetadata(string serverPath, CancellationToken cancellationToken = default(CancellationToken))
        {
            serverPath = GetValidatedServerPath(serverPath);

            bool exists = await this.ftpClient.FileExistsAsync(serverPath, cancellationToken);
            if (!exists)
                return null;

            var fileSize = await this.ftpClient.GetFileSizeAsync(serverPath, cancellationToken);
            var lastModifiedTime = await this.ftpClient.GetModifiedTimeAsync(serverPath, FtpDate.UTC, cancellationToken);

            var ret = new FileMetadata()
            {
                Uri = new Uri(serverPath),
                Size = new DataSize(fileSize),
                LastChangeTimeUtc = lastModifiedTime
            };

            if (this.ftpClient.HasFeature(FtpCapability.HASH))
            {
                var ftpHash = await this.ftpClient.GetHashAsync(serverPath, cancellationToken);
                if (ftpHash.IsValid && !string.IsNullOrWhiteSpace(ftpHash.Value))
                    ret.SetHashString(ftpHash.Value.Trim());
            } else if (this.ftpClient.HasFeature(FtpCapability.XMD5))
            {
                var md5 = await this.ftpClient.GetMD5Async(serverPath, cancellationToken);
                if (!string.IsNullOrWhiteSpace(md5))
                    ret.SetHashString(md5.Trim());
            }

            return ret;
        }

        public async Task<FileCopyOperationSummary> DownloadFile(string serverPath, FileInfo localDestination, CancellationToken cancellationToken = default(CancellationToken), CopyOptions downloadOptions = null)
        {
            serverPath = GetValidatedServerPath(serverPath);

            var fromUri = new Uri(this.baseUri, serverPath);
            var options = downloadOptions ?? DefaultDownloadOptions;

            // if a non-empty local file exists but we cannot overwrite files
            if (!options.OverwriteFiles && localDestination.Exists && localDestination.Length > 0)
                throw new ArgumentException($"Local file '{localDestination.FullName}' already exists!", nameof(localDestination));

            FileCopyOperationSummary ret = new FileCopyOperationSummary(fromUri, localDestination, options);
            var swTotal = Stopwatch.StartNew();
            int tryCount = 1;
            while (true)
            {
                try
                {
                    var fromMetadata = await GetFileMetadata(serverPath, cancellationToken);

                    var sw = Stopwatch.StartNew();

                    this.ftpClient.RetryAttempts = downloadOptions.MaxRetryCount;
                    var success = await this.ftpClient.DownloadFileAsync(localDestination.FullName, serverPath, FtpLocalExists.Overwrite, FtpVerify.Retry | FtpVerify.Throw, null, cancellationToken);

                } catch (Exception ex)
                {
                    if (tryCount > options.MaxRetryCount)
                    {
                        ret.Error = ex;
                        ret.
                    }
                }
            }

            return ret;
        }

        public Task<FileCopyOperationSummary> UploadFile(FileInfo localFile, string serverDestination, CancellationToken cancellationToken = default(CancellationToken))
        {
            serverDestination = GetValidatedServerPath(serverDestination);
        }

        private Uri GetServerUri(string serverPath)
        {
            return new Uri(serverPath);
        }
    }
}
