using System.Collections.Generic;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic repository interface of entity types
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
	public interface IEntityRepo<TEntity> : IEntityQuery<TEntity>
	{
        /// <summary>
        /// Inserts the provided entity into this repository
        /// </summary>
        /// <param name="entity">The entity to insert</param>
		void Insert(TEntity entity);
        
        /// <summary>
        /// Inserts the provided entities into this repository
        /// </summary>
        /// <param name="entities">The entities to insert</param>
		void Insert(IEnumerable<TEntity> entities);
        
        /// <summary>
        /// Deletes the provided entity from this repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
		void Delete(TEntity entity);
        
        /// <summary>
        /// Deletes the provided entities from this repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
		void Delete(IEnumerable<TEntity> entities);
        
        /// <summary>
        /// Deletes all entities in this repository
        /// </summary>
		void DeleteAll();
	}
}
