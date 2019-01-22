using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic async repository interface of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityRepoAsync<TPrimaryKey, TEntity> 
        : IEntityRepoAsync<TEntity>, IEntityQueryByPkAsync<TPrimaryKey, TEntity>, IEntityQueryAsync<TPrimaryKey, TEntity>
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>
	{
        /// <summary>
        /// Updates an existing entity in the repository by the provided instance
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="entity">The entity to update</param>
        Task Update(CancellationToken cancellationToken, TEntity entity);
        /// <summary>
        /// Updates already existing entities in the repository by the provided instances
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="entities">The entities to update</param>
        Task Update(CancellationToken cancellationToken, IEnumerable<TEntity> entities);
        /// <summary>
        /// Deletes the entity having the provided primary key
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="pk">The primary key value</param>
		Task Delete(CancellationToken cancellationToken, TPrimaryKey pk);
        /// <summary>
        /// Deletes the entities having the provided primary key values
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="pks">The primary key values</param>
		Task Delete(CancellationToken cancellationToken, IEnumerable<TPrimaryKey> pks);
	}
}
