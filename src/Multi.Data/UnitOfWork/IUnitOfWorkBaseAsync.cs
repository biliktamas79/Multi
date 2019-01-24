using System;
using System.Threading.Tasks;

namespace Multi.Data.UnitOfWork
{
    /// <summary>
    /// Interface for async UnitOfWork components
    /// </summary>
    public partial interface IUnitOfWorkBaseAsync : IDisposable
	{
        /// <summary>
        /// Gets the database context this unit of work belongs to
        /// </summary>
        DbContextBase DbContext { get; }

        /// <summary>
        /// Gets the unique identifier of this unit of work instance
        /// </summary>
        int UowId { get; }

        /// <summary>
        /// Gets the change state of this unit of work instance
        /// </summary>
        UowChangeStateEnum ChangeState { get; }

        /// <summary>
        /// Gets a boolean value indicating that whether disposing of this instance has been started, or not
        /// </summary>
        bool IsDisposingOrDisposed { get; }

        /// <summary>
        /// Tries to get the repo for the entity type
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="repo">The found repo or null</param>
        /// <returns>Return true if the repo for the entity type is found, otherwise returns false.</returns>
        bool TryGetRepo<TEntity>(out IEntityRepo<TEntity> repo);

        /// <summary>
        /// Tries to get the repo for the entity type with the provided primary key type
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <param name="repo">The found repo or null</param>
        /// <returns>Return true if the repo for the entity type with the provided primary key type is found, otherwise returns false.</returns>
        bool TryGetRepo<TPrimaryKey, TEntity>(out IEntityRepo<TPrimaryKey, TEntity> repo)
            where TEntity : IReadOnlyPkHolder<TPrimaryKey>;

        /// <summary>
        /// Commits all changes made since the beginning or the last call to Commit()
        /// </summary>
        Task Commit();

        /// <summary>
        /// Rolls back all changes made since the beginning or the last call to Commit()
        /// </summary>
        Task Rollback();
	}
}
