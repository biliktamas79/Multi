using System;
using System.Collections.Generic;
using System.Text;

namespace Multi
{
    public class ArrayComparer<T> : IEqualityComparer<T[]>
    {
        /// <summary>
        /// The defaultStatic read-only field for the default array equality comparer using the <see cref="EqualityComparer{T}.Default.Equals"/> for checking array item equality.
        /// </summary>
        public static readonly ArrayComparer<T> Default = new ArrayComparer<T>(EqualityComparer<T>.Default);

        private readonly Func<T, T, bool> _checkEquality;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayComparer{T}"/> class with the given array item equality checker function.
        /// </summary>
        /// <param name="checkEquality">The equality checker function.</param>
        public ArrayComparer(Func<T, T, bool> checkEquality)
        {
            _checkEquality = checkEquality;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayComparer{T}"/> class with the given array item equality comparer.
        /// </summary>
        /// <param name="equalityComparer">The equality comparer.</param>
        public ArrayComparer(IEqualityComparer<T> equalityComparer)
        {
            if (equalityComparer != null)
                _checkEquality = equalityComparer.Equals;
        }

        /// <summary>
        /// c
        /// </summary>
        /// <param name="arr1">The first array.</param>
        /// <param name="arr2">The second array</param>
        /// <param name="checkEquality">The array item equality checker to use, or null to use the <see cref="EqualityComparer{T}.Default.Equals"/> method.</param>
        /// <returns>Returns true if the two arrays are both null or if they have the same length and the same values at the same index.</returns>
        public static bool AreEqual(T[] arr1, T[] arr2, Func<T, T, bool> checkEquality = null)
        {
            if (object.ReferenceEquals(arr1, arr2))
                return true;

            bool isNull1 = object.ReferenceEquals(arr1, null);
            bool isNull2 = object.ReferenceEquals(arr2, null);

            if (isNull1 && isNull2)
                return true;
            else if (!isNull1 && !isNull2)
            {
                if (arr1.Length != arr2.Length)
                    return false;
                if (arr1.Length == 0)
                    return true;

                if (checkEquality == null)
                    checkEquality = EqualityComparer<T>.Default.Equals;

                for (int i = 0; i < arr1.Length; i++)
                {
                    // if the values at the index are different
                    if (!checkEquality(arr1[i], arr2[i]))
                        return false;
                }

                // If all elements were same.
                return true;
            }

            // if only one of them is null
            return false;
        }

        public bool Equals(T[] x, T[] y)
        {
            return AreEqual(x, y, _checkEquality);
        }

        public int GetHashCode(T[] obj)
        {
            if (obj == null)
                return 0;

            int hash = obj.Length;
            for (int i = 0; i < obj.Length; i++)
            {
                hash ^= obj[i].GetHashCode();
            }
            return hash;
        }
    }
}
