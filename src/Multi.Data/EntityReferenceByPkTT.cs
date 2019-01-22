using System;

namespace Multi.Data
{
    /// <summary>
    /// Generic class that represents an entity reference by primary key
    /// (This is the default <see cref="IEntityReferenceByPk{TPrimaryKey, TEntity}"/> implementation)
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public class EntityReferenceByPk<TPrimaryKey, TEntity> : IEntityReferenceByPk<TPrimaryKey, TEntity>
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>//, new()
    {
        /// <summary>
        /// Read-only field for the delegate that is used for checking equality of primary key values.
        /// </summary>
        private static Func<TPrimaryKey, TPrimaryKey, bool> CheckPkEqualityDelegate;

        /// <summary>
        /// Performs an implicit conversion from <see cref="EntityReferenceByPk{TPrimaryKey, TEntity}"/> to <see cref="TEntity"/> by loading the entity from the repository by primary key.
        /// </summary>
        /// <param name="entRef">The entity reference.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator TEntity(EntityReferenceByPk<TPrimaryKey, TEntity> entRef)
        {
            if (entRef == null)
                return default(TEntity);

            return entRef.Entity;
        }

        private WeakReference<IUnitOfWorkBase> _uowRef;
        private DbContextBase _dbCtx;
        private readonly object _lockObj = new object();
        private bool _loadEntity = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityReferenceByPk{TPrimaryKey, TEntity}"/> class.
        /// </summary>
        /// <param name="pk">The primary key value</param>
        /// <param name="uow">The UnitOfWork</param>
        public EntityReferenceByPk(TPrimaryKey pk, IUnitOfWorkBase uow)
        {
            Throw.IfArgumentIsNull(uow, "uow");

            this.Pk = pk;
            this._uowRef = new WeakReference<IUnitOfWorkBase>(uow, false);
            this._dbCtx = uow.DbContext;
            if (CheckPkEqualityDelegate == null)
                CheckPkEqualityDelegate = ((ModelEntityRegistration<TPrimaryKey, TEntity>)this._dbCtx.GetModelEntityRegistrationBase<TEntity>(true)).CheckPkEqualityDelegate;
        }

        private TPrimaryKey _pk;
        /// <summary>
        /// Gets the primary key value
        /// </summary>
        public TPrimaryKey Pk
        {
            get { return _pk; }
            protected set
            {
                // if the new primary key value is different from the old one
                if (!CheckPkEqualityDelegate(_pk, value))
                {
                    var prevPk = _pk;
                    _pk = value;
                    OnPkChanged(prevPk, value);
                }
            }
        }
        protected virtual void OnPkChanged(TPrimaryKey from, TPrimaryKey to)
        {
            if (_entity != null)
            {
                lock (_lockObj)
                {
                    if ((_entity != null) && !_entity.PkEquals(to))
                    {
                        _entity = default(TEntity);
                        _loadEntity = true;
                    }
                }
            }
        }

        private TEntity _entity;
        /// <summary>
        /// Gets the entity
        /// </summary>
        public TEntity Entity
        {
            get
            {
                if (_loadEntity)
                {
                    lock (_lockObj)
                    {
                        if (_loadEntity)
                        {
                            _entity = LoadEntityByPk(this.Pk);
                            _loadEntity = false;
                        }
                    }
                }
                return _entity;
            }
        }

        private TEntity LoadEntityByPk(TPrimaryKey pk)
        {
            // if we have a weak reference of the unit of work that created this entity reference instance
            if (this._uowRef != null)
            {
                // and its weak reference is still alive
                if (_uowRef.TryGetTarget(out IUnitOfWorkBase iuow)
                    && (iuow != null) && !iuow.IsDisposingOrDisposed)
                {
                    var uow = iuow as UnitOfWorkBase;
                    if (uow != null)
                    {
                        // ha több szálról használható, vagy ha a megfelelő szálon vagyunk
                        if (uow.SupportsMultiThread || (uow.OwnerThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId))
                        {
                            return uow.GetRepo<TPrimaryKey, TEntity>(true).GetByPk(pk, true);
                        }
                    }
                    else
                    {
                        return iuow.GetRepo<TPrimaryKey, TEntity>(true).GetByPk(pk, true);
                    }
                }
            }
            // as a last chance we initialize a new unit of work and load the entity using that
            using (var uow = this._dbCtx.InitNewUnitOfWork())
            {
                return uow.GetRepo<TPrimaryKey, TEntity>(true).GetByPk(pk, true);
            }
        }
    }
}
