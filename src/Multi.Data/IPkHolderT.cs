namespace Multi.Data
{
    /// <summary>
    /// Generic interface for writable Primary Key holders
    /// </summary>
    /// <typeparam name="Tpk">The type of the primary key</typeparam>
    public interface IPkHolder<Tpk> : IReadOnlyPkHolder<Tpk>
    {
        /// <summary>
        /// Sets the primary key of this instance to the provided value
        /// </summary>
        /// <param name="pk">The primary key value</param>
        void SetPk(Tpk pk);
    }
}
