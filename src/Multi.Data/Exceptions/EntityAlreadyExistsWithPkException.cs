using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Exception for errors thrown if an entity with the given primary key already exists.
    /// </summary>
    /// <seealso cref="Multi.Data.EntityAlreadyExistsException" />
    [Serializable]
    public class EntityAlreadyExistsWithPkException : EntityAlreadyExistsException
    {
        #region STATIC
        /// <summary>
        /// Gets the default message of entity already exists with pk exception.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="pkType">Type of the pk.</param>
        /// <param name="pk">The pk.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// entityType
        /// or
        /// pkType
        /// </exception>
        public static string GetDefaultMessageOfEntityAlreadyExistsWithPkException(Type entityType, Type pkType, object pk)
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));
            if (pkType == null)
                throw new ArgumentNullException(nameof(pkType));

            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Entity (Type='{0}') with primary key (Type='{1}'; Value='{2}') already exists.", entityType, pkType, pk);
        }

        /// <summary>
        /// Instantiates a new <see cref="EntityAlreadyExistsWithPkException"/> instance with the given primary key.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="pk">The pk.</param>
        /// <returns></returns>
        public static EntityAlreadyExistsWithPkException New<TPrimaryKey, TEntity>(TPrimaryKey pk)
        {
            return new EntityAlreadyExistsWithPkException(typeof(TEntity), typeof(TPrimaryKey), pk);
        }
        /// <summary>
        /// Instantiates a new <see cref="EntityAlreadyExistsWithPkException"/> instance with the given primary key and message.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="pk">The pk.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static EntityAlreadyExistsWithPkException New<TPrimaryKey, TEntity>(TPrimaryKey pk, string message)
        {
            return new EntityAlreadyExistsWithPkException(typeof(TEntity), typeof(TPrimaryKey), pk, message);
        }
        /// <summary>
        /// Instantiates a new <see cref="EntityAlreadyExistsWithPkException"/> instance with the given primary key and inner exception.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="pk">The pk.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <returns></returns>
        public static EntityAlreadyExistsWithPkException New<TPrimaryKey, TEntity>(TPrimaryKey pk, Exception innerException)
        {
            return new EntityAlreadyExistsWithPkException(typeof(TEntity), typeof(TPrimaryKey), pk, innerException);
        }
        /// <summary>
        /// Instantiates a new <see cref="EntityAlreadyExistsWithPkException"/> instance with the given primary key, message and inner exception.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="pk">The pk.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <returns></returns>
        public static EntityAlreadyExistsWithPkException New<TPrimaryKey, TEntity>(TPrimaryKey pk, string message, Exception innerException)
        {
            return new EntityAlreadyExistsWithPkException(typeof(TEntity), typeof(TPrimaryKey), pk, message, innerException);
        }
        #endregion STATIC

        #region CONSTRUCTORS        
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsWithPkException"/> class.
        /// </summary>
        protected EntityAlreadyExistsWithPkException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsWithPkException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="pkType">Type of the pk.</param>
        /// <param name="pk">The pk.</param>
        /// <exception cref="System.ArgumentNullException">pkType</exception>
        protected EntityAlreadyExistsWithPkException(Type entityType, Type pkType, object pk)
            : base(entityType, GetDefaultMessageOfEntityAlreadyExistsWithPkException(entityType, pkType, pk))
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");

            Init(pkType, pk);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsWithPkException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="pkType">Type of the pk.</param>
        /// <param name="pk">The pk.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">pkType</exception>
        protected EntityAlreadyExistsWithPkException(Type entityType, Type pkType, object pk, string message)
            : base(entityType, message)
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");

            Init(pkType, pk);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsWithPkException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="pkType">Type of the pk.</param>
        /// <param name="pk">The pk.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.ArgumentNullException">pkType</exception>
        protected EntityAlreadyExistsWithPkException(Type entityType, Type pkType, object pk, string message, Exception innerException)
            : base(entityType, message, innerException)
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");

            Init(pkType, pk);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsWithPkException"/> class.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="pkType">Type of the pk.</param>
        /// <param name="pk">The pk.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <exception cref="System.ArgumentNullException">pkType</exception>
        protected EntityAlreadyExistsWithPkException(Type entityType, Type pkType, object pk, Exception innerException)
            : base(entityType, GetDefaultMessageOfEntityAlreadyExistsWithPkException(entityType, pkType, pk), innerException)
        {
            if (pkType == null)
                throw new ArgumentNullException("pkType");

            Init(pkType, pk);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsWithPkException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected EntityAlreadyExistsWithPkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.PkValueString = info.GetString("PkValueString");

                string pkTypeName = info.GetString("PkType.FullName");

                try
                {
                    this.PkValue = info.GetValue("PkValue", this.PkType); // does PkType exists here?
                }
                catch (System.Runtime.Serialization.SerializationException)// sEx)
                {
                    //this.PkValue = null;
                }

                if (this.PkValue != null)
                {
                    var pkValueType = this.PkValue.GetType();
                    if (pkValueType.FullName == pkTypeName)
                    {
                        this.PkType = pkValueType;
                        return;
                    }
                }
                this.PkType = Type.GetType(pkTypeName, true);
            }
        }
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the type of the primary key of the not found entity
        /// </summary>
        public Type PkType { get; private set; }
        /// <summary>
        /// Gets the primary key value of the not found entity
        /// </summary>
        public object PkValue { get; private set; }
        /// <summary>
        /// Gets the string representing the primary key value of the not found entity
        /// </summary>
        public string PkValueString { get; private set; }
        #endregion PROPERTIES

        private void Init(Type pkType, object pk)
        {
            this.PkType = pkType;
            this.PkValue = pk;
            if (pk != null)
                this.PkValueString = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", pk);
        }

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
                if (this.PkValueString != null)
                    info.AddValue("PkValueString", this.PkValueString);

                info.AddValue("PkType.FullName", this.PkType.FullName);

                try
                {
                    info.AddValue("PkValue", this.PkValue);
                }
                catch (Exception)// ex)
                {
                    //lenyeljük
                }
            }
        }
    }
}