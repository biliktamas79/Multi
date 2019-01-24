using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    [Serializable]
    public class EntityModelRegistrationTypeMismatchException : EntityModelRegistrationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityModelRegistrationTypeMismatchException(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity model registration (EntityType='{0}') type mismatch.", entityType.GetFriendlyTypeName());
        }

        public static new EntityModelRegistrationTypeMismatchException New<TEntity>()
        {
            return new EntityModelRegistrationTypeMismatchException(typeof(TEntity));
        }
        public static new EntityModelRegistrationTypeMismatchException New<TEntity>(string message)
        {
            return new EntityModelRegistrationTypeMismatchException(typeof(TEntity), message);
        }
        public static new EntityModelRegistrationTypeMismatchException New<TEntity>(Exception innerException)
        {
            return new EntityModelRegistrationTypeMismatchException(typeof(TEntity), innerException);
        }
        public static new EntityModelRegistrationTypeMismatchException New<TEntity>(string message, Exception innerException)
        {
            return new EntityModelRegistrationTypeMismatchException(typeof(TEntity), message, innerException);
        }

        #region CONSTRUCTORS
        protected EntityModelRegistrationTypeMismatchException()
            : base()
        { }

        protected EntityModelRegistrationTypeMismatchException(Type entityType)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationTypeMismatchException(entityType))
        { }

        protected EntityModelRegistrationTypeMismatchException(Type entityType, string message)
            : base(entityType, message)
        { }

        protected EntityModelRegistrationTypeMismatchException(Type entityType, string message, Exception innerException)
            : base(entityType, message, innerException)
        { }

        protected EntityModelRegistrationTypeMismatchException(Type entityType, Exception innerException)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationTypeMismatchException(entityType), innerException)
        { }

        protected EntityModelRegistrationTypeMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        #endregion CONSTRUCTORS
    }
}