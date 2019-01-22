using System;

namespace Multi
{
    /// <summary>
    /// Attribute used for marking classes containing <see cref="Microsoft.Extensions.Logging.EventId"/> declarations.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class LogEventsClassAttribute : Attribute
    {
        // This is a positional argument
        public LogEventsClassAttribute()
        {}
    }
}
