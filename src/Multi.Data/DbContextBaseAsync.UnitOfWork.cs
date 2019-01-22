using Microsoft.Extensions.Logging;
using Multi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multi.Data
{
    /// <summary>
    /// Abstract base class for async database contexts
    /// </summary>
    public abstract partial class DbContextBaseAsync
    {
		#region PROPERTIES
		protected readonly object _uowDictLockObj = new object();
		private ConcurrentDictionary<int, IUnitOfWorkBaseAsync> _uowDict = null;
        /// <summary>
        /// Gets the unit of work dictionary.
        /// </summary>
        /// <value>
        /// The uow dictionary.
        /// </value>
        protected ConcurrentDictionary<int, IUnitOfWorkBaseAsync> UowDict
		{
			get
			{
				if (_uowDict == null)
				{
					lock (_uowDictLockObj)
					{
						// még egy ellenőrzés biztos, ami biztos
						if (_uowDict == null)
						{
							_uowDict = new ConcurrentDictionary<int, IUnitOfWorkBaseAsync>();
						}
					}
				}
				return _uowDict;
			}
		}
		
        /// <summary>
        /// Gets the count of registered unit of work instances.
        /// </summary>
        /// <value>
        /// The unit of work count.
        /// </value>
        public int UnitOfWorkCount
		{
			get
			{
				var dict = _uowDict;
				return (dict == null) ? 0 : dict.Count;
			}
		}
        #endregion PROPERTIES

        /// <summary>
        /// Determines whether the unit of work with the given identifier is registered.
        /// </summary>
        /// <param name="uowID">The unit of work identifier.</param>
        /// <returns>
        ///   <c>true</c> if a unit of work instance with the given identifier is registered; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsUnitOfWork(int uowID)
		{
			var dict = _uowDict;
			return (dict != null) && dict.ContainsKey(uowID);
		}

        /// <summary>
        /// Initializes a new UnitOfWork instance
        /// </summary>
        /// <returns>The new UnitOfWork instance</returns>
        public IUnitOfWorkBaseAsync InitNewUnitOfWork()
        {
            ThrowIfDisposingOrDisposed();

            int newUowID = GetNextFreeUowID();
            var uow = InitNewUnitOfWork(newUowID);
            if (uow == null)
                Logger?.LogError("InitNewUnitOfWork from {type} returned null.", this.GetType());
            else
                Logger?.LogDebug("New UnitOfWork ({uow.id}, {uow.type}) initialized by {dbCtx.type}.", uow.UowId, uow.GetType(), this.GetType());
            RegisterUnitOfWork(uow);

            return uow;
        }
        /// <summary>
        /// Initializes a new UnitOfWork instance with the provided identifier
        /// </summary>
        /// <param name="uowID"></param>
        /// <returns></returns>
        protected abstract IUnitOfWorkBaseAsync InitNewUnitOfWork(int uowID);

        /// <summary>
        /// Executes the provided delegate using a new UnitOfWork
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="action">The action delegate to execute inside a UnitOfWork</param>
        public async Task ExecuteInUow(CancellationToken cancellationToken, Func<IUnitOfWorkBaseAsync, CancellationToken, Task> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            ThrowIfDisposingOrDisposed();

            cancellationToken.ThrowIfCancellationRequested();
            using (var uow = InitNewUnitOfWork())
            {
                await action(uow, cancellationToken).ConfigureAwait(false);
            }
        }

        private int _lastUowID = 0;
        private int GetNextFreeUowID()
        {
            int newUowID;
            do { newUowID = Interlocked.Increment(ref _lastUowID); }
            while (ContainsUnitOfWork(newUowID));
            return newUowID;
        }

		private void RegisterUnitOfWork(IUnitOfWorkBaseAsync uow)
		{
			if (uow == null)
				throw new ArgumentNullException(nameof(uow));

            Logger?.LogTrace("Registering UnitOfWork ({uow.id}, {uow.type}) in {dbCtx.type}...", uow.UowId, uow.GetType(), this.GetType());

            ThrowIfDisposingOrDisposed();
			lock (_uowDictLockObj)
			{
                if (!this.UowDict.TryAdd(uow.UowId, uow))
                    throw new ArgumentException($"Unit of work with Id {uow.UowId} already exists!", nameof(uow));

                OnUnitOfWorkAdded(uow);
                this.IsBusy = (_uowDict.Count > 0);
			}
		}

        /// <summary>
        /// Unregisters the provided UnitOfWork instance
        /// </summary>
        /// <param name="uow"></param>
		protected internal void UnregisterUnitOfWork(IUnitOfWorkBaseAsync uow)
		{
			if (uow == null)
				throw new ArgumentNullException(nameof(uow));

            Logger?.LogTrace("Unregistering UnitOfWork ({uow.id}, {uow.type}) in {dbCtx.type}...", uow.UowId, uow.GetType(), this.GetType());

            //ThrowIfDisposingOrDisposed();
            if (_uowDict != null)
			{
				lock (_uowDictLockObj)
				{
					// még egy ellenőrzés biztos, ami biztos
					if (_uowDict != null)
					{
                        Logger?.LogTrace("Removing UnitOfWork ({uow.id}, {uow.type}) from {dbCtx.type}...", uow.UowId, uow.GetType(), this.GetType());

                        if (_uowDict.TryRemove(uow.UowId, out var removedUow))
                        {
                            if (!object.ReferenceEquals(uow, removedUow))
                            {
                                throw new ArgumentException($"Unit of work instance removed by Id {uow.UowId} not equals the given instance!");
                            }

                            Logger?.LogTrace("UnitOfWork ({uow.id}, {uow.type}) removed from {dbCtx.type}.", uow.UowId, uow.GetType(), this.GetType());

                            OnUnitOfWorkRemoved(uow);
                            this.IsBusy = (_uowDict.Count > 0);
                        }
					}
				}
			}
		}

        /// <summary>
        /// Method that is called after a UnitOfWork is removed 
        /// </summary>
        /// <param name="uow">The removed UnitOfWork instance</param>
        protected virtual void OnUnitOfWorkRemoved(IUnitOfWorkBaseAsync uow)
        {
            
        }

        /// <summary>
        /// Method that is called after a UnitOfWork is added
        /// </summary>
        /// <param name="uow">The added UnitOfWork instance</param>
        protected virtual void OnUnitOfWorkAdded(IUnitOfWorkBaseAsync uow)
        {
            
        }
	}
}
