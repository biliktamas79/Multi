using System.Collections.Generic;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic repository interface of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityRepo<TPrimaryKey, TEntity> 
        : IEntityRepo<TEntity>, IEntityQueryByPk<TPrimaryKey, TEntity>, IEntityQuery<TPrimaryKey, TEntity>
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>
	{
        /// <summary>
        /// Updates an existing entity in the repository by the provided instance
        /// </summary>
        /// <param name="entity">The entity to update</param>
        void Update(TEntity entity);
        /// <summary>
        /// Updates already existing entities in the repository by the provided instances
        /// </summary>
        /// <param name="entities">The entities to update</param>
        void Update(IEnumerable<TEntity> entities);
        /// <summary>
        /// Deletes the entity having the provided primary key
        /// </summary>
        /// <param name="pk">The primary key value</param>
		void Delete(TPrimaryKey pk);
        /// <summary>
        /// Deletes the entities having the provided primary key values
        /// </summary>
        /// <param name="pks">The primary key values</param>
		void Delete(IEnumerable<TPrimaryKey> pks);
	}
}
