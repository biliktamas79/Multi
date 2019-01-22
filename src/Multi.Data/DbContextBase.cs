using Microsoft.Extensions.Logging;
using Multi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Multi.Data
{
    /// <summary>
    /// Abstract base class for database contexts
    /// </summary>
    public abstract partial class DbContextBase : DisposableBase, IOwnedByThread, IFluentInterface
    {
        /// <summary>
        /// Read-only logger instance
        /// </summary>
        public readonly ILogger Logger;

		protected DbContextBase(ILogger logger)
		{
            this.Logger = logger;
            ModelEntityRegistrationsByType = new Dictionary<Type, ModelEntityRegistrationBase>();

            this.Logger?.LogDbCtxCreation(LogLevel.Debug);
        }
        protected DbContextBase(int modelEntityRegistrationsInitialCapacity, ILogger logger)
        {
            this.Logger = logger;
            ModelEntityRegistrationsByType = new Dictionary<Type, ModelEntityRegistrationBase>(modelEntityRegistrationsInitialCapacity);

            this.Logger?.LogDbCtxCreation(LogLevel.Debug);
        }

        #region IOwnedByThread Members
        private int _ownerThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        /// <summary>
        /// Gets the managed thread identifier of the owner thread
        /// </summary>
        public int OwnerThreadId
        {
            get { return _ownerThreadId; }
        }
        #endregion

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public abstract void Init();

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

            this.Logger?.LogDbCtxIsBusyChanged(LogLevel.Debug, isBusy);
        }
        /// <summary>
        /// Event that is triggered after the <see cref="IsBusy"/> property value changed
        /// </summary>
        public event ValueChangeDelegate<bool> IsBusyChanged;
        #endregion IsBusy

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
		{
			if (disposing && !this.IsDisposed)
			{
                try
                {
                    if (_uowDict != null)
                    {
                        lock (_uowDictLockObj)
                        {
                            this.Logger?.LogDbCtxDisposing(LogLevel.Debug);

                            if ((_uowDict != null) && (_uowDict.Count > 0))
                            {
                                List<KeyValuePair<int, IUnitOfWorkBase>> list = new List<KeyValuePair<int, IUnitOfWorkBase>>(_uowDict);

                                foreach (var kvp in list)
                                {
                                    try
                                    {
                                        if (kvp.Value != null)
                                        {
                                            kvp.Value.Dispose();
                                        }
                                        if (_uowDict.TryRemove(kvp.Key, out var removed))
                                        {
                                            this.Logger?.LogUowRemovalFromDbCtx(LogLevel.Debug, kvp.Key);
                                        }
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

                    this.Logger?.LogDbCtxDisposeSuccess(LogLevel.Debug);
                }
                catch (Exception ctxDisposeEx)
                {
                    this.Logger?.LogDbCtxDisposeFailed(LogLevel.Error, ctxDisposeEx);
                }
            }
		}
	}
}
