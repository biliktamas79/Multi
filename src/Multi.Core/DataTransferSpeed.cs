using System;
using System.Globalization;
using System.Text;

namespace Multi
{
	/// <summary>
	/// Description of DataTransferSpeed.
	/// </summary>
	[Serializable]
	public struct DataTransferSpeed : IEquatable<DataTransferSpeed>, IComparable<DataTransferSpeed>
	{
		#region STATIC METHODS
		public static DataTransferSpeed FromKiloBytesPerSec(double KBPerSec)
		{
			return new DataTransferSpeed(DataSize.KiloBytes_to_Bytes(KBPerSec));
		}

        public static DataTransferSpeed FromKibiBytesPerSec(double KiBPerSec)
        {
            return new DataTransferSpeed(DataSize.KibiBytes_to_Bytes(KiBPerSec));
        }

        public static DataTransferSpeed FromMegaBytesPerSec(double MBPerSec)
		{
			return new DataTransferSpeed(DataSize.MegaBytes_to_Bytes(MBPerSec));
		}

        public static DataTransferSpeed FromMebiBytesPerSec(double MiBPerSec)
        {
            return new DataTransferSpeed(DataSize.MebiBytes_to_Bytes(MiBPerSec));
        }

        public static DataTransferSpeed FromGigaBytesPerSec(double GBPerSec)
		{
			return new DataTransferSpeed(DataSize.GigaBytes_to_Bytes(GBPerSec));
		}

        public static DataTransferSpeed FromGibiBytesPerSec(double GiBPerSec)
        {
            return new DataTransferSpeed(DataSize.GibiBytes_to_Bytes(GiBPerSec));
        }

        public static DataTransferSpeed FromTeraBytesPerSec(double TBPerSec)
		{
			return new DataTransferSpeed(DataSize.TeraBytes_to_Bytes(TBPerSec));
		}

        public static DataTransferSpeed FromTebiBytesPerSec(double TiBPerSec)
        {
            return new DataTransferSpeed(DataSize.TebiBytes_to_Bytes(TiBPerSec));
        }


        private static void ValidateMaxDecimalsCount(int maxDecimalsCount)
		{
			if (maxDecimalsCount < 0)
				throw new ArgumentOutOfRangeException("maxDecimalsCount", "Negative values are not allowed!");
		}

		private static string ToString(string format, double value, int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
		{
			ValidateMaxDecimalsCount(maxDecimalsCount);

			string numberFormatter = DataSize.GetNumberFormatter(maxDecimalsCount);
			return string.Format(format, value.ToString(numberFormatter, numberFormatProvider ?? CultureInfo.InvariantCulture));
		}
		#endregion STATIC METHODS		END
		
		#region	CONSTRUCTORS
		public DataTransferSpeed(double bytesPerSec)
		{
			this._bytesPerSec = bytesPerSec;
		}
        public DataTransferSpeed(DataSize dataSize, double timeSec)
		{
            this._bytesPerSec = (timeSec == 0) ? 
                ((dataSize.Bytes < 0) ? double.NegativeInfinity : double.PositiveInfinity) : 
                dataSize.Bytes / timeSec;
		}
        public DataTransferSpeed(DataSize dataSize, TimeSpan time)
            : this(dataSize, time.TotalSeconds)
        {}
		public DataTransferSpeed(DataTransferSpeed speed)
		{
			this._bytesPerSec = speed._bytesPerSec;
		}
		#endregion CONSTRUCTORS		END
		
		#region	PROPERTIES
        public double BitsPerSec
        {
            get { return _bytesPerSec * 8; }
        }

		private double _bytesPerSec;
        /// <summary>
        /// Gets the data transfer speed in Byte/sec
        /// </summary>
		public double BytesPerSec
		{
			get { return _bytesPerSec; }
			//set { _bytesPerSec = value; }
		}

        public double KiloBitsPerSec
        {
            get { return (_bytesPerSec * 8) / DataSize.Multiplier_Kilo; }
        }
		public double KiloBytesPerSec
		{
			get { return _bytesPerSec / DataSize.Multiplier_Kilo; }
		}

        public double MegaBitsPerSec
        {
            get { return (_bytesPerSec * 8) / DataSize.Multiplier_Mega; }
        }
		public double MegaBytesPerSec
		{
			get { return _bytesPerSec / DataSize.Multiplier_Mega; }
		}

        public double GigaBitsPerSec
        {
            get { return (_bytesPerSec * 8) / DataSize.Multiplier_Giga; }
        }
		public double GigaBytesPerSec
		{
			get { return _bytesPerSec / DataSize.Multiplier_Giga; }
		}

        public double TeraBitsPerSec
        {
            get { return (_bytesPerSec * 8) / DataSize.Multiplier_Tera; }
        }
		public double TeraBytesPerSec
		{
			get { return _bytesPerSec / DataSize.Multiplier_Tera; }
		}

        public double PetaBitsPerSec
        {
            get { return (_bytesPerSec * 8) / DataSize.Multiplier_Peta; }
        }
        public double PetaBytesPerSec
        {
            get { return _bytesPerSec / DataSize.Multiplier_Peta; }
        }
        #endregion PROPERTIES		END

        #region	METHODS
		public override string ToString()
		{
			return ToString(2);
		}

        public string ToString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            double absBytesPerSec = Math.Abs(_bytesPerSec);
            if (absBytesPerSec < DataSize.Multiplier_Kilo)
                return ToBytesPerSecString(maxDecimalsCount, numberFormatProvider);
            else if (absBytesPerSec < DataSize.Multiplier_Mega)
                return ToKiloBytesPerSecString(maxDecimalsCount, numberFormatProvider);
            else if (absBytesPerSec < DataSize.Multiplier_Giga)
                return ToMegaBytesPerSecString(maxDecimalsCount, numberFormatProvider);
            else if (absBytesPerSec < DataSize.Multiplier_Tera)
                return ToGigaBytesPerSecString(maxDecimalsCount, numberFormatProvider);
            else if (absBytesPerSec < DataSize.Multiplier_Peta)
                return ToTeraBytesPerSecString(maxDecimalsCount, numberFormatProvider);
            else
                return ToPetaBytesPerSecString(maxDecimalsCount, numberFormatProvider);
        }

        public string ToBitsPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} Bit/sec", this.BitsPerSec, maxDecimalsCount, numberFormatProvider);
        }
		
        public string ToBytesPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
		{
			return ToString("{0} Byte/sec", this._bytesPerSec, maxDecimalsCount, numberFormatProvider);
		}

        public string ToKiloBitsPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} KBit/sec", this.KiloBitsPerSec, maxDecimalsCount, numberFormatProvider);
        }
		
        public string ToKiloBytesPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} KByte/sec", this.KiloBytesPerSec, maxDecimalsCount, numberFormatProvider);
        }

        public string ToMegaBitsPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} MBit/sec", this.MegaBitsPerSec, maxDecimalsCount, numberFormatProvider);
        }
		
        public string ToMegaBytesPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} MByte/sec", this.MegaBytesPerSec, maxDecimalsCount, numberFormatProvider);
        }

        public string ToGigaBitsPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} GBit/sec", this.GigaBitsPerSec, maxDecimalsCount, numberFormatProvider);
        }
		
        public string ToGigaBytesPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} GByte/sec", this.GigaBytesPerSec, maxDecimalsCount, numberFormatProvider);
        }

        public string ToTeraBitsPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} TBit/sec", this.TeraBitsPerSec, maxDecimalsCount, numberFormatProvider);
        }

        public string ToTeraBytesPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} TByte/sec", this.TeraBytesPerSec, maxDecimalsCount, numberFormatProvider);
        }

        public string ToPetaBitsPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} PBit/sec", this.PetaBitsPerSec, maxDecimalsCount, numberFormatProvider);
        }

        public string ToPetaBytesPerSecString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} PByte/sec", this.PetaBytesPerSec, maxDecimalsCount, numberFormatProvider);
        }
        #endregion METHODS		END

        #region Equals and GetHashCode implementation
        public override bool Equals(object obj)
		{
			if (obj is DataTransferSpeed)
				return Equals((DataTransferSpeed)obj);
			else
				return false;
		}
		
		public bool Equals(DataTransferSpeed other)
		{
			return this._bytesPerSec == other._bytesPerSec;
		}
		
		public override int GetHashCode()
		{
			return _bytesPerSec.GetHashCode();
		}
		#endregion

        #region IComparable<DataTransferSpeed> Members
        public int CompareTo(DataTransferSpeed other)
        {
            return Math.Sign(this._bytesPerSec - other._bytesPerSec);
        }
        #endregion

        #region ---------------------------------		OPERATOR OVERRIDE		----------------------------------------
        public static bool operator ==(DataTransferSpeed left, DataTransferSpeed right)
        {
            return (left._bytesPerSec == right._bytesPerSec);
        }
        public static bool operator !=(DataTransferSpeed left, DataTransferSpeed right)
        {
            return (left._bytesPerSec != right._bytesPerSec);
        }

        public static DataTransferSpeed operator +(DataTransferSpeed left, DataTransferSpeed right)
        {
            return new DataTransferSpeed(left._bytesPerSec + right._bytesPerSec);
        }
        public static DataTransferSpeed operator -(DataTransferSpeed left, DataTransferSpeed right)
        {
            return new DataTransferSpeed(left._bytesPerSec - right._bytesPerSec);
        }

        public static bool operator >(DataTransferSpeed left, DataTransferSpeed right)
        {
            return left._bytesPerSec > right._bytesPerSec;
        }
        public static bool operator >=(DataTransferSpeed left, DataTransferSpeed right)
        {
            return left._bytesPerSec >= right._bytesPerSec;
        }
        public static bool operator <(DataTransferSpeed left, DataTransferSpeed right)
        {
            return left._bytesPerSec < right._bytesPerSec;
        }
        public static bool operator <=(DataTransferSpeed left, DataTransferSpeed right)
        {
            return left._bytesPerSec <= right._bytesPerSec;
        }

        public static DataSize operator *(DataTransferSpeed dts, TimeSpan ts)
        {
            return new DataSize(dts._bytesPerSec * ts.TotalSeconds);
        }
        #endregion ---------------------------------		OPERATOR OVERRIDE	END
    }
}
