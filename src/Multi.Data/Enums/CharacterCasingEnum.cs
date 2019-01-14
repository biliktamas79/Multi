using System;
using System.Collections.Generic;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Enumeration of the possible character casing values, mostly used by comparers.
    /// (Descending, Unordered, Ascending)
    /// </summary>
    public enum CharacterCasingEnum : int
    {
        /// <summary>
        /// 0 = Unchanged
        /// </summary>
        Unchanged = 0,
        /// <summary>
        /// 1 = Lower case
        /// </summary>
        LowerCase = 1,
        /// <summary>
        /// 2 = Upper case
        /// </summary>
        UpperCase = 2,
    }
}
