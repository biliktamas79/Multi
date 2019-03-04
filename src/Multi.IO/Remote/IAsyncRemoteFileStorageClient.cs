using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Multi.IO.Remote
{
    /// <summary>
    /// Interface of asynchronous remote file storage clients;
    /// </summary>
    public interface IAsyncRemoteFileStorageClient
    {
        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        bool IsConnected { get; }

        /// <summary>
        /// Tries to connect to the FTP server.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if connected successfully</returns>
        Task<bool> TryConnect(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks whether the given path exists at the server.
        /// </summary>
        /// <param name="serverPath">The path on the server.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the path exists at the server.</returns>
        Task<bool> Exists(string serverPath, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the metadata of the file at the given path on the server.
        /// </summary>
        /// <param name="serverPath">The path on the server.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The metadata of the file on the server or null if the path was not found.</returns>
        Task<FileMetadata> GetFileMetadata(string serverPath, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Downloads the file at the given path on the server to the given local file.
        /// </summary>
        /// <param name="serverPath">The path on the server of the source file.</param>
        /// <param name="localDestination">The local destination file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The summary of the file download.</returns>
        Task<FileCopyOperationSummary> DownloadFile(string serverPath, FileInfo localDestination, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Uploads the given local file to the given path on the server.
        /// </summary>
        /// <param name="localFile">The local file to upload.</param>
        /// <param name="serverDestination">The path on the server of the destination file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The summary of the file upload.</returns>
        Task<FileCopyOperationSummary> UploadFile(FileInfo localFile, string serverDestination, CancellationToken cancellationToken = default(CancellationToken));
    }
}
