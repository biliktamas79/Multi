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
    public class EntityWithPkRepoNotFoundException : ApplicationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityWithPkRepoNotFoundException(Type pkType, Type entityType)
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity repo (PkType='{0}'; EntityType='{1}') not found.", pkType, entityType);
        }

        public static EntityWithPkRepoNotFoundException New<TPrimaryKey, TEntity>()
        {
            return new EntityWithPkRepoNotFoundException(typeof(TPrimaryKey), typeof(TEntity));
        }
        public static EntityWithPkRepoNotFoundException New<TPrimaryKey, TEntity>(string message)
        {
            return new EntityWithPkRepoNotFoundException(typeof(TPrimaryKey), typeof(TEntity), message);
        }
        public static EntityWithPkRepoNotFoundException New<TPrimaryKey, TEntity>(Exception innerException)
        {
            return new EntityWithPkRepoNotFoundException(typeof(TPrimaryKey), typeof(TEntity), innerException);
        }
        public static EntityWithPkRepoNotFoundException New<TPrimaryKey, TEntity>(string message, Exception innerException)
        {
            return new EntityWithPkRepoNotFoundException(typeof(TPrimaryKey), typeof(TEntity), message, innerException);
        }

        #region CONSTRUCTORS
        protected EntityWithPkRepoNotFoundException()
        { }

        protected EntityWithPkRepoNotFoundException(Type pkType, Type entityType)
            : base(GetDefaultMessageOfEntityWithPkRepoNotFoundException(pkType, entityType))
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.PkType = pkType;
            this.EntityType = entityType;
        }

        protected EntityWithPkRepoNotFoundException(Type pkType, Type entityType, string message)
            : base(message)
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.PkType = pkType;
            this.EntityType = entityType;
        }

        protected EntityWithPkRepoNotFoundException(Type pkType, Type entityType, string message, Exception innerException)
            : base(message, innerException)
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.PkType = pkType;
            this.EntityType = entityType;
        }

        protected EntityWithPkRepoNotFoundException(Type pkType, Type entityType, Exception innerException)
            : base(GetDefaultMessageOfEntityWithPkRepoNotFoundException(pkType, entityType), innerException)
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");
            if (entityType == null)
                throw new ArgumentNullException("entityType");

            this.PkType = pkType;
            this.EntityType = entityType;
        }

#if !PCL
        protected EntityWithPkRepoNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                string pkTypeName = info.GetString("PkType.FullName");
                this.PkType = Type.GetType(pkTypeName, true);

                string entityTypeName = info.GetString("EntityType.FullName");
                this.EntityType = Type.GetType(entityTypeName, true);
            }
        }
#endif
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the type of the primary key of the not found repo
        /// </summary>
        public Type PkType { get; private set; }
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
                info.AddValue("PkType.FullName", this.PkType.FullName);
                info.AddValue("EntityType.FullName", this.EntityType.FullName);
            }
        }
#endif
    }
}