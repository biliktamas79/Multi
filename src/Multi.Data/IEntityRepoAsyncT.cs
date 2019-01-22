using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic async repository interface of entity types
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityRepoAsync<TEntity> : IEntityQueryAsync<TEntity>
	{
        /// <summary>
        /// Inserts the provided entity into this repository
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="entity">The entity to insert</param>
		Task Insert(CancellationToken cancellationToken, TEntity entity);

        /// <summary>
        /// Inserts the provided entities into this repository
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="entities">The entities to insert</param>
        Task Insert(CancellationToken cancellationToken, IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the provided entity from this repository
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="entity">The entity to delete</param>
        Task Delete(CancellationToken cancellationToken, TEntity entity);

        /// <summary>
        /// Deletes the provided entities from this repository
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        /// <param name="entities">The entities to delete</param>
        Task Delete(CancellationToken cancellationToken, IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes all entities in this repository
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used for cancelling this operation</param>
        Task DeleteAll(CancellationToken cancellationToken);
	}
}
