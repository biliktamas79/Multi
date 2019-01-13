using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic "primary key query" interface of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public interface IEntityQuery<TPrimaryKey, TEntity> : IEntityQuery<TEntity>
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>
	{
        /// <summary>
        /// Gets all primary keys
        /// </summary>
        /// <returns>Returns an enumerator of all primary keys of this type</returns>
        IEnumerable<TPrimaryKey> GetAllPks();

        /// <summary>
        /// Gets the primary keys of entities matching the provided entity filter
        /// </summary>
        /// <param name="filter">The entity filter</param>
        /// <returns>Returns the primary keys of entities matching the provided entity filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> is null.</exception>
        IEnumerable<TPrimaryKey> GetPks(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the primary keys of entities matching the provided entity filter transformed by the provided primary key converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="filter">The entity filter</param>
        /// <param name="pkConverter">The primary key converter to the output type</param>
        /// <returns>Returns the primary keys of entities matching the provided entity filter transformed by the provided primary key converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filter"/> or <paramref name="pkConverter"/> is null.</exception>
        IEnumerable<T> GetPks<T>(Expression<Func<TEntity, bool>> filter, Expression<Func<TPrimaryKey, T>> pkConverter);

        /// <summary>
        /// Gets all primary keys transformed by the provided primary key converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="pkConverter">The primary key converter to the output type</param>
        /// <returns>Returns all primary keys transformed by the provided primary key converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pkConverter"/> is null.</exception>
        IEnumerable<T> GetAllPks<T>(Expression<Func<TPrimaryKey, T>> pkConverter);
	}
}
