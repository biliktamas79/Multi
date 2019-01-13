using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Exception for errors thrown if a method is called from a wrong thread.
    /// </summary>
    /// <seealso cref="System.ApplicationException" />
    [Serializable]
    public class WrongThreadException : ApplicationException//, ISerializable
    {
        /// <summary>
        /// Gets the default message of wrong thread exception.
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultMessageOfWrongThreadException()
        {
            return "The operation was called from the wrong thread.";
        }

        #region CONSTRUCTORS        
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongThreadException"/> class.
        /// </summary>
        public WrongThreadException()
            : base(GetDefaultMessageOfWrongThreadException())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongThreadException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public WrongThreadException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongThreadException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public WrongThreadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongThreadException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public WrongThreadException(Exception innerException)
            : base(GetDefaultMessageOfWrongThreadException(), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrongThreadException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected WrongThreadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
            }
        }
        #endregion CONSTRUCTORS

        #region PROPERTIES
        #endregion PROPERTIES

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                
            }
        }
    }
}