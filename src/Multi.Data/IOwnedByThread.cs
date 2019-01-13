namespace Multi.Data
{
    /// <summary>
    /// Interface for components owned by a thread
    /// </summary>
    public interface IOwnedByThread
    {
        /// <summary>
        /// Gets the managed thread identifier of the owner thread
        /// </summary>
        int OwnerThreadId { get; }
    }
}
