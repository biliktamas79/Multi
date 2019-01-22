using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Data
{
    public static partial class Extensions
	{
        /// <summary>
        /// Gets the repo of the provided entity type, or if <paramref name="throwIfNull"/> is true then it throws <see cref="EntityRepoNotFoundException"/>, otherwise (if it's false) returns null
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="uow">The unit of work</param>
        /// <param name="throwIfNull">True to throw <see cref="EntityRepoNotFoundException"/>, false to return null if the repo of the provided entity type not found</param>
        /// <exception cref="EntityRepoNotFoundException">Thrown when <paramref name="throwIfNull"/> is true and the repo of the entity type not found</exception>
        /// <returns>The repo of the provided entity type or null</returns>
        public static IEntityRepo<TEntity> GetRepo<TEntity>(this IUnitOfWorkBase uow, bool throwIfNull)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            IEntityRepo<TEntity> repo;
            uow.TryGetRepo<TEntity>(out repo);
            if (throwIfNull && (repo == null))
                throw EntityRepoNotFoundException.New<TEntity>();
            return repo;
        }

        /// <summary>
        /// Gets the repo of the provided entity type with the provided primary key type, or if <paramref name="throwIfNull"/> is true then it throws <see cref="EntityWithPkRepoNotFoundException"/>, otherwise (if it's false) returns null
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="uow"></param>
        /// <param name="throwIfNull">True to throw <see cref="EntityWithPkRepoNotFoundException"/>, false to return null if the repo of the provided entity type with the provided primary key type not found</param>
        /// <exception cref="EntityWithPkRepoNotFoundException">Thrown when <paramref name="throwIfNull"/> is true and the repo of the entity with the provided primary key type not found</exception>
        /// <returns>The repo of the provided entity type with the provided primary key type or null</returns>
        public static IEntityRepo<TPrimaryKey, TEntity> GetRepo<TPrimaryKey, TEntity>(this IUnitOfWorkBase uow, bool throwIfNull)
            where TEntity : IReadOnlyPkHolder<TPrimaryKey>
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            IEntityRepo<TPrimaryKey, TEntity> repo;
            uow.TryGetRepo<TPrimaryKey, TEntity>(out repo);
            if (throwIfNull && (repo == null))
                throw EntityWithPkRepoNotFoundException.New<TPrimaryKey, TEntity>();
            return repo;
        }
	}
}
