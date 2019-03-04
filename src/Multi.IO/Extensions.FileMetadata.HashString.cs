using System;
using System.Collections.Generic;
using System.Globalization;

namespace Multi.IO
{
    public static partial class Extensions
    {
        /// <summary>
        /// The key string of hash string in file metadata custom property dictionary.
        /// </summary>
        public const string FileMetadataCustomPropertyKey_HashString = "HashString";

        /// <summary>
        /// Tries to get the file hash string.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="hash">The hash string.</param>
        /// <returns>True if the metadata contains a non empty hash string.</returns>
        public static bool TryGetHashString(this FileMetadata metadata, out string hash)
        {
            return TryGetCustomPropertyString(metadata, FileMetadataCustomPropertyKey_HashString, out hash);
        }

        /// <summary>
        /// Sets the file hash string.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="hash">The hash string.</param>
        public static void SetHashString(this FileMetadata metadata, string hash)
        {
            SetCustomPropertyString(metadata, FileMetadataCustomPropertyKey_HashString, hash);
        }

        /// <summary>
        /// Removes the file hash string.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="hash">The hash string.</param>
        public static bool TryRemoveHashString(this FileMetadata metadata)
        {
            return TryRemoveCustomProperty(metadata, FileMetadataCustomPropertyKey_HashString);
        }
    }
}
