using System;

namespace Multi.Timing
{
    /// <summary>
    /// Time source for returning date and time information provided by the OS of the running device.
    /// (Returns values using the <see cref="System.DateTime"/> as source.)
    /// </summary>
    [Serializable]
    public sealed class LocalTimeSource : ITimeSource
    {
        #region	STATIC
        /// <summary>
        /// Read-only field for the local time source
        /// </summary>
        [NonSerialized]
        public readonly static LocalTimeSource Instance;

        static LocalTimeSource()
        {
            Instance = new LocalTimeSource();
        }
        #endregion	STATIC		END

        /// <summary>
        /// The name of the local time source: 'Local'
        /// </summary>
        [NonSerialized]
        public const string name = "Local";
        /// <summary>
        /// The description of the time source.
        /// </summary>
        [NonSerialized]
        public const string description = "Local time provided by the clock of the running device.\r\n(Time returned from System.DateTime class)";

        #region ITimeSource Members
        /// <summary>
        /// Gets the name of the time source.
        /// </summary>
        public string TimeSourceName
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the description of the time source.
        /// </summary>
        public string TimeSourceDescription
        {
            get { return description; }
        }

        /// <summary>
        /// Gets a System.DateTime structure representing the current date and time.
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// Gets a System.DateTime structure representing the current date and time by UTC.
        /// </summary>
        public DateTime NowUtc
        {
            get { return DateTime.UtcNow; }
        }

        /// <summary>
        /// Gets a System.DateTime structure representing the current date.
        /// </summary>
        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        /// <summary>
        /// Gets a System.DateTime structure representing the date of the current UTC time.
        /// </summary>
        public DateTime TodayUtc
        {
            get { return DateTime.UtcNow.Date; }
        }
        #endregion
    }
}
