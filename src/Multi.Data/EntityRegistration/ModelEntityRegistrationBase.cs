using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    /// <summary>
    /// Abstract base class for model entity registrations
    /// </summary>
    public abstract class ModelEntityRegistrationBase : IEquatable<ModelEntityRegistrationBase>
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
        /// <param name="tableName">Name of the table. (can be null)</param>
        /// <param name="entityType">Type of the entity. (not null)</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        protected internal ModelEntityRegistrationBase(string tableName, Type entityType)
        {
            //Throw.IfArgumentIsNullOrWhiteSpace(tableName, nameof(tableName));
            Throw.IfArgumentIsNull(entityType, nameof(entityType));

            this.TableName = tableName;
            this.EntityType = entityType;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
                return false;

            return Equals(obj as ModelEntityRegistrationBase);
        }

        /// <summary>
        /// Indicates whether the <paramref name="other"/> instance is not null and its <see cref="ModelEntityRegistrationBase.EntityType"/> and <see cref="ModelEntityRegistrationBase.TableName"/> equals this instance's.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the <paramref name="other"/> instance is not null and its <see cref="ModelEntityRegistrationBase.EntityType"/> and <see cref="ModelEntityRegistrationBase.TableName"/> equals this instance's; otherwise, false.
        /// </returns>
        public bool Equals(ModelEntityRegistrationBase other)
        {
            if (object.ReferenceEquals(other, null))
                return false;

            return this.EntityType == other.EntityType
                && this.TableName == other.TableName;
        }

        /// <summary>
        /// Returns a combined hash code of <see cref="ModelEntityRegistrationBase.EntityType"/> and <see cref="ModelEntityRegistrationBase.TableName"/> values.
        /// </summary>
        /// <returns>
        /// A combined hash code of <see cref="ModelEntityRegistrationBase.EntityType"/> and <see cref="ModelEntityRegistrationBase.TableName"/> values for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return object.ReferenceEquals(this.TableName, null)
                ? this.EntityType.GetHashCode()
                : this.EntityType.GetHashCode() ^ this.TableName.GetHashCode();
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
