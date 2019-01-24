using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    [Serializable]
    public class EntityModelRegistrationNotFoundException : EntityModelRegistrationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityModelRegistrationNotFoundException(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity model registration (EntityType='{0}') not found.", entityType.GetFriendlyTypeName());
        }

        public static new EntityModelRegistrationNotFoundException New<TEntity>()
        {
            return new EntityModelRegistrationNotFoundException(typeof(TEntity));
        }
        public static new EntityModelRegistrationNotFoundException New<TEntity>(string message)
        {
            return new EntityModelRegistrationNotFoundException(typeof(TEntity), message);
        }
        public static new EntityModelRegistrationNotFoundException New<TEntity>(Exception innerException)
        {
            return new EntityModelRegistrationNotFoundException(typeof(TEntity), innerException);
        }
        public static new EntityModelRegistrationNotFoundException New<TEntity>(string message, Exception innerException)
        {
            return new EntityModelRegistrationNotFoundException(typeof(TEntity), message, innerException);
        }

        #region CONSTRUCTORS
        protected EntityModelRegistrationNotFoundException()
            : base()
        { }

        protected EntityModelRegistrationNotFoundException(Type entityType)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationNotFoundException(entityType))
        { }

        protected EntityModelRegistrationNotFoundException(Type entityType, string message)
            : base(entityType, message)
        { }

        protected EntityModelRegistrationNotFoundException(Type entityType, string message, Exception innerException)
            : base(entityType, message, innerException)
        { }

        protected EntityModelRegistrationNotFoundException(Type entityType, Exception innerException)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationNotFoundException(entityType), innerException)
        { }

        protected EntityModelRegistrationNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        #endregion CONSTRUCTORS
    }
}