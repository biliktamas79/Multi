using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Multi.Data.EntityRegistration
{
    /// <summary>
    /// Class that manages model entity registrations by storing them in an internal dictionary by entity <see cref="Type"/>.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.Type, Multi.Data.EntityRegistration.ModelEntityRegistrationBase}}" />
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class ModelEntityRegistry : IEnumerable<KeyValuePair<Type, ModelEntityRegistrationBase>>, IFluentInterface
	{
        /// <summary>
        /// Read-only dictionary instance containing the model entity registrations by entity type
        /// </summary>
        private readonly Dictionary<Type, ModelEntityRegistrationBase> ModelEntityRegistrationsByType;

        #region CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelEntityRegistry"/> class.
        /// </summary>
        public ModelEntityRegistry()
        {
            this.ModelEntityRegistrationsByType = new Dictionary<Type, ModelEntityRegistrationBase>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelEntityRegistry"/> class with the given initial capacity for the internal dictionary of model entity registrations.
        /// </summary>
        /// <param name="initialCapacity">The initial capacity.</param>
        public ModelEntityRegistry(int initialCapacity)
        {
            this.ModelEntityRegistrationsByType = new Dictionary<Type, ModelEntityRegistrationBase>(initialCapacity);
        }
        #endregion CONSTRUCTORS

        /// <summary>
        /// Gets the string to display in the debugger for this instance.
        /// </summary>
        /// <value>
        /// The string to display in the debugger for this instance.
        /// </value>
        private string DebuggerDisplay
        {
            get { return $"Count = {Count}"; }
        }

        /// <summary>
        /// Clears all model entity registrations.
        /// </summary>
        public void Clear()
        {
            this.ModelEntityRegistrationsByType.Clear();
        }

        /// <summary>
        /// Determines whether the given entity type is registered.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>
        ///   <c>true</c> if the given entity type is registered; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        public bool Contains(Type entityType)
        {
            Throw.IfArgumentIsNull(entityType, nameof(entityType));

            return ModelEntityRegistrationsByType.ContainsKey(entityType);
        }

        /// <summary>
        /// Gets the count of registered model entity types.
        /// </summary>
        /// <value>
        /// The count of registered model entity types.
        /// </value>
        public int Count
        {
            get { return this.ModelEntityRegistrationsByType.Count; }
        }

        /// <summary>
        /// Determines whether the given entity type is registered.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        ///   <c>true</c> if the given entity type is registered; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        public bool Contains<TEntity>()
        {
            Type t = typeof(TEntity);
            return ModelEntityRegistrationsByType.ContainsKey(t);
        }

        /// <summary>
        /// Removes the given model entity registration if it's registered.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>
        ///   <c>true</c> if the given entity type is successfully found and removed; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        public bool Remove(Type entityType)
        {
            Throw.IfArgumentIsNull(entityType, nameof(entityType));

            return ModelEntityRegistrationsByType.Remove(entityType);
        }

        /// <summary>
        /// Removes the given model entity registration if it's registered.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        ///   <c>true</c> if the given entity type is successfully found and removed; otherwise, <c>false</c>.
        /// </returns>
        public bool Remove<TEntity>()
        {
            Type t = typeof(TEntity);
            return ModelEntityRegistrationsByType.Remove(t);
        }

        /// <summary>
        /// Adds the given model entity registration.
        /// (An error is thrown if the entity type is already registered)
        /// </summary>
        /// <param name="reg">The model entity registration.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="reg"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the given entity type is already registered.</exception>
        public void Add(ModelEntityRegistrationBase reg)
        {
            Throw.IfArgumentIsNull(reg, nameof(reg));

            ModelEntityRegistrationsByType.Add(reg.EntityType, reg);
        }

        ///// <summary>
        ///// Sets or removes the model entity registration of entity types not having primary key.
        ///// (If the given <paramref name="reg"/> is null, then the model registration for <typeparamref name="TEntity"/> gets removed.
        ///// Otherwise the given <paramref name="reg"/> gets registered, optionally overwriting the existing model registration for <typeparamref name="TEntity"/>.)
        ///// </summary>
        ///// <typeparam name="TEntity">The type of the entity.</typeparam>
        ///// <param name="reg">The model entity registration.</param>
        //public void SetOrRemove(ModelEntityRegistrationBase reg)
        //{
        //    if (reg == null)
        //        ModelEntityRegistrationsByType.Remove(reg.EntityType);
        //    else
        //        ModelEntityRegistrationsByType[reg.EntityType] = reg;
        //}

        /// <summary>
        /// Tries to get the model entity registration for the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="reg">The model entity registration.</param>
        /// <returns>Returns true if the model entity registration for the given entity type is found; otherwise false.</returns>
        public bool TryGet<TEntity>(out ModelEntityRegistrationBase reg)
        {
            Type t = typeof(TEntity);
            return ModelEntityRegistrationsByType.TryGetValue(t, out reg) && (reg != null);
        }

        /// <summary>
        /// Tries to get the model entity registration for the given entity type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="reg">The model entity registration.</param>
        /// <returns>Returns true if the model entity registration for the given entity type is found; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        public bool TryGet(Type entityType, out ModelEntityRegistrationBase reg)
        {
            Throw.IfArgumentIsNull(entityType, nameof(entityType));

            return ModelEntityRegistrationsByType.TryGetValue(entityType, out reg) && (reg != null);
        }

        /// <summary>
        /// Gets the model entity registration for the given entity type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="throwIfNull">If set to <c>true</c> a <see cref="NullReferenceException"/> is thrown if the model entity registration is not found; otherwise null is returned.</param>
        /// <returns>Returns the model entity registration or null.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        /// <exception cref="NullReferenceException">Thrown if the model entity registration is not found and <paramref name="throwIfNull"/> is true.</exception>
        public ModelEntityRegistrationBase Get(Type entityType, bool throwIfNull)
        {
            Throw.IfArgumentIsNull(entityType, nameof(entityType));

            ModelEntityRegistrationBase ret = null;
            TryGet(entityType, out ret);
            if (throwIfNull && (ret == null))
                throw new NullReferenceException(string.Format(System.Globalization.CultureInfo.InvariantCulture
                    , "Model entity registration (TEntity = {0}) not found!"
                    , entityType.GetFriendlyTypeName()
                    ));
            return ret;
        }

        /// <summary>
        /// Gets the model entity registration for the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="throwIfNull">If set to <c>true</c> a <see cref="NullReferenceException"/> is thrown if the model entity registration is not found; otherwise null is returned.</param>
        /// <returns>Returns the model entity registration or null.</returns>
        /// <exception cref="NullReferenceException">Thrown if the model entity registration is not found and <paramref name="throwIfNull"/> is true.</exception>
        public ModelEntityRegistrationBase Get<TEntity>(bool throwIfNull)
        {
            ModelEntityRegistrationBase ret = null;
            TryGet<TEntity>(out ret);
            if (throwIfNull && (ret == null))
                throw new NullReferenceException(string.Format(System.Globalization.CultureInfo.InvariantCulture
                    , "Model entity registration (TEntity = {0}) not found!"
                    , typeof(TEntity).GetFriendlyTypeName()
                    ));
            return ret;
        }

        /// <summary>
        /// Gets the model entity registration for the given entity type having primary key.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="throwIfNullOrTypeMismatch">
        /// If set to <c>true</c> 
        ///     a <see cref="EntityModelRegistrationNotFoundException"/> is thrown if the model entity registration is not found 
        ///     or a <see cref="EntityModelRegistrationHasNoPkException"/> is thrown if the found model entity registration has no primary key 
        ///     or a <see cref="EntityModelRegistrationPkTypeMismatchException"/> is thrown if the found model entity registration does not match the requested primary key type; 
        ///     or a <see cref="EntityModelRegistrationTypeMismatchException"/> is thrown if the found model entity registration does not match the requested types; 
        /// otherwise null is returned.</param>
        /// <returns>Returns the model entity registration or null.</returns>
        /// <exception cref="EntityModelRegistrationNotFoundException">Thrown if the model entity registration is not found and <paramref name="throwIfNullOrTypeMismatch"/> is true.</exception>
        /// <exception cref="EntityModelRegistrationHasNoPkException">Thrown if the found model entity registration has no primary key and <paramref name="throwIfNullOrTypeMismatch"/> is true.</exception>
        /// <exception cref="EntityModelRegistrationPkTypeMismatchException">Thrown if the found model entity registration does not match the requested primary key type and <paramref name="throwIfNullOrTypeMismatch"/> is true.</exception>
        /// <exception cref="EntityModelRegistrationTypeMismatchException">Thrown if the found model entity registration does not match the requested types and <paramref name="throwIfNullOrTypeMismatch"/> is true.</exception>
        public ModelEntityRegistration<TPrimaryKey, TEntity> Get<TPrimaryKey, TEntity>(bool throwIfNullOrTypeMismatch)
            where TEntity : IReadOnlyPkHolder<TPrimaryKey>
        {
            ModelEntityRegistrationBase foundReg = null;
            TryGet<TEntity>(out foundReg);

            // if no entity registration was found for the entity type
            if (foundReg == null)
            {
                // if we don't have to throw on errors
                if (!throwIfNullOrTypeMismatch)
                    return null;

                throw EntityModelRegistrationNotFoundException.New<TEntity>();
            }

            var ret = foundReg as ModelEntityRegistration<TPrimaryKey, TEntity>;
            // if the found registration does not match the requested types
            if (ret == null)
            {
                // if we don't have to throw on errors
                if (!throwIfNullOrTypeMismatch)
                    return null;

                var withoutPk = foundReg as ModelEntityRegistration<TEntity>;
                // if the found entity registration does not have a primary key
                if (withoutPk != null)
                {
                    throw EntityModelRegistrationHasNoPkException.New<TEntity>();
                }

                var foundRegType = foundReg.GetType();
                if (foundRegType.IsGenericType)
                {
                    var genericTypeDef = foundRegType.GetGenericTypeDefinition();

                    // if the found entity registration has a different primary key type
                    if (genericTypeDef.Equals(typeof(ModelEntityRegistration<,>)))
                    {
                        throw new EntityModelRegistrationPkTypeMismatchException(typeof(TEntity), typeof(TPrimaryKey), genericTypeDef.GetGenericArguments()[0]);
                    }
                }

                throw EntityModelRegistrationTypeMismatchException.New<TEntity>();
            }
            return ret;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the model entity registrations.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the model entity registrations.
        /// </returns>
        public IEnumerator<KeyValuePair<Type, ModelEntityRegistrationBase>> GetEnumerator()
        {
            return ModelEntityRegistrationsByType.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the model entity registrations.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the model entity registrations.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ModelEntityRegistrationsByType.GetEnumerator();
        }
    }
}
