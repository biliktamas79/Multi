namespace Multi
{
    /// <summary>
    /// Delegate class that contains both the old and the new value
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="from">The old value</param>
    /// <param name="to">The new value</param>
    public delegate void ValueChangeDelegate<T>(T from, T to);
}
