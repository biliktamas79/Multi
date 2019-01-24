using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Multi.Data
{
    using UnitOfWork;

    /// <summary>
    /// Abstract base class for entity repositories of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public abstract class EntityRepoBase<TPrimaryKey, TEntity> : IEntityRepo<TPrimaryKey, TEntity>
        where TEntity : IPkHolder<TPrimaryKey>
    {
        /// <summary>
        /// Read-only static filed for the default value of the primary key type.
        /// </summary>
        protected static TPrimaryKey PkTypeDefaultValue = default(TPrimaryKey);

        protected readonly Func<TPrimaryKey> _newPkFactory;
        protected readonly Func<TEntity, TPrimaryKey> _getEntityPk;
        protected readonly Func<TPrimaryKey, TEntity> _newEntityWithPkFactory;
        protected readonly Func<TPrimaryKey, TPrimaryKey, bool> _checkPkEquality;

        public EntityRepoBase(IUnitOfWorkBase uow, Func<TEntity, TPrimaryKey> getEntityPkDelegate, Func<TPrimaryKey, TEntity> newEntityWithPkFactory, Func<TPrimaryKey, TPrimaryKey, bool> checkPkEqualityDelegate, Func<TPrimaryKey> newPkFactory = null)
        {
            Throw.IfArgumentIsNull(uow, nameof(uow));
            Throw.IfArgumentIsNull(getEntityPkDelegate, nameof(getEntityPkDelegate));
            Throw.IfArgumentIsNull(newEntityWithPkFactory, nameof(newEntityWithPkFactory));
            Throw.IfArgumentIsNull(checkPkEqualityDelegate, nameof(checkPkEqualityDelegate));

            this.Uow = uow;
            this._getEntityPk = getEntityPkDelegate;
            this._newEntityWithPkFactory = newEntityWithPkFactory;
            this._checkPkEquality = checkPkEqualityDelegate;
            this._newPkFactory = newPkFactory;
            this.SupportsGenerateNewPk = (newPkFactory != null);
        }

        #region PROPERTIES
        /// <summary>
        /// Gets the unit of work instance this repository belongs to
        /// </summary>
        protected IUnitOfWorkBase Uow { get; private set; }
        /// <summary>
        /// Gets a boolean value indicating whether this repository supports generating new primary key values
        /// </summary>
        public bool SupportsGenerateNewPk { get; private set; }
        #endregion

        /// <summary>
        /// Generates a new primary key value.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Generating new unused primary key value is not supported!</exception>
        protected virtual TPrimaryKey GenerateNewPk()
        {
            var factory = _newPkFactory;
            if (factory == null)
                throw new NotSupportedException("Generating new unused primary key value is not supported!");

            TPrimaryKey pk;
            do
            {
                pk = factory();
            }
            while (Exists(pk));
            return pk;
        }

        #region IEntityRepo<TPrimaryKey, TEntity> members
        public TEntity New(bool initPkWithUnusedValue = false)
        {
            TPrimaryKey pk = default(TPrimaryKey);
            if (initPkWithUnusedValue)
                pk = GenerateNewPk();
            return _newEntityWithPkFactory(pk);
        }

        public IEnumerable<TEntity> New(int count, bool initPkWithUnusedValue = false)
        {
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    yield return New(initPkWithUnusedValue);
                }
            }
        }

        public virtual void Insert(TEntity entity)
        {
            Throw.IfArgumentIsNull(entity, nameof(entity));

            // if the primary key value is not set (equals the type default value)
            if (_checkPkEquality(PkTypeDefaultValue, _getEntityPk(entity)))
            {
                if (this.SupportsGenerateNewPk)
                {
                    var pk = GenerateNewPk();
                    entity.SetPk(pk);
                }
                else
                    throw new ArgumentException("The given entity does not have its primary key set and this repository does not support generating new primary key values.");
            }
            DoInsert(entity);
        }

        public abstract void Update(TEntity entity);

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    if (entity != null)
                        Update(entity);
                }
            }
        }

        public abstract void Delete(TPrimaryKey pk);

        public virtual void Delete(IEnumerable<TPrimaryKey> pks)
        {
            if (pks != null)
            {
                foreach (var pk in pks)
                {
                    //if (pk != null)
                        Delete(pk);
                }
            }
        }

        public abstract void DoInsert(TEntity entity);

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    if (entity != null)
                        Insert(entity);
                }
            }
        }

        public virtual void Delete(TEntity entity)
        {
            Throw.IfArgumentIsNull(entity, nameof(entity));

            Delete(_getEntityPk(entity));
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    if (entity != null)
                        Delete(_getEntityPk(entity));
                }
            }
        }

        public abstract void DeleteAll();

        public abstract uint Count();

        public abstract IEnumerable<TEntity> GetAll();

        public virtual bool Any(Expression<Func<TEntity, bool>> filter)
        {
            Throw.IfArgumentIsNull(filter, nameof(filter));

            return GetAll().Any(filter.Compile());
        }

        public virtual uint Count(Expression<Func<TEntity, bool>> filter)
        {
            //Throw.IfArgumentIsNull(filter, "filter");

            if (filter == null)
                return (uint)GetAll().Count();
            else
                return (uint)GetAll().Count(filter.Compile());
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, OrderBy[] orderBy = null, int skip = 0, int take = 0)
        {
            //Throw.IfArgumentIsNull(filter, "filter");

            if (filter == null)
                return GetAll();
            else
                return GetAll().Where(filter.Compile());
        }

        public virtual IEnumerable<T> Get<T>(Expression<Func<TEntity, T>> converter, Expression<Func<TEntity, bool>> filter = null, OrderBy[] orderBy = null, int skip = 0, int take = 0)
        {
            //Throw.IfArgumentIsNull(filter, "filter");
            Throw.IfArgumentIsNull(converter, nameof(converter));

            var compiledConverter = converter.Compile();
            if (filter == null)
                return GetAll().Select(compiledConverter);
            else
                return GetAll().Where(filter.Compile()).Select(compiledConverter);
        }

        public virtual IEnumerable<T> GetAll<T>(Expression<Func<TEntity, T>> converter)
        {
            Throw.IfArgumentIsNull(converter, nameof(converter));

            return GetAll().Select(converter.Compile());
        }

        public abstract bool Exists(TPrimaryKey pk);

        public abstract bool TryGetByPk(TPrimaryKey pk, out TEntity entity);

        public virtual bool AnyByPk(Expression<Func<TPrimaryKey, bool>> pkFilter)
        {
            Throw.IfArgumentIsNull(pkFilter, nameof(pkFilter));

            return GetAllPks().Any(pkFilter.Compile());
        }

        public virtual uint CountByPk(Expression<Func<TPrimaryKey, bool>> pkFilter)
        {
            if (pkFilter == null)
                return (uint)GetAll().Count();
            else
                return (uint)GetAll().Select(_getEntityPk).Count(pkFilter.Compile());
        }

        public virtual IEnumerable<TEntity> GetByPk(Expression<Func<TPrimaryKey, bool>> pkFilter = null, OrderBy[] orderBy = null, int skip = 0, int take = 0)
        {
            IEnumerable<TEntity> ret;
            if (pkFilter == null)
                ret = GetAll();
            else
                // TODO itt kéne tudni össze merge-elni a két expression-t
                //return GetAll().Select(_getEntityPkDelegate).Where(pkFilter.Compile()).Select;
                throw new NotImplementedException();

            if (orderBy != null)
            {
                // TODO
            }

            if (skip > 0)
                ret = ret.Skip(skip);

            if (take != 0)
                ret = ret.Take(take);

            return ret;
        }

        public virtual IEnumerable<T> GetByPk<T>(Expression<Func<TEntity, T>> converter, Expression<Func<TPrimaryKey, bool>> pkFilter = null, OrderBy[] orderBy = null, int skip = 0, int take = 0)
        {
            Throw.IfArgumentIsNull(converter, nameof(converter));

            var ret = GetByPk(pkFilter, orderBy, skip, take);
            if (ret == null)
                return null;
            else
                return ret.Select(converter.Compile());
        }

        public virtual IEnumerable<TPrimaryKey> GetAllPks()
        {
            return GetAll().Select(_getEntityPk);
        }

        public virtual IEnumerable<TPrimaryKey> GetPks(Expression<Func<TEntity, bool>> filter, OrderBy[] orderBy = null, int skip = 0, int take = 0)
        {
            //return Get(filter).Select(_getEntityPkExpression.Compile());
            if (filter == null)
                return GetAllPks();
            else
            {
                var ret = Get(filter, orderBy, skip, take);
                if (ret == null)
                    return null;
                else
                    return ret.Select(_getEntityPk);
            }
        }

        public virtual IEnumerable<T> GetPks<T>(Expression<Func<TPrimaryKey, T>> pkConverter, Expression<Func<TEntity, bool>> filter = null, OrderBy[] orderBy = null, int skip = 0, int take = 0)
        {
            Throw.IfArgumentIsNull(pkConverter, nameof(pkConverter));

            if (filter == null)
                return GetAllPks(pkConverter);
            else
            {
                var ret = GetPks(filter, orderBy, skip, take);
                if (ret == null)
                    return null;
                else
                    return ret.Select(pkConverter.Compile());
            }
        }

        public virtual IEnumerable<T> GetAllPks<T>(Expression<Func<TPrimaryKey, T>> pkConverter)
        {
            Throw.IfArgumentIsNull(pkConverter, nameof(pkConverter));

            return GetAllPks().Select(pkConverter.Compile());
        }
        #endregion IEntityRepo<TPrimaryKey, TEntity> members
    }
}
