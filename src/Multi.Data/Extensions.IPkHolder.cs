using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Static class containing extension methods
    /// </summary>
    public static partial class Extensions
	{
        ///// <summary>
        ///// Indicates whether primary key value of the current instance equals to another primary key.
        ///// </summary>
        ///// <param name="other">The other primary key to compare with this instance</param>
        ///// <returns>true if the primary key value is equal to the other parameter; otherwise, false.</returns>
        //bool PkEquals(Tpk other);

        /// <summary>
        /// Indicates whether primary key value of the current instance equals to another primary key.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of primary key</typeparam>
        /// <param name="pkHolder">The primary key holder</param>
        /// <param name="other">The other primary key to check pk equality to</param>
        /// <param name="comparer">The pk equality comparer to use, or null</param>
        /// <returns>Returns a boolean value indicating that whether the primary key value of <paramref name="pkHolder"/> equals to the provided <paramref name="other"/> primary key.</returns>
        public static bool PkEquals<TPrimaryKey>(this IReadOnlyPkHolder<TPrimaryKey> pkHolder, TPrimaryKey other, IEqualityComparer<TPrimaryKey> comparer = null)
            //where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            if (pkHolder == null)
                return false;

            TPrimaryKey pk = pkHolder.GetPk();
            //if (object.ReferenceEquals(pk, other))
            //    return true;
            //if (pk = null)
            //    return other == null;
            return (comparer ?? EqualityComparer<TPrimaryKey>.Default).Equals(pk, other);
        }

        /// <summary>
        /// Indicates whether primary key value of the current instance equals to the primary key value of another instance.
        /// </summary>
        /// <typeparam name="TPrimaryKey">The type of primary key</typeparam>
        /// <param name="pkHolder">The primary key holder</param>
        /// <param name="other">The other primary key holder to check pk equality to</param>
        /// <param name="comparer">The pk equality comparer to use, or null</param>
        /// <returns>Returns a boolean value indicating that whether the primary key value of <paramref name="pkHolder"/> equals to the primary key value of the provided <paramref name="other"/> primary key holder.</returns>
        public static bool PkEquals<TPrimaryKey>(this IReadOnlyPkHolder<TPrimaryKey> pkHolder, IReadOnlyPkHolder<TPrimaryKey> other, IEqualityComparer<TPrimaryKey> comparer = null)
        //where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            if (pkHolder == null)
                return (other == null);
            if (object.ReferenceEquals(other, null) || (other == null))
                return false;
            if (object.ReferenceEquals(pkHolder, other))
                return true;

            TPrimaryKey pk = pkHolder.GetPk();
            TPrimaryKey otherPk = other.GetPk();
            //if (object.ReferenceEquals(pk, other))
            //    return true;
            //if (pk = null)
            //    return other == null;
            return (comparer ?? EqualityComparer<TPrimaryKey>.Default).Equals(pk, otherPk);
        }
	}
}
