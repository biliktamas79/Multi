using System;
using System.Collections.Generic;
using System.Globalization;

namespace Multi.IO
{
    public static partial class Extensions
    {
        /// <summary>
        /// The key string of image metadata in file metadata custom property dictionary.
        /// </summary>
        public const string FileMetadataCustomPropertyKey_ImageMetadata = "ImageMetadata";

        /// <summary>
        /// Tries to get the image metadata of the file.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="hash">The hash string.</param>
        /// <returns>True if the metadata contains a non empty hash string.</returns>
        public static bool TryGetImageMetadata(this FileMetadata metadata, out ImageMetadata data)
        {
            return TryGetCustomPropertyValue(metadata, FileMetadataCustomPropertyKey_ImageMetadata, out data);
        }

        /// <summary>
        /// Sets the image metadata of the file.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="hash">The hash string.</param>
        public static void SetImageMetadata(this FileMetadata metadata, ImageMetadata data)
        {
            SetCustomPropertyValue(metadata, FileMetadataCustomPropertyKey_ImageMetadata, data);
        }

        /// <summary>
        /// Removes the image metadata of the file.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="hash">The hash string.</param>
        public static bool TryRemoveImageMetadata(this FileMetadata metadata)
        {
            return TryRemoveCustomProperty(metadata, FileMetadataCustomPropertyKey_ImageMetadata);
        }
    }
}
