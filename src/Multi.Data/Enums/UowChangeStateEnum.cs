using System;

namespace Multi.Data
{
    /// <summary>
    /// Enumeration of the possible change states of a Unit Of Work
    /// (NoChanges, HasChanges, Commiting, RollingBack)
    /// </summary>
    public enum UowChangeStateEnum : int
    {
        /// <summary>
        /// 0 = There are no changes
        /// </summary>
        NoChanges = 0, 
        /// <summary>
        /// 1 = There are changes
        /// </summary>
        HasChanges = 1, 
        /// <summary>
        /// 2 = Commit is in progress
        /// </summary>
        Commiting = 2, 
        /// <summary>
        /// 4 = Rollback is in progress
        /// </summary>
        RollingBack = 4
    }
}
