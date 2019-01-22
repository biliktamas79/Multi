using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Multi;
using Microsoft.Extensions.Logging;

namespace Multi.Data
{
    /// <summary>
    /// Abstract base class for UnitOfWork components
    /// </summary>
    [DebuggerDisplay("UowID: {UowID,nq}, ChangeState: {ChangeState,nq}", Name = "{UowID}")]
    public abstract partial class UnitOfWorkBase : DisposableBase, IOwnedByThread, IUnitOfWorkBase, IFluentInterface
    {
		private readonly int _uowId;
        /// <summary>
        /// Read-only logger instance
        /// </summary>
        public readonly ILogger Logger;
        /// <summary>
        /// Read-only dictionary instance containing the repo instances by entity type
        /// </summary>
        protected readonly Dictionary<Type, object> ReposByType;

		#region CONSTRUCTORS
        protected UnitOfWorkBase(DbContextBase dbContext, int uowId, ILogger logger, int reposDictionaryInitialCapacity = 0)
		{
            Throw.IfArgumentIsNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
			_uowId = uowId;
            Logger = logger;

            if (reposDictionaryInitialCapacity > 0)
                this.ReposByType = new Dictionary<Type, object>(reposDictionaryInitialCapacity);
            else
                this.ReposByType = new Dictionary<Type, object>();

            this.Logger?.LogUowCreation(LogLevel.Debug, uowId);
        }
		#endregion CONSTRUCTORS

		#region PROPERTIES
        private readonly DbContextBase _dbContext;
        /// <summary>
        /// Gets the database context this unit of work belongs to
        /// </summary>
        public DbContextBase DbContext
		{
			get { return _dbContext; }
		}

        /// <summary>
        /// Gets a boolean value indicating that whether it is safe to call methods on any thread, or just on the <see cref="OwnerThreadID"/>
        /// </summary>
        public bool SupportsMultiThread { get; protected set; }
		#endregion

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

		#region IUnitOfWorkBase Members
        /// <summary>
        /// Gets the unique identifier of this unit of work instance
        /// </summary>
		public int UowId
		{
			get { return _uowId; }
		}

        /// <summary>
        /// Tries to get the repo for the entity type
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="repo">The found repo or null</param>
        /// <returns>Return true if the repo for the entity type is found, otherwise returns false.</returns>
        /// <exception cref="System.InvalidCastException">Thrown when a not null repo is found for the requested entity type, but casting it to <see cref="IEntityRepo<TEntity>"/> fails.</exception>
        public bool TryGetRepo<TEntity>(out IEntityRepo<TEntity> repo)
        {
            if (!this.SupportsMultiThread)
                this.EnsureOwnerThread();

            object o;
            if (ReposByType.TryGetValue(typeof(TEntity), out o) && (o != null))
            {
                repo = (IEntityRepo<TEntity>)o;
                return true;
            }
            else
            {
                repo = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to get the repo for the entity type with the provided primary key type
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="repo">The found repo or null</param>
        /// <returns>Return true if the repo for the entity type with the provided primary key type is found, otherwise returns false.</returns>
        /// <exception cref="System.InvalidCastException">Thrown when a not null repo is found for the requested entity type, but casting it to <see cref="IEntityRepo<TPrimaryKey, TEntity>"/> fails.</exception>
        public bool TryGetRepo<TPrimaryKey, TEntity>(out IEntityRepo<TPrimaryKey, TEntity> repo)
            where TEntity : IReadOnlyPkHolder<TPrimaryKey>
        {
            if (!this.SupportsMultiThread)
                this.EnsureOwnerThread();

            object o;
            if (ReposByType.TryGetValue(typeof(TEntity), out o) && (o != null))
            {
                repo = (IEntityRepo<TPrimaryKey, TEntity>)o;
                return true;
            }
            else
            {
                repo = null;
                return false;
            }
        }

        private UowChangeStateEnum _changeState;
        /// <summary>
        /// Gets the change state of this unit of work instance
        /// </summary>
        public UowChangeStateEnum ChangeState
        {
            get { return _changeState; }
            private set
            {
                var oldValue = _changeState;
                if (oldValue != value)
                {
                    _changeState = value;

                    this.Logger.LogUowChangeStateChanged(LogLevel.Debug, this.UowId, oldValue, value);
                }
            }
        }

        /// <summary>
        /// Lock object for synchronizing Commit() and Rollback() calls.
        /// </summary>
        protected readonly object _commitOrRollbackLockObj = new object();

        /// <summary>
        /// Commits all changes made since the beginning or the last call to Commit()
        /// </summary>
        /// <exception cref="InvalidUowChangeStateException">
        /// Commit is already running.
        /// or
        /// Cannot commit during rollback.
        /// </exception>
        public virtual void Commit()
        {
            // ha nincs semmi változás
            if (this.ChangeState == UowChangeStateEnum.NoChanges)
                return;
            ThrowIfDisposingOrDisposed();

            if (!this.SupportsMultiThread)
                this.EnsureOwnerThread();

            lock (_commitOrRollbackLockObj)
            {
                // lekérjük, miután megszereztük a lock-ot
                var currentChangeState = this.ChangeState;
                switch (currentChangeState)
                {
                    case UowChangeStateEnum.HasChanges:
                        var sw = Stopwatch.StartNew();
                        try
                        {
                            this.ChangeState = UowChangeStateEnum.Commiting;
                            this.Logger.LogUowCommiting(LogLevel.Debug, this.UowId);

                            DoCommit();

                            sw.Stop();
                            this.Logger.LogUowCommitSuccess(LogLevel.Debug, this.UowId, sw.Elapsed);
                        }
                        catch (Exception ex)
                        {
                            sw.Stop();
                            this.Logger.LogUowCommitFailed(LogLevel.Error, this.UowId, sw.Elapsed, ex);

                            this.ChangeState = UowChangeStateEnum.HasChanges;

                            throw;
                        }
                        this.ChangeState = UowChangeStateEnum.NoChanges;
                        break;
                    case UowChangeStateEnum.NoChanges: return;
                    case UowChangeStateEnum.Commiting: throw new InvalidUowChangeStateException(currentChangeState, "Commit is already running.");
                    case UowChangeStateEnum.RollingBack: throw new InvalidUowChangeStateException(currentChangeState, "Cannot commit during rollback.");
                    default:
                        throw NotSupportedValueException.New<UowChangeStateEnum>(currentChangeState);
                }
            }
        }
        /// <summary>
        /// Makes sure all changes made since the beginning or the last call to <see cref="Commit()"/> are saved to the database.
        /// </summary>
        protected abstract void DoCommit();

        /// <summary>
        /// Rolls back all changes made since the beginning or the last call to Commit()
        /// </summary>
        /// <exception cref="InvalidUowChangeStateException">
        /// Cannot rollback during commit.
        /// or
        /// Rollback is already running.
        /// </exception>
        public virtual void Rollback()
        {
            // ha nincs semmi változás
            if (this.ChangeState == UowChangeStateEnum.NoChanges)
                return;

            if (!this.IsDisposing)
            {
                ThrowIfDisposingOrDisposed();
                if (!this.IsDisposing && !this.SupportsMultiThread)
                    this.EnsureOwnerThread();
            }

            lock (_commitOrRollbackLockObj)
            {
                // lekérjük, miután megszereztük a lock-ot
                var currentChangeState = this.ChangeState;
                switch (currentChangeState)
                {
                    case UowChangeStateEnum.HasChanges:
                        var sw = Stopwatch.StartNew();
                        try
                        {
                            this.ChangeState = UowChangeStateEnum.RollingBack;
                            this.Logger.LogUowRollingBack(LogLevel.Debug, this.UowId);

                            DoRollback();

                            sw.Stop();
                            this.Logger.LogUowRollbackSuccess(LogLevel.Debug, this.UowId, sw.Elapsed);
                        }
                        catch (Exception ex)
                        {
                            sw.Stop();
                            this.Logger.LogUowRollbackFailed(LogLevel.Error, this.UowId, sw.Elapsed, ex);

                            this.ChangeState = UowChangeStateEnum.HasChanges;
                            throw;
                        }
                        this.ChangeState = UowChangeStateEnum.NoChanges;
                        break;
                    case UowChangeStateEnum.NoChanges: return;
                    case UowChangeStateEnum.Commiting: throw new InvalidUowChangeStateException(currentChangeState, "Cannot rollback during commit.");
                    case UowChangeStateEnum.RollingBack: throw new InvalidUowChangeStateException(currentChangeState, "Rollback is already running.");
                    //break;

                    default:
                        //throw new NotSupportedException(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        //    "'{0}' is a not supported '{1}' value.", currentChangeState, typeof(UowChangeStateEnum).Name));
                        throw NotSupportedValueException.New<UowChangeStateEnum>(currentChangeState);
                }
            }
        }
        /// <summary>
        /// Makes sure all changes made since the beginning or the last call to <see cref="Commit()"/> are rolled back from the database.
        /// </summary>
        protected abstract void DoRollback();

        /// <summary>
        /// Gets a value indicating whether this instance is disposing or disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposing or disposed; otherwise, <c>false</c>.
        /// </value>
        public new bool IsDisposingOrDisposed
        {
            get { return base.IsDisposingOrDisposed; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of an entity reference by the given primary key.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="pk">The primary key.</param>
        /// <returns>Returns a new instance of an entity reference by the given primary key</returns>
        public EntityReferenceByPk<TPrimaryKey, TEntity> NewEntityReferenceByPk<TPrimaryKey, TEntity>(TPrimaryKey pk)
            where TEntity : IPkHolder<TPrimaryKey>//, new()
        {
            ThrowIfDisposingOrDisposed();

            var reg = this.DbContext.GetModelEntityRegistrationBase<TEntity>(true);
            return ((ModelEntityRegistration<TPrimaryKey, TEntity>)reg).NewEntityReferenceByPkFactory(this, pk);
        }

        /// <summary>
        /// Sets the change state to has changes.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if <see cref="ChangeState"/> equals <see cref="UowChangeStateEnum.RollingBack"/> or <seealso cref="UowChangeStateEnum.Commiting"/>.
        /// </exception>
        /// <exception cref="NotSupportedValueException">
        /// Thrown if <see cref="ChangeState"/> is not supported.
        /// </exception>
        protected void SetChangeStateToHasChanges()
        {
            var cs = this.ChangeState;
            switch (cs)
            {
                case UowChangeStateEnum.NoChanges:
                    this.ChangeState = UowChangeStateEnum.HasChanges;
                    break;
                case UowChangeStateEnum.HasChanges:
                    break;
                case UowChangeStateEnum.RollingBack:
                    throw new InvalidOperationException("Cannot make changes during rollback!");
                case UowChangeStateEnum.Commiting:
                    throw new InvalidOperationException("Cannot make changes during commit!");
                default:
                    throw NotSupportedValueException.New<UowChangeStateEnum>(cs);
            }
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
                    if (this.ChangeState == UowChangeStateEnum.HasChanges)
					    Rollback();
				}
				catch (Exception ex)
				{
					System.Diagnostics.Trace.WriteLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, 
                        "Error rolling back changes while disposing {0}!{1}{2}", this.GetType().GetFriendlyTypeName(), Environment.NewLine, ex), "ERROR");
				}
			}
			//base.Dispose(disposing);
		}
	}
}
