using System;

namespace Multi.Timing
{
    /// <summary>
	/// Interface for time sources
	/// </summary>
	public interface ITimeSource
    {
        /// <summary>
        /// Gets the name of the time source.
        /// </summary>
        string TimeSourceName
        { get; }

        /// <summary>
        /// Gets the description of the time source.
        /// </summary>
        string TimeSourceDescription
        { get; }

        /// <summary>
        /// Gets a System.DateTime structure representing the current date and time in the local timezone.
        /// </summary>
        DateTime Now
        { get; }

        /// <summary>
        /// Gets a System.DateTime structure representing the current date and time by UTC.
        /// </summary>
        DateTime NowUtc
        { get; }

        /// <summary>
        /// Gets a System.DateTime structure representing the current date.
        /// </summary>
        DateTime Today
        { get; }

        /// <summary>
        /// Gets a System.DateTime structure representing the current date by UTC.
        /// </summary>
        DateTime TodayUtc
        { get; }
    }
}
