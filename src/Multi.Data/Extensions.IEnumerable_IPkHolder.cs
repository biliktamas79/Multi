using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Data
{
    public static partial class Extensions
	{
        /// <summary>
        /// Enumerates the primary key values of the entities
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of primary key</typeparam>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="entities">The entities</param>
        /// <returns>Returns the enumerable primary keys</returns>
        public static IEnumerable<TPrimaryKey> PrimaryKeys<TPrimaryKey, TEntity>(this IEnumerable<TEntity> entities)
            where TEntity : IPkHolder<TPrimaryKey>
        {
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    if (entity != null)
                        yield return entity.GetPk();
                }
            }
        }
	}
}
