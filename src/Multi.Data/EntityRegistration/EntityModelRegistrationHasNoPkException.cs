using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    [Serializable]
    public class EntityModelRegistrationHasNoPkException : EntityModelRegistrationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityModelRegistrationHasNoPkException(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity model registration (EntityType='{0}') has no primary key.", entityType.GetFriendlyTypeName());
        }

        public static new EntityModelRegistrationHasNoPkException New<TEntity>()
        {
            return new EntityModelRegistrationHasNoPkException(typeof(TEntity));
        }
        public static new EntityModelRegistrationHasNoPkException New<TEntity>(string message)
        {
            return new EntityModelRegistrationHasNoPkException(typeof(TEntity), message);
        }
        public static new EntityModelRegistrationHasNoPkException New<TEntity>(Exception innerException)
        {
            return new EntityModelRegistrationHasNoPkException(typeof(TEntity), innerException);
        }
        public static new EntityModelRegistrationHasNoPkException New<TEntity>(string message, Exception innerException)
        {
            return new EntityModelRegistrationHasNoPkException(typeof(TEntity), message, innerException);
        }

        #region CONSTRUCTORS
        protected EntityModelRegistrationHasNoPkException()
            : base()
        { }

        protected EntityModelRegistrationHasNoPkException(Type entityType)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationHasNoPkException(entityType))
        { }

        protected EntityModelRegistrationHasNoPkException(Type entityType, string message)
            : base(entityType, message)
        { }

        protected EntityModelRegistrationHasNoPkException(Type entityType, string message, Exception innerException)
            : base(entityType, message, innerException)
        { }

        protected EntityModelRegistrationHasNoPkException(Type entityType, Exception innerException)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationHasNoPkException(entityType), innerException)
        { }

        protected EntityModelRegistrationHasNoPkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        #endregion CONSTRUCTORS
    }
}