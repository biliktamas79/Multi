using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Exception for errors thrown if an entity was not found.
    /// </summary>
    /// <seealso cref="System.ApplicationException" />
    [Serializable]
    public class EntityNotFoundException : ApplicationException//, ISerializable
    {
        #region STATIC
        /// <summary>
        /// Gets the default message of entity not found exception.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public static string GetDefaultMessageOfEntityNotFoundException(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity (Type='{0}') not found.", entityType);
        }
        #endregion STATIC

        #region CONSTRUCTORS        
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        protected EntityNotFoundException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityNotFoundException(Type entityType)
            : base(GetDefaultMessageOfEntityNotFoundException(entityType))
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityNotFoundException(Type entityType, string message)
            : base(message)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityNotFoundException(Type entityType, string message, Exception innerException)
            : base(message, innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityNotFoundException(Type entityType, Exception innerException)
            : base(GetDefaultMessageOfEntityNotFoundException(entityType), innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                string entityTypeName = info.GetString("EntityType.FullName");
                this.EntityType = Type.GetType(entityTypeName, true);
            }
        }
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the type of the not found entity
        /// </summary>
        public Type EntityType { get; private set; }
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
                info.AddValue("EntityType.FullName", this.EntityType.FullName);
            }
        }
    }
}