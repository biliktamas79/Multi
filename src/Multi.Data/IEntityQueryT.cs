using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic query interface of entity types
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityQuery<TEntity>
	{
        /// <summary>
        /// Gets the total count of entities
        /// </summary>
        /// <returns>Returns the total count of entities</returns>
        uint Count();
        
        /// <summary>
        /// Gets all entities of this type
        /// </summary>
        /// <returns>Returns an enumerator of all entities of this type</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Checks whether there are any entities matching the provided entity filter
        /// </summary>
        /// <param name="filter">The entity filter</param>
        /// <returns>Returns true if there is at least one entity matching the provided entity filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is null.</exception>
		bool Exists(Expression<Func<TEntity, bool>> filter);
        
        /// <summary>
        /// Gets the count of entities matching the provided entity filter
        /// </summary>
        /// <param name="filter">The entity filter</param>
        /// <returns>Returns the count of entities matching the provided entity filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is null.</exception>
        uint Count(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the entities matching the provided entity filter
        /// </summary>
        /// <param name="filter">The entity filter</param>
        /// <returns>Returns the entities matching the provided entity filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is null.</exception>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the entities matching the provided entity filter, transformed by the provided converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="filter">The entity filter</param>
        /// <param name="converter">The entity converter to the output type</param>
        /// <returns>Returns the entities matching the provided entity filter transformed by the provided converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> or <paramref name="converter"/> is null.</exception>
        IEnumerable<T> Get<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, T>> converter);

        /// <summary>
        /// Gets all entities transformed by the provided converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="converter">The entity converter to the output type</param>
        /// <returns>Returns all entities transformed by the provided converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is null.</exception>
        IEnumerable<T> GetAll<T>(Expression<Func<TEntity, T>> converter);
	}
}
