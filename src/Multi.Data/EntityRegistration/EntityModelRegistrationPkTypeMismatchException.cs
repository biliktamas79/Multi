using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    [Serializable]
    public class EntityModelRegistrationPkTypeMismatchException : EntityModelRegistrationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityModelRegistrationPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity model registration exists for the requested entity type '{0}', but it has primary key type '{1}' not the requested '{2}'.", 
                entityType.GetFriendlyTypeName(), foundPkType.GetFriendlyTypeName(), requestedPkType.GetFriendlyTypeName());
        }

        public static EntityModelRegistrationPkTypeMismatchException New<TRequestedPrimaryKey, TEntity, TFoundPk>()
        {
            return new EntityModelRegistrationPkTypeMismatchException(typeof(TEntity), typeof(TRequestedPrimaryKey), typeof(TFoundPk));
        }
        public static EntityModelRegistrationPkTypeMismatchException New<TRequestedPrimaryKey, TEntity, TFoundPk>(string message)
        {
            return new EntityModelRegistrationPkTypeMismatchException(typeof(TEntity), typeof(TRequestedPrimaryKey), typeof(TFoundPk), message);
        }
        public static EntityModelRegistrationPkTypeMismatchException New<TRequestedPrimaryKey, TEntity, TFoundPk>(Exception innerException)
        {
            return new EntityModelRegistrationPkTypeMismatchException(typeof(TEntity), typeof(TRequestedPrimaryKey), typeof(TFoundPk), innerException);
        }
        public static EntityModelRegistrationPkTypeMismatchException New<TRequestedPrimaryKey, TEntity, TFoundPk>(string message, Exception innerException)
        {
            return new EntityModelRegistrationPkTypeMismatchException(typeof(TEntity), typeof(TRequestedPrimaryKey), typeof(TFoundPk), message, innerException);
        }

        #region CONSTRUCTORS
        protected EntityModelRegistrationPkTypeMismatchException()
            : base()
        { }

        public EntityModelRegistrationPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationPkTypeMismatchException(entityType, requestedPkType, foundPkType))
        {
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.FoundPkType = foundPkType;
        }

        public EntityModelRegistrationPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType, string message)
            : base(entityType, message)
        {
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.FoundPkType = foundPkType;
        }

        public EntityModelRegistrationPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType, string message, Exception innerException)
            : base(entityType, message, innerException)
        {
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.FoundPkType = foundPkType;
        }

        public EntityModelRegistrationPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType, Exception innerException)
            : base(entityType, GetDefaultMessageOfEntityModelRegistrationPkTypeMismatchException(entityType, requestedPkType, foundPkType), innerException)
        {
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.FoundPkType = foundPkType;
        }

        protected EntityModelRegistrationPkTypeMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                string requestedPkTypeName = info.GetString("RequestedPkType.FullName");
                this.RequestedPkType = Type.GetType(requestedPkTypeName, true);

                string foundPkTypeName = info.GetString("FoundPkType.FullName");
                this.FoundPkType = Type.GetType(foundPkTypeName, true);
            }
        }
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the requested type of the primary key of the requested entity type
        /// </summary>
        public Type RequestedPkType { get; private set; }
        /// <summary>
        /// Gets the found type of the primary key of the requested entity type
        /// </summary>
        public Type FoundPkType { get; private set; }
        #endregion PROPERTIES

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("RequestedPkType.FullName", this.RequestedPkType.FullName);
                info.AddValue("FoundPkType.FullName", this.FoundPkType.FullName);
            }
        }
    }
}