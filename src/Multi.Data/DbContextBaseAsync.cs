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
    public abstract partial class DbContextBaseAsync : DisposableBase, IOwnedByThread, IFluentInterface
    {
        /// <summary>
        /// Read-only logger instance
        /// </summary>
        public readonly ILogger Logger;

		protected DbContextBaseAsync(ILogger logger)
		{
            this.Logger = logger;
            ModelEntityRegistrationsByType = new Dictionary<Type, ModelEntityRegistrationBase>();
        }
        protected DbContextBaseAsync(int modelEntityRegistrationsInitialCapacity, ILogger logger)
        {
            this.Logger = logger;
            ModelEntityRegistrationsByType = new Dictionary<Type, ModelEntityRegistrationBase>(modelEntityRegistrationsInitialCapacity);
        }

        #region IOwnedByThread Members
        private int _ownerThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        public int OwnerThreadId
        {
            get { return _ownerThreadId; }
        }
        #endregion

        /// <summary>
        /// Initializes this database context instance.
        /// </summary>
        /// <returns></returns>
        public abstract Task Init();

        #region IsBusy
        private bool _isBusy = false;
        /// <summary>
        /// Gets a boolean value indicating that whether there are UnitOfWork instances, or not
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                //bool prev = Interlocked.CompareExchange(ref _isBusy, value, !value)
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnIsBusyChanged(value);
                }
            }
        }
        private void OnIsBusyChanged(bool isBusy)
        {
            var tmp = this.IsBusyChanged;
            if (tmp != null)
                tmp(!isBusy, isBusy);
        }
        /// <summary>
        /// Event that is triggered after the <see cref="IsBusy"/> property value changed
        /// </summary>
        public event ValueChangeDelegate<bool> IsBusyChanged;
        #endregion IsBusy

        protected override void Dispose(bool disposing)
		{
			if (disposing && !this.IsDisposed)
			{
				if (_uowDict != null)
				{
					lock (_uowDictLockObj)
					{
						if ((_uowDict != null) && (_uowDict.Count > 0))
						{
							List<KeyValuePair<int, IUnitOfWorkBaseAsync>> list = new List<KeyValuePair<int, IUnitOfWorkBaseAsync>>(_uowDict);

                            foreach (var kvp in list)
							{
								try
								{
									if (kvp.Value != null)
									{
										kvp.Value.Dispose();
									}
									_uowDict.TryRemove(kvp.Key, out var removed);
								}
								catch (Exception ex)
								{
                                    try
                                    {
                                        Logger?.LogCritical(ex, "Error disposing IUnitOfWorkBase ({uow.id}, {uow.type}) instance while disposing {type}.", kvp.Key, kvp.Value.GetType(), this.GetType());

                                        System.Diagnostics.Debug.WriteLine(string.Format("Error disposing IUnitOfWorkBase while disposing {0}!\r\n\r\n", this.GetType().GetFriendlyTypeName()) + ex.ToString(), "ERROR");
                                    }
                                    catch (Exception)
                                    {
                                        // lenyeljük
                                    }
								}
							}
							_uowDict.Clear();
						}
						_uowDict = null;
					}
				}
                this.IsBusy = false;
			}
		}
	}
}
