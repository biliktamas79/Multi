using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace Multi.Data.UnitOfWork
{
    /// <summary>
    /// Class that manages unit of work registrations by storing them in an internal dictionary by their <see cref="IUnitOfWorkBase.UowId"/>.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.Int32, Multi.Data.UnitOfWork.IUnitOfWorkBase}}" />
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class ThreadSafeUnitOfWorkRegistry : IUnitOfWorkRegistry
    {
        /// <summary>
        /// Read-only dictionary instance containing the unit of works by their Ids
        /// </summary>
        private readonly ConcurrentDictionary<int, IUnitOfWorkBase> UowsById;

        #region CONSTRUCTORS
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeUnitOfWorkRegistry"/> class.
        /// </summary>
        public ThreadSafeUnitOfWorkRegistry()
        {
            this.UowsById = new ConcurrentDictionary<int, IUnitOfWorkBase>();
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

        #region IUnitOfWorkRegistry members
        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get { return this.UowsById.IsEmpty; }
        }

        /// <summary>
        /// Gets the count of registered unit of work instances.
        /// </summary>
        /// <value>
        /// The count of registered unit of work instances.
        /// </value>
        public int Count
        {
            get { return this.UowsById.Count; }
        }

        /// <summary>
        /// Removes all unit of work registrations.
        /// </summary>
        public void Clear()
        {
            this.UowsById.Clear();
        }

        /// <summary>
        /// Determines whether a unit of work with the given identifier is registered.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <returns>
        ///   <c>true</c> if a unit of work with the given identifier is registered; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int uowId)
        {
            return UowsById.ContainsKey(uowId);
        }

        /// <summary>
        /// Adds the given unit of work.
        /// (An error is thrown if the unit of work is already registered)
        /// </summary>
        /// <param name="reg">The unit of work registration.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="reg"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the given unit of work is already registered.</exception>
        public void Add(IUnitOfWorkBase reg)
        {
            Throw.IfArgumentIsNull(reg, nameof(reg));

            if (!UowsById.TryAdd(reg.UowId, reg))
                throw new ArgumentException($"Unit of work with Id {reg.UowId} already exists!", nameof(reg));
        }

        /// <summary>
        /// Tries to remove the unit of work with the given identifier.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <param name="removedInstance">The removed unit of work instance.</param>
        /// <returns>
        ///   <c>true</c> if a unit of work with the given identifier is successfully found and removed; otherwise, <c>false</c>.
        /// </returns>
        public bool TryRemove(int uowId, out IUnitOfWorkBase removedInstance)
        {
            return UowsById.TryRemove(uowId, out removedInstance);
        }

        /// <summary>
        /// Tries to get the unit of work with the given Id.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <param name="reg">The unit of work registered with the given Id.</param>
        /// <returns>Returns true if the unit of work for the given Id is found; otherwise false.</returns>
        public bool TryGet(int uowId, out IUnitOfWorkBase reg)
        {
            return UowsById.TryGetValue(uowId, out reg) && (reg != null);
        }

        /// <summary>
        /// Gets the unit of work with the given Id.
        /// </summary>
        /// <param name="uowId">Identifier of the unit of work.</param>
        /// <param name="throwIfNull">If set to <c>true</c> a <see cref="NullReferenceException"/> is thrown if the unit of work is not found; otherwise null is returned.</param>
        /// <returns>Returns the unit of work or null.</returns>
        /// <exception cref="NullReferenceException">Thrown if a unit of work with the given Id is not found and <paramref name="throwIfNull"/> is true.</exception>
        public IUnitOfWorkBase Get(int uowId, bool throwIfNull)
        {
            IUnitOfWorkBase ret = null;
            TryGet(uowId, out ret);
            if (throwIfNull && (ret == null))
                throw new NullReferenceException(string.Format(System.Globalization.CultureInfo.InvariantCulture
                    , "Unit of work registration (Id = {0}) not found!", uowId));
            return ret;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the unit of work registrations.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the unit of work registrations.
        /// </returns>
        public IEnumerator<KeyValuePair<int, IUnitOfWorkBase>> GetEnumerator()
        {
            return UowsById.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the unit of work registrations.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the unit of work registrations.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return UowsById.GetEnumerator();
        }
        #endregion IUnitOfWorkRegistry members
    }
}
