using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Data
{
    public static partial class Extensions
	{
        /// <summary>
        /// Gets the type of the entities
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="repo">The entity repo</param>
        /// <returns>Returns the type of the entity.</returns>
        public static Type GetEntityType<TEntity>(this IEntityQuery<TEntity> repo)
        {
            return typeof(TEntity);
        }
	}
}
