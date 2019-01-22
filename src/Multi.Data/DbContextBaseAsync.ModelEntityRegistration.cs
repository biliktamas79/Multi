using System;
using System.Collections;
using System.Collections.Generic;

namespace Multi.Data
{
    abstract partial class DbContextBaseAsync : IEnumerable<KeyValuePair<Type, ModelEntityRegistrationBase>>
    {
        /// <summary>
        /// Read-only dictionary instance containing the model entity registrations by entity type
        /// </summary>
        protected readonly Dictionary<Type, ModelEntityRegistrationBase> ModelEntityRegistrationsByType;

        /// <summary>
        /// Sets the model entity registration.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="reg">The model entity registration.</param>
        protected void SetModelEntityRegistration<TEntity>(ModelEntityRegistration<TEntity> reg)
        {
            Type t = typeof(TEntity);
            ModelEntityRegistrationsByType[t] = reg;
        }
        /// <summary>
        /// Sets the model entity registration.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="reg">The model entity registration.</param>
        protected void SetModelEntityRegistration<TPrimaryKey, TEntity>(ModelEntityRegistration<TPrimaryKey, TEntity> reg)
            where TEntity : IPkHolder<TPrimaryKey>
        {
            Type t = typeof(TEntity);
            ModelEntityRegistrationsByType[t] = reg;
        }

        /// <summary>
        /// Tries the get the model entity registration for the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="reg">The model entity registration.</param>
        /// <returns>Returns true if the model entity registration for the given entity type is found; otherwise false.</returns>
        protected internal bool TryGetModelEntityRegistration<TEntity>(out ModelEntityRegistrationBase reg)
        {
            Type t = typeof(TEntity);
            return ModelEntityRegistrationsByType.TryGetValue(t, out reg) && (reg != null);
        }

        /// <summary>
        /// Gets the model entity registration for the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="throwIfNull">If set to <c>true</c> a <see cref="NullReferenceException"/> is thrown if the model entity registration is not found; otherwise null is returned.</param>
        /// <returns>Returns the model entity registration or null.</returns>
        /// <exception cref="NullReferenceException">Thrown if the model entity registration is not found and <paramref name="throwIfNull"/> is true.</exception>
        protected internal ModelEntityRegistrationBase GetModelEntityRegistrationBase<TEntity>(bool throwIfNull)
        {
            ModelEntityRegistrationBase ret = null;
            TryGetModelEntityRegistration<TEntity>(out ret);
            if (throwIfNull && (ret == null))
                throw new NullReferenceException(string.Format(System.Globalization.CultureInfo.InvariantCulture
                    , "Model entity registration (TEntity = {0}) not found!"
                    , typeof(TEntity).GetFriendlyTypeName()
                    ));
            return ret;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the model entity registrations of this database context.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the model entity registrations of this database context.
        /// </returns>
        public IEnumerator<KeyValuePair<Type, ModelEntityRegistrationBase>> GetEnumerator()
        {
            return ModelEntityRegistrationsByType.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the model entity registrations of this database context.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the model entity registrations of this database context.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ModelEntityRegistrationsByType.GetEnumerator();
        }
    }
}
