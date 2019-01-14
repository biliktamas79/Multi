using System;
using System.Collections.Generic;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Enumeration of the possible order when sorting item collections.
    /// (Descending, Unordered, Ascending)
    /// </summary>
    public enum OrderEnum : int
    {
        /// <summary>
        /// -1 = Descending
        /// </summary>
        DESC = -1,
        /// <summary>
        /// 0 = Unordered
        /// </summary>
        None = 0,
        /// <summary>
        /// 1 = Ascending
        /// </summary>
        ASC = 1,
    }
}
