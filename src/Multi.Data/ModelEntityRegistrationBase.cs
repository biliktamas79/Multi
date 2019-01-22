using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Abstract base class for model entity registrations
    /// </summary>
    public abstract class ModelEntityRegistrationBase
    {
        /// <summary>
        /// Read-only field for the table name
        /// </summary>
        public readonly string TableName;
        /// <summary>
        /// Read-only field for the entity type
        /// </summary>
        public readonly Type EntityType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelEntityRegistrationBase"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        protected internal ModelEntityRegistrationBase(string tableName, Type entityType)
        {
            //Throw.IfArgumentIsNullOrWhiteSpace(tableName, nameof(tableName));
            Throw.IfArgumentIsNull(entityType, nameof(entityType));

            this.TableName = tableName;
            this.EntityType = entityType;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"ModelEntityRegistration: {EntityType.GetFriendlyTypeName()} to table '{TableName}'.";
        }
    }
}
