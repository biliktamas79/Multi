using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Data
{
    public static partial class Extensions
	{
        /// <summary>
        /// Gets the type of the primary key of the entities
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of primary key</typeparam>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="repo">The entity repo</param>
        /// <returns>Returns the type of the primary key.</returns>
        public static Type GetPkType<TPrimaryKey, TEntity>(this IEntityQueryByPk<TPrimaryKey, TEntity> repo)
            where TEntity : IReadOnlyPkHolder<TPrimaryKey>
        {
            return typeof(TPrimaryKey);
        }

        /// <summary>
        /// Gets the entity with the provided primary key, or if <paramref name="throwIfNotFound"/> is true then it throws <see cref="EntityNotFoundByPkException"/>, otherwise (if it's false) returns null
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of primary key</typeparam>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="repo">The entity repo</param>
        /// <param name="pk">The primary key</param>
        /// <param name="throwIfNotFound">True to throw <see cref="EntityNotFoundByPkException"/>, false to return null if the entity with the provided primary key not found</param>
        /// <exception cref="EntityNotFoundByPkException">Thrown when <paramref name="throwIfNotFound"/> is true and the entity with the provided primary key not found</exception>
        /// <returns>The entity with the provided primary key or null</returns>
        public static TEntity GetByPk<TPrimaryKey, TEntity>(this IEntityQueryByPk<TPrimaryKey, TEntity> repo, TPrimaryKey pk, bool throwIfNotFound)
            where TEntity : IReadOnlyPkHolder<TPrimaryKey>
        {
            if (repo == null)
                throw new ArgumentNullException(nameof(repo));

            TEntity entity;
            repo.TryGetByPk(pk, out entity);
            if (throwIfNotFound && (entity == null))
                throw EntityNotFoundByPkException.New<TPrimaryKey, TEntity>(pk);
            return entity;
        }
	}
}
