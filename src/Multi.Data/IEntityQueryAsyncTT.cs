using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Multi.Data
{
    /// <summary>
    /// Basic generic "primary key query" interface of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public interface IEntityQueryAsync<TPrimaryKey, TEntity> : IEntityQuery<TEntity>
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>
	{
        /// <summary>
        /// Gets the primary keys of entities matching the optional entity filter
        /// </summary>
        /// <param name="filter">The optional entity filter</param>
        /// <param name="orderBy">The optional ordering of the matching records.</param>
        /// <param name="skip">The count of entities to skip. (Default = 0)</param>
        /// <param name="take">The count of entities to take. (Default = -1, meaning all entities)</param>
        /// <returns>Returns the primary keys of entities matching the optional entity filter</returns>
        IEnumerable<Task<TPrimaryKey>> GetPks(Expression<Func<TEntity, bool>> filter = null, OrderBy[] orderBy = null, uint skip = 0, uint take = 0);

        /// <summary>
        /// Gets the primary keys of entities matching the provided entity filter transformed by the provided primary key converter
        /// </summary>
        /// <typeparam name="T">The type of the returned output records</typeparam>
        /// <param name="pkConverter">The primary key converter to the output type</param>
        /// <param name="filter">The optional entity filter</param>
        /// <param name="orderBy">The optional ordering of the matching records.</param>
        /// <param name="skip">The count of entities to skip. (Default = 0)</param>
        /// <param name="take">The count of entities to take. (Default = -1, meaning all entities)</param>
        /// <returns>Returns the primary keys of entities matching the provided entity filter transformed by the provided primary key converter</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pkConverter"/> is null.</exception>
        IEnumerable<Task<T>> GetPks<T>(Expression<Func<TPrimaryKey, T>> pkConverter, Expression<Func<TEntity, bool>> filter = null, OrderBy[] orderBy = null, uint skip = 0, uint take = 0);
    }
}
