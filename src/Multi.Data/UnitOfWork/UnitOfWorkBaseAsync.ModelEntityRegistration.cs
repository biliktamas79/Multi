using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Multi.Data.UnitOfWork
{
    abstract partial class UnitOfWorkBaseAsync
	{
        //protected internal bool TryGetModelEntityRegistration<TEntity>(out ModelEntityRegistrationBase reg)
        //{
        //    return this.DbContext.TryGetModelEntityRegistration<TEntity>(out reg);
        //}
        //protected internal bool TryGetModelEntityRegistration<TPrimaryKey, TEntity>(out ModelEntityRegistration<TPrimaryKey, TEntity> reg)
        //    where TEntity : IPkHolder<TPrimaryKey>
        //{
        //    return this.DbContext.TryGetModelEntityRegistration<TPrimaryKey, TEntity>(out reg);
        //}
        //protected internal bool TryGetModelEntityRegistration<TPrimaryKey, TEntity, TdbEntity>(out ModelEntityRegistration<TPrimaryKey, TEntity, TdbEntity> reg)
        //    where TEntity : IPkHolder<TPrimaryKey>
        //    where TdbEntity : class, TEntity
        //{
        //    return this.DbContext.TryGetModelEntityRegistration<TPrimaryKey, TEntity, TdbEntity>(out reg);
        //}

        //protected internal ModelEntityRegistrationBase GetModelEntityRegistrationBase<TEntity>(bool throwIfNull)
        //{
        //    return this.DbContext.GetModelEntityRegistrationBase<TEntity>(throwIfNull);
        //}
        //protected internal ModelEntityRegistration<TPrimaryKey, TEntity> GetModelEntityRegistration<TPrimaryKey, TEntity>(bool throwIfNull)
        //    where TEntity : IPkHolder<TPrimaryKey>
        //{
        //    return this.DbContext.GetModelEntityRegistration<TPrimaryKey, TEntity>(throwIfNull);
        //}
        //protected internal ModelEntityRegistration<TPrimaryKey, TEntity, TdbEntity> GetModelEntityRegistration<TPrimaryKey, TEntity, TdbEntity>(bool throwIfNull)
        //    where TEntity : IPkHolder<TPrimaryKey>
        //    where TdbEntity : class, TEntity
        //{
        //    return this.DbContext.GetModelEntityRegistration<TPrimaryKey, TEntity, TdbEntity>(throwIfNull);
        //}
	}
}
