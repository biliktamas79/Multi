using System;
using System.Collections.Generic;
using System.Linq;
#if !PocketPC && !WindowsCE && !CF35
using System.Linq.Expressions;
#endif
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic "query by primary key" interface of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityQueryByPk<TPrimaryKey, TEntity>
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>
	{
        /// <summary>
        /// Checks whether the provided primary key already exists, or not
        /// </summary>
        /// <param name="pk">The primary key</param>
        /// <returns>Returns a boolean value indicating that whether <paramref name="pk"/> already exists, or not.</returns>
		bool Exists(TPrimaryKey pk);

        /// <summary>
        /// Tries to get the entity with the provided primary key
        /// </summary>
        /// <param name="pk">The primary key</param>
        /// <param name="entity">The found entity or null</param>
        /// <returns>Returns true if the entity is found, otherwise false.</returns>
        bool TryGet(TPrimaryKey pk, out TEntity entity);

        /// <summary>
        /// Check whether there are any entities matching the provided primary key filter
        /// </summary>
        /// <param name="pkFilter">The primary key filter</param>
        /// <returns>Returns true if there are entities matching the provided primary key filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pkFilter"/> is null.</exception>
        bool Exists(Expression<Func<TPrimaryKey, bool>> pkFilter);
        
        /// <summary>
        /// Gets the count of entities matching the provided primary key filter
        /// </summary>
        /// <param name="pkFilter">The primary key filter</param>
        /// <returns>Returns the count of entities matching the provided primary key filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pkFilter"/> is null.</exception>
        uint Count(Expression<Func<TPrimaryKey, bool>> pkFilter);

        /// <summary>
        /// Gets the entities matching the provided primary key filter
        /// </summary>
        /// <param name="pkFilter">The primary key filter</param>
        /// <returns>Returns the entities matching the provided primary key filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pkFilter"/> is null.</exception>
        IEnumerable<TEntity> Get(Expression<Func<TPrimaryKey, bool>> pkFilter);

        /// <summary>
        /// Gets the entities matching the provided primary key filter, transformed by the provided converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="pkFilter">The primary key filter</param>
        /// <param name="converter">The entity converter to the output type</param>
        /// <returns>Returns the entities matching the provided primary key filter transformed by the provided converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pkFilter"/> is null.</exception>
        IEnumerable<T> Get<T>(Expression<Func<TPrimaryKey, bool>> pkFilter, Expression<Func<TEntity, T>> converter);
	}
}
