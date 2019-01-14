﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Multi.Data
{
    /// <summary>
    /// Asynchronous basic generic query interface of entity types
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityQueryAsync<TEntity>
	{
        /// <summary>
        /// Checks whether there are any entities matching the provided optional entity filter
        /// </summary>
        /// <param name="filter">The optional entity filter</param>
        /// <returns>Returns true if there is at least one entity matching the provided entity filter</returns>
		Task<bool> Any(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the count of entities matching the provided optional entity filter
        /// </summary>
        /// <param name="filter">The optional entity filter</param>
        /// <returns>Returns the count of entities matching the provided entity filter</returns>
        Task<uint> Count(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Gets the entities matching the provided optional entity filter, ordering and paging values
        /// </summary>
        /// <param name="filter">The optional entity filter</param>
        /// <param name="orderBy">The optional ordering of the matching records.</param>
        /// <param name="skip">The count of entities to skip. (Default = 0)</param>
        /// <param name="take">The count of entities to take. (Default = -1, meaning all entities)</param>
        /// <returns>Returns an enumerator of the result entities</returns>
        IEnumerable<Task<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, OrderBy[] orderBy = null, uint skip = 0, uint take = 0);

        /// <summary>
        /// Gets the entities matching the provided optional entity filter, ordering and paging values, transformed by the provided converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="converter">The entity converter to the output type</param>
        /// <param name="filter">The optional entity filter</param>
        /// <param name="orderBy">The optional ordering of the matching records.</param>
        /// <param name="skip">The count of entities to skip. (Default = 0)</param>
        /// <param name="take">The count of entities to take. (Default = -1, meaning all entities)</param>
        /// <returns>Returns an enumerator of the result entities transformed by the provided converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="converter"/> is null.</exception>
        IEnumerable<Task<T>> Get<T>(Expression<Func<TEntity, T>> converter, Expression<Func<TEntity, bool>> filter = null, OrderBy[] orderBy = null, uint skip = 0, uint take = 0);
	}
}
