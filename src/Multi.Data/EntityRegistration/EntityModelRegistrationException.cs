using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    [Serializable]
    public class EntityModelRegistrationException : ApplicationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityModelRegistrationException(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity model registration (EntityType='{0}') error.", entityType.GetFriendlyTypeName());
        }

        public static EntityModelRegistrationException New<TEntity>()
        {
            return new EntityModelRegistrationException(typeof(TEntity));
        }
        public static EntityModelRegistrationException New<TEntity>(string message)
        {
            return new EntityModelRegistrationException(typeof(TEntity), message);
        }
        public static EntityModelRegistrationException New<TEntity>(Exception innerException)
        {
            return new EntityModelRegistrationException(typeof(TEntity), innerException);
        }
        public static EntityModelRegistrationException New<TEntity>(string message, Exception innerException)
        {
            return new EntityModelRegistrationException(typeof(TEntity), message, innerException);
        }

        #region CONSTRUCTORS
        protected EntityModelRegistrationException()
        { }

        protected EntityModelRegistrationException(Type entityType)
            : base(GetDefaultMessageOfEntityModelRegistrationException(entityType))
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            this.EntityType = entityType;
        }

        protected EntityModelRegistrationException(Type entityType, string message)
            : base(message)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            this.EntityType = entityType;
        }

        protected EntityModelRegistrationException(Type entityType, string message, Exception innerException)
            : base(message, innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            this.EntityType = entityType;
        }

        protected EntityModelRegistrationException(Type entityType, Exception innerException)
            : base(GetDefaultMessageOfEntityModelRegistrationException(entityType), innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            this.EntityType = entityType;
        }

#if !PCL
        protected EntityModelRegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                string entityTypeName = info.GetString("EntityType.FullName");
                this.EntityType = Type.GetType(entityTypeName, true);
            }
        }
#endif
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the type of the entity whose model entity registration caused this exception
        /// </summary>
        public Type EntityType { get; private set; }
        #endregion PROPERTIES

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