namespace Multi.Data
{
    /// <summary>
    /// Generic interface for read-only Primary Key holders
    /// </summary>
    /// <typeparam name="Tpk">The type of the primary key</typeparam>
    public interface IReadOnlyPkHolder<Tpk>
    {
        /// <summary>
        /// Gets the primary key of this instance.
        /// </summary>
        /// <returns>Returns the value representing the primary key of this instance</returns>
        Tpk GetPk();
    }
}
