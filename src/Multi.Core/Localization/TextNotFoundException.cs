using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Multi.Localization
{
    public class TextNotFoundException : ApplicationException//, ISerializable
    {
        public static string GetDefaultMessage(object textId)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Text with id='{0}' not found!", textId);
        }

        /// <summary>
        /// Initializes a new instance of the TextNotFoundException class
        /// with the specified text identifier and a system-supplied message.
        /// </summary>
        /// <param name="textId">The identifier of the not found text.</param>
        public TextNotFoundException(object textId)
            : base(GetDefaultMessage(textId))
        {
            this.TextIdObj = textId;
        }

        /// <summary>
        /// Initializes a new instance of the TextNotFoundException class
        /// with the specified text identifier and message string.
        /// </summary>
        /// <param name="textId">The identifier of the not found text.</param>
        /// <param name="message">A string that describes the exception.</param>
        public TextNotFoundException(object textId, string message)
            : base(message)
        {
            this.TextIdObj = textId;
        }

        /// <summary>
        /// Initializes a new instance of the TextNotFoundException class
        /// from serialized data.
        /// </summary>
        /// <param name="info">The object that contains the serialized data.</param>
        /// <param name="context">The stream that contains the serialized data.</param>
        /// <exception cref="System.ArgumentNullException">The info parameter is null.-or-The context parameter is null.</exception>
        [SecurityCritical]
        //[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected TextNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                string typeName = info.GetString("TextIdObj.GetType().FullName");
                if (typeName == null)
                    throw new NullReferenceException("Invalid serialization data");
                if (typeName == "null")
                    this.TextIdObj = null;
                else
                {
                    Type t = Type.GetType(typeName, true);
                    this.TextIdObj = info.GetValue("TextIdObj", t);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the TextNotFoundException class
        /// with a specified text identifier, error message and a reference to the inner exception that
        /// is the cause of this exception.
        /// </summary>
        /// <param name="textId">The identifier of the not found text.</param>
        /// <param name="message">A string that describes the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public TextNotFoundException(object textId, string message, Exception innerException)
            : base(message, innerException)
        {
            this.TextIdObj = textId;
        }

        /// <summary>
        /// Gets the identifier of the multilanguage text that was not found
        /// </summary>
        public object TextIdObj { get; private set; }

        /// <summary>
        /// Gets the <see cref="TextIdObj"/> as a string
        /// </summary>
        //[NonSerialized]
        public string TextIdString
        {
            get { return (TextIdObj == null) ? null : string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", TextIdObj); }
        }

        /// <summary>
        /// Sets the System.Runtime.Serialization.SerializationInfo object with the not found
        /// text identifier value and additional exception information.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">An object that describes the source or destination of the serialized data.</param>
        /// <exception cref="System.ArgumentNullException">The info object is null.</exception>
        [SecurityCritical]
        //[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("TextIdObj.GetType().FullName", (this.TextIdObj == null) ? "null" : this.TextIdObj.GetType().FullName);
                if (this.TextIdObj != null)
                    info.AddValue("TextIdObj", this.TextIdObj);
            }
        }
    }
}
