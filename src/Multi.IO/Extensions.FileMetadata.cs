using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Multi.IO
{
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the given value with the given key into the <see cref="IFileMetadata.CustomProperties"/> dictionary.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">
        /// metadata
        /// or
        /// key
        /// </exception>
        /// <exception cref="System.ArgumentException">Thrown if <see cref="IFileMetadata.CustomProperties"/> dictionary is null.</exception>
        public static void SetCustomPropertyValue<T>(this FileMetadata metadata, string key, T value)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (metadata.CustomProperties == null)
                throw new ArgumentException($"{nameof(metadata)}.{nameof(metadata.CustomProperties)} is null.", nameof(metadata));

            metadata.CustomProperties[key] = value;
        }

        /// <summary>
        /// Tries the get a string value with the given key from the <see cref="IFileMetadata.CustomProperties"/> dictionary.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="key">The key.</param>
        /// <param name="s">The s.</param>
        /// <returns>True if <see cref="IFileMetadata.CustomProperties"/> is not null, contains a non-null value for <paramref name="key"/> that is not empty after converting to string.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// metadata
        /// or
        /// key
        /// </exception>
        public static bool TryGetCustomPropertyValue<T>(this FileMetadata metadata, string key, out T value)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (metadata.HasCustomProperties
                && metadata.CustomProperties.TryGetValue(key, out var obj)
                && (obj != null))
            {
                value = (obj is T) ? (T)obj : (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
                return (value != null);
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// Tries the get a string value with the given key from the <see cref="IFileMetadata.CustomProperties"/> dictionary.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="key">The key.</param>
        /// <param name="throwIfNotFoundOrNull">True to throw <see cref="KeyNotFoundException"/> if key not found or found but value is null.</param>
        /// <returns>True if <see cref="IFileMetadata.CustomProperties"/> is not null, contains a non-null value for <paramref name="key"/> that is not empty after converting to string.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// metadata
        /// or
        /// key
        /// </exception>
        /// <exception cref="KeyNotFoundException">Thrown if <paramref name="throwIfNotFoundOrNull"/> is true and the given <paramref name="key"/> is either not found or found but value is null.</exception>
        public static T GetCustomPropertyValue<T>(this FileMetadata metadata, string key, bool throwIfNotFoundOrNull = true)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            T ret = default(T);
            if (metadata.HasCustomProperties
                && metadata.CustomProperties.TryGetValue(key, out var obj)
                && (obj != null))
            {
                ret = (obj is T) ? (T)obj : (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
            }

            if (throwIfNotFoundOrNull && object.ReferenceEquals(ret, default(T)))
                throw new KeyNotFoundException($"'Key '{key}' not found in {nameof(FileMetadata.CustomProperties)} dictionary.");

            return ret;
        }

        /// <summary>
        /// Sets the given string value with the given key into the <see cref="IFileMetadata.CustomProperties"/> dictionary.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">
        /// metadata
        /// or
        /// key
        /// </exception>
        /// <exception cref="System.ArgumentException">Thrown if <see cref="IFileMetadata.CustomProperties"/> dictionary is null.</exception>
        public static void SetCustomPropertyString(this FileMetadata metadata, string key, string value)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (metadata.CustomProperties == null)
                throw new ArgumentException($"{nameof(metadata)}.{nameof(metadata.CustomProperties)} is null.", nameof(metadata));

            metadata.CustomProperties[key] = value;
        }

        /// <summary>
        /// Tries to get a string value with the given key from the <see cref="IFileMetadata.CustomProperties"/> dictionary.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="key">The key.</param>
        /// <param name="s">The s.</param>
        /// <returns>True if <see cref="IFileMetadata.CustomProperties"/> is not null, contains a non-null value for <paramref name="key"/> that is not empty after converting to string.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// metadata
        /// or
        /// key
        /// </exception>
        public static bool TryGetCustomPropertyString(this FileMetadata metadata, string key, out string value)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (metadata.HasCustomProperties
                && metadata.CustomProperties.TryGetValue(key, out var obj)
                && (obj != null))
            {
                value = (obj is string) ? (string)obj : Convert.ToString(obj, CultureInfo.InvariantCulture);
                return !string.IsNullOrEmpty(value);
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Tries to get a string value with the given key from the <see cref="IFileMetadata.CustomProperties"/> dictionary.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="key">The key.</param>
        /// <param name="throwIfNotFoundOrNull">True to throw <see cref="KeyNotFoundException"/> if key not found or found but value is null.</param>
        /// <returns>True if <see cref="IFileMetadata.CustomProperties"/> is not null, contains a non-null value for <paramref name="key"/> that is not empty after converting to string.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// metadata
        /// or
        /// key
        /// </exception>
        /// <exception cref="KeyNotFoundException">Thrown if <paramref name="throwIfNotFoundOrNull"/> is true and the given <paramref name="key"/> is either not found or found but value is null.</exception>
        public static string GetCustomPropertyString(this FileMetadata metadata, string key, bool throwIfNotFoundOrNull = true)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            string ret = null;
            if (metadata.HasCustomProperties
                && metadata.CustomProperties.TryGetValue(key, out var obj)
                && (obj != null))
            {
                ret = (obj is string) ? (string)obj : Convert.ToString(obj, CultureInfo.InvariantCulture);
            }

            if (throwIfNotFoundOrNull && (ret == null))
                throw new KeyNotFoundException($"'Key '{key}' not found in {nameof(FileMetadata.CustomProperties)} dictionary.");

            return ret;
        }

        /// <summary>
        /// Tries to remove the value with the given key from the <see cref="IFileMetadata.CustomProperties"/> dictionary.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="key">The key.</param>
        /// <returns>True if <see cref="IFileMetadata.CustomProperties"/> is not null, contained the <paramref name="key"/> and it got successfully removed.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// metadata
        /// or
        /// key
        /// </exception>
        public static bool TryRemoveCustomProperty(this FileMetadata metadata, string key)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return metadata.HasCustomProperties && metadata.CustomProperties.Remove(key);
        }

        /// <summary>
        /// Gets the metadata of the given file.
        /// </summary>
        /// <param name="fi">The file info object.</param>
        /// <returns>A new <see cref="FileMetadata"/> instance containing the metadata of the given file.</returns>
        public static FileMetadata GetMetadata(this FileInfo fi)
        {
            return new FileMetadata()
            {
                Uri = new Uri(Path.GetFullPath(fi.FullName)),
                Size = new DataSize(fi.Length),
                LastChangeTimeUtc = fi.LastWriteTimeUtc
            };
        }
    }
}
