using System;
using System.Collections;
using System.Collections.Generic;

namespace Multi.Data.UnitOfWork
{
    /// <summary>
    /// Interface of unit of work registration handlers that store them in an internal dictionary by their <see cref="IUnitOfWorkBase.UowId"/>.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.Int32, Multi.Data.UnitOfWork.IUnitOfWorkBase}}" />
    public interface IUnitOfWorkRegistry : IEnumerable<KeyValuePair<int, IUnitOfWorkBase>>, IFluentInterface
	{
        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets the count of registered unit of work instances.
        /// </summary>
        /// <value>
        /// The count of registered unit of work instances.
        /// </value>
        int Count { get; }

        /// <summary>
        /// Removes all unit of work registrations.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether a unit of work with the given identifier is registered.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <returns>
        ///   <c>true</c> if a unit of work with the given identifier is registered; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(int uowId);

        /// <summary>
        /// Adds the given unit of work.
        /// (An error is thrown if the unit of work is already registered)
        /// </summary>
        /// <param name="reg">The unit of work registration.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="reg"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the given unit of work is already registered.</exception>
        void Add(IUnitOfWorkBase reg);

        /// <summary>
        /// Tries to remove the unit of work with the given identifier.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <param name="removedInstance">The removed unit of work instance.</param>
        /// <returns>
        ///   <c>true</c> if a unit of work with the given identifier is successfully found and removed; otherwise, <c>false</c>.
        /// </returns>
        bool TryRemove(int uowId, out IUnitOfWorkBase removedInstance);

        /// <summary>
        /// Tries to get the unit of work with the given Id.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <param name="reg">The unit of work registered with the given Id.</param>
        /// <returns>Returns true if the unit of work for the given Id is found; otherwise false.</returns>
        bool TryGet(int uowId, out IUnitOfWorkBase reg);

        /// <summary>
        /// Gets the unit of work with the given Id.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <param name="throwIfNull">If set to <c>true</c> a <see cref="NullReferenceException"/> is thrown if the unit of work is not found; otherwise null is returned.</param>
        /// <returns>Returns the unit of work or null.</returns>
        /// <exception cref="NullReferenceException">Thrown if a unit of work with the given Id is not found and <paramref name="throwIfNull"/> is true.</exception>
        IUnitOfWorkBase Get(int uowId, bool throwIfNull);
    }
}
