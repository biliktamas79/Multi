using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data
{
#if !PCL
    [Serializable]
#endif
    public class EntityRepoNotFoundException : ApplicationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityRepoNotFoundException(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity repo (EntityType='{1}') not found.", entityType);
        }

        public static EntityRepoNotFoundException New<TEntity>()
        {
            return new EntityRepoNotFoundException(typeof(TEntity));
        }
        public static EntityRepoNotFoundException New<TEntity>(string message)
        {
            return new EntityRepoNotFoundException(typeof(TEntity), message);
        }
        public static EntityRepoNotFoundException New<TEntity>(Exception innerException)
        {
            return new EntityRepoNotFoundException(typeof(TEntity), innerException);
        }
        public static EntityRepoNotFoundException New<TEntity>(string message, Exception innerException)
        {
            return new EntityRepoNotFoundException(typeof(TEntity), message, innerException);
        }

        #region CONSTRUCTORS
        protected EntityRepoNotFoundException()
        { }

        protected EntityRepoNotFoundException(Type entityType)
            : base(GetDefaultMessageOfEntityRepoNotFoundException(entityType))
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

        protected EntityRepoNotFoundException(Type entityType, string message)
            : base(message)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

        protected EntityRepoNotFoundException(Type entityType, string message, Exception innerException)
            : base(message, innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

        protected EntityRepoNotFoundException(Type entityType, Exception innerException)
            : base(GetDefaultMessageOfEntityRepoNotFoundException(entityType), innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
        }

#if !PCL
        protected EntityRepoNotFoundException(SerializationInfo info, StreamingContext context)
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
        /// Gets the type of the entity of the not found repo
        /// </summary>
        public Type EntityType { get; private set; }
        #endregion PROPERTIES

#if !PCL
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("EntityType.FullName", this.EntityType.FullName);
            }
        }
#endif
    }
}