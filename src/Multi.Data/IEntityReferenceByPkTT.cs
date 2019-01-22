namespace Multi.Data
{
    /// <summary>
    /// Generic interface for entity references by primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public interface IEntityReferenceByPk<TPrimaryKey, TEntity> : IFluentInterface
        where TEntity : /*class,*/ IReadOnlyPkHolder<TPrimaryKey>//, new()
    {
        /// <summary>
        /// Gets the primary key value
        /// </summary>
        TPrimaryKey Pk { get; }

        /// <summary>
        /// Gets the entity
        /// </summary>
        TEntity Entity { get; }
    }
}
