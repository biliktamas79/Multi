using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Exception for errors thrown if an entity already exists.
    /// </summary>
    /// <seealso cref="System.ApplicationException" />
    [Serializable]
    public class EntityAlreadyExistsException : ApplicationException
    {
        #region STATIC
        /// <summary>
        /// Gets the default message of entity already exists exception.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public static string GetDefaultMessageOfEntityAlreadyExistsException(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            return string.Format(CultureInfo.InvariantCulture, "Entity (Type='{0}') already exists.", entityType.GetFriendlyTypeName());
        }
        #endregion STATIC

        #region CONSTRUCTORS        
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        protected EntityAlreadyExistsException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityAlreadyExistsException(Type entityType)
            : base(GetDefaultMessageOfEntityAlreadyExistsException(entityType))
        {
            this.EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityAlreadyExistsException(Type entityType, string message)
            : base(message)
        {
            this.EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityAlreadyExistsException(Type entityType, string message, Exception innerException)
            : base(message, innerException)
        {
            this.EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.ArgumentNullException">entityType</exception>
        public EntityAlreadyExistsException(Type entityType, Exception innerException)
            : base(GetDefaultMessageOfEntityAlreadyExistsException(entityType), innerException)
        {
            this.EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context)
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
        /// Gets the type of the already existing entity
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