using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Multi.IO
{
    /// <summary>
    /// Class representing the metadata of a file.
    /// </summary>
    public class FileMetadata
    {
        private readonly Lazy<Dictionary<string, object>> customProperties = new Lazy<Dictionary<string, object>>(true);

        /// <summary>
        /// Gets the file name without its extension.
        /// </summary>
        /// <value>
        /// The file name without extension.
        /// </value>
        [XmlIgnore]
        [IgnoreDataMember]
        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(this.Uri.LocalPath); }
        }

        /// <summary>
        /// Gets the file name extension.
        /// </summary>
        /// <value>
        /// The file name extension.
        /// </value>
        [XmlIgnore]
        [IgnoreDataMember]
        public string Extension
        {
            get { return Path.GetExtension(this.Uri.LocalPath); }
        }

        /// <summary>
        /// Gets the file name with extension.
        /// </summary>
        /// <value>
        /// The file name with extension.
        /// </value>
        [XmlIgnore]
        [IgnoreDataMember]
        public string NameWithExtension
        {
            get { return Path.GetFileName(this.Uri.LocalPath); }
        }

        /// <summary>
        /// Gets or sets the file URI.
        /// </summary>
        /// <value>
        /// The file URI.
        /// </value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        public DataSize Size { get; set; }

        /// <summary>
        /// Gets or sets the time in UTC when the file last changed.
        /// </summary>
        /// <value>
        /// The file's last change time in UTC.
        /// </value>
        public DateTime LastChangeTimeUtc { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has any custom properties.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has custom properties; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        [IgnoreDataMember]
        public bool HasCustomProperties
        {
            get { return (this.customProperties.IsValueCreated && this.customProperties.Value.Count > 0); }
        }

        /// <summary>
        /// Gets the dictionary of custom file properties.
        /// (The internal dictionary is initialized on the first call, so please always check <see cref="HasCustomProperties"/> before trying to get custom property values to avoid getting a dictionary initialized unnecessarily!)
        /// </summary>
        /// <value>
        /// The dictionary of custom file properties.
        /// </value>
        public IDictionary<string, object> CustomProperties
        {
            get { return this.customProperties.Value; }
        }
    }
}
