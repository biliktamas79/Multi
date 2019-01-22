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
    public class EntityRepoPkTypeMismatchException : ApplicationException//, ISerializable
    {
        public static string GetDefaultMessageOfEntityRepoPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Entity repo exists for the requested entity type '{0}', but it has primary key type '{1}' not the requested '{2}'.", entityType, foundPkType, requestedPkType);
        }

        //public static EntityRepoPkTypeMismatchException New<TRequestedPrimaryKey, TEntity, TFoundPk>()
        //{
        //    return new EntityRepoPkTypeMismatchException(typeof(TPrimaryKey), typeof(TEntity));
        //}
        //public static EntityRepoPkTypeMismatchException New<TPrimaryKey, TEntity>(string message)
        //{
        //    return new EntityRepoPkTypeMismatchException(typeof(TPrimaryKey), typeof(TEntity), message);
        //}
        //public static EntityRepoPkTypeMismatchException New<TPrimaryKey, TEntity>(Exception innerException)
        //{
        //    return new EntityRepoPkTypeMismatchException(typeof(TPrimaryKey), typeof(TEntity), innerException);
        //}
        //public static EntityRepoPkTypeMismatchException New<TPrimaryKey, TEntity>(string message, Exception innerException)
        //{
        //    return new EntityRepoPkTypeMismatchException(typeof(TPrimaryKey), typeof(TEntity), message, innerException);
        //}

        #region CONSTRUCTORS
        protected EntityRepoPkTypeMismatchException()
        { }

        public EntityRepoPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType)
            : base(GetDefaultMessageOfEntityRepoPkTypeMismatchException(entityType, requestedPkType, foundPkType))
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.EntityType = entityType;
            this.FoundPkType = foundPkType;
        }

        public EntityRepoPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType, string message)
            : base(message)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.EntityType = entityType;
            this.FoundPkType = foundPkType;
        }

        public EntityRepoPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType, string message, Exception innerException)
            : base(message, innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.EntityType = entityType;
            this.FoundPkType = foundPkType;
        }

        public EntityRepoPkTypeMismatchException(Type entityType, Type requestedPkType, Type foundPkType, Exception innerException)
            : base(GetDefaultMessageOfEntityRepoPkTypeMismatchException(entityType, requestedPkType, foundPkType), innerException)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");
            if (requestedPkType == null)
                throw new ArgumentNullException("requestedPkType");
            if (foundPkType == null)
                throw new ArgumentNullException("foundPkType");

            this.RequestedPkType = requestedPkType;
            this.EntityType = entityType;
            this.FoundPkType = foundPkType;
        }

#if !PCL
        protected EntityRepoPkTypeMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                string requestedPkTypeName = info.GetString("RequestedPkType.FullName");
                this.RequestedPkType = Type.GetType(requestedPkTypeName, true);

                string entityTypeName = info.GetString("EntityType.FullName");
                this.EntityType = Type.GetType(entityTypeName, true);

                string foundPkTypeName = info.GetString("FoundPkType.FullName");
                this.FoundPkType = Type.GetType(foundPkTypeName, true);
            }
        }
#endif
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the type of the entity
        /// </summary>
        public Type EntityType { get; private set; }
        /// <summary>
        /// Gets the requested type of the primary key of the requested entity type
        /// </summary>
        public Type RequestedPkType { get; private set; }
        /// <summary>
        /// Gets the found type of the primary key of the requested entity type
        /// </summary>
        public Type FoundPkType { get; private set; }
        #endregion PROPERTIES

#if !PCL
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("RequestedPkType.FullName", this.RequestedPkType.FullName);
                info.AddValue("EntityType.FullName", this.EntityType.FullName);
                info.AddValue("FoundPkType.FullName", this.FoundPkType.FullName);
            }
        }
#endif
    }
}