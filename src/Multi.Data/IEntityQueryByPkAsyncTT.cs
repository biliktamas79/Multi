using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic async "query by primary key" interface of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityQueryByPkAsync<TPrimaryKey, TEntity> : IFluentInterface
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>
	{
        /// <summary>
        /// Checks whether the provided primary key already exists, or not
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="pk">The primary key</param>
        /// <returns>Returns a boolean value indicating that whether <paramref name="pk"/> already exists, or not.</returns>
		Task<bool> Exists(CancellationToken cancellationToken, TPrimaryKey pk);

        /// <summary>
        /// Tries to get the entity with the provided primary key
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="pk">The primary key</param>
        /// <param name="entity">The found entity or null</param>
        /// <returns>Returns true if the entity is found, otherwise false.</returns>
        Task<bool> TryGetByPk(CancellationToken cancellationToken, TPrimaryKey pk, out TEntity entity);

        /*
        /// <summary>
        /// Check whether there are any entities matching the optional primary key filter
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="pkFilter">The optional primary key filter</param>
        /// <returns>Returns true if there are entities matching the optional primary key filter</returns>
        Task<bool> AnyByPk(CancellationToken cancellationToken, Expression<Func<TPrimaryKey, bool>> pkFilter = null);

        /// <summary>
        /// Gets the count of entities matching the optional primary key filter
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="pkFilter">The optional primary key filter</param>
        /// <returns>Returns the count of entities matching the optional primary key filter</returns>
        Task<uint> CountByPk(CancellationToken cancellationToken, Expression<Func<TPrimaryKey, bool>> pkFilter = null);

        /// <summary>
        /// Gets the entities matching the optional primary key filter, ordering and paging values
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="pkFilter">The optional primary key filter</param>
        /// <param name="orderBy">The optional ordering of the matching records.</param>
        /// <param name="skip">The count of entities to skip. (Default = 0)</param>
        /// <param name="take">The count of entities to take. (Default = -1, meaning all entities)</param>
        /// <returns>Returns the entities matching the optional primary key filter</returns>
        IEnumerable<Task<TEntity>> GetByPk(CancellationToken cancellationToken, Expression<Func<TPrimaryKey, bool>> pkFilter = null, OrderBy[] orderBy = null, int skip = 0, int take = 0);

        /// <summary>
        /// Gets the entities matching the optional primary key filter, ordering and paging values, transformed by the provided converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="converter">The entity converter to the output type</param>
        /// <param name="pkFilter">The optional primary key filter</param>
        /// <param name="orderBy">The optional ordering of the matching records.</param>
        /// <param name="skip">The count of entities to skip. (Default = 0)</param>
        /// <param name="take">The count of entities to take. (Default = -1, meaning all entities)</param>
        /// <returns>Returns the entities matching the provided primary key filter transformed by the provided converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="converter" /> is null.</exception>
        IEnumerable<Task<T>> GetByPk<T>(CancellationToken cancellationToken, Expression<Func<TEntity, T>> converter, Expression<Func<TPrimaryKey, bool>> pkFilter = null, OrderBy[] orderBy = null, int skip = 0, int take = 0);
        */
    }
}
