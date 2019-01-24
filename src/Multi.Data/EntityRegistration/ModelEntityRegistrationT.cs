using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    /// <summary>
    /// Class for model entity registrations of entity types not having primary key
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public class ModelEntityRegistration<TEntity> : ModelEntityRegistrationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelEntityRegistration{TEntity}"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public ModelEntityRegistration(string tableName)
            : base(tableName, typeof(TEntity))
        {}

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.GetType().GetFriendlyTypeName()} to table '{TableName}'.";
        }
    }
}
