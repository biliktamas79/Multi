using System;
using System.Globalization;
using System.Text;

namespace Multi
{
    /// <summary>
    /// Struct representing the size of data
    /// https://en.wikipedia.org/wiki/Kibibyte
    /// </summary>
    [Serializable]
	public struct DataSize : IEquatable<DataSize>, IComparable<DataSize>
    {
        #region CONST
        //// https://www.dr-lex.be/info-stuff/bytecalc.html
        //// Full notation   Symbol Value
        //// 1 kilobyte	1 kB	10^3 = 1000 bytes
        //// 1 megabyte	1 MB	10^6 = 1000000 bytes
        //// 1 gigabyte	1 GB	10^9 = 1000000000 bytes
        //// 1 terabyte	1 TB	10^12 = 1000000000000 bytes
        //// 1 petabyte	1 PB	10^15 = 1000000000000000 bytes
        //// 1 kibibyte	1 KiB	2^10 = 1024 bytes
        //// 1 mebibyte	1 MiB	2^20 = 1048576 bytes
        //// 1 gibibyte	1 GiB	2^30 = 1073741824 bytes
        //// 1 tebibyte	1 TiB	2^40 = 1099511627776 bytes
        //// 1 pebibyte	1 PiB	2^50 = 1125899906842624 bytes

        /// <summary>
        /// 10^3 = 1 000
        /// </summary>
        public const double Multiplier_Kilo = 1000.0;
        /// <summary>
		/// 2^10 = 1 024
		/// </summary>
		public const double Multiplier_Kibi = 1024.0;
        /// <summary>
        /// 10^6 = 1 000 000
        /// </summary>
        public const double Multiplier_Mega = 1000000.0;
        /// <summary>
        /// 2^20 = 1024 * 1024 = 1 048 576
        /// </summary>
        public const double Multiplier_Mebi = 1048576.0;
        /// <summary>
        /// 10^9 = 1 000 000 000
        /// </summary>
        public const double Multiplier_Giga = 1000000000.0;
        /// <summary>
        /// 2^30 = 1024 * 1024 * 1024 = 1 073 741 824
        /// </summary>
        public const double Multiplier_Gibi = 1073741824.0;
        /// <summary>
        /// 10^12 = 1 000 000 000 000
        /// </summary>
        public const double Multiplier_Tera = 1000000000000.0;
        /// <summary>
        /// 2^40 = 1024 * 1024 * 1024 * 1024 = 1 099 511 627 776
        /// </summary>
        public const double Multiplier_Tebi = 1099511627776.0;
        /// <summary>
        /// 10^15 = 1 000 000 000 000 000
        /// </summary>
        public const double Multiplier_Peta = 1000000000000000.0;
        /// <summary>
        /// 2^50 = 1024 * 1024 * 1024 * 1024 * 1024 = 1 125 899 906 842 624
        /// </summary>
        public const double Multiplier_Pebi = 1125899906842624.0;
        #endregion CONST

        #region STATIC
        private readonly static string[] _formats = new string[] { "0", "0.#", "0.##", "0.###", "0.####", "0.#####", "0.######" };

        private static void ValidateMaxDecimalsCount(int maxDecimalsCount)
		{
			if (maxDecimalsCount < 0)
				throw new ArgumentOutOfRangeException("maxDecimalsCount", "Negative values are not allowed!");
		}
		
		internal static string GetNumberFormatter(int maxDecimalsCount)
		{
			// ha már megvan elõre elkészítve a formatter string
			if ((maxDecimalsCount >= 0) && (maxDecimalsCount < _formats.Length))
				return _formats[maxDecimalsCount];

			StringBuilder sb = new StringBuilder();
			sb.Append("0");
			if (maxDecimalsCount > 0)
				sb.Append(".").Append('#', maxDecimalsCount);
			return sb.ToString();
		}

        // Kilo
        public static DataSize FromKiloBits(double kbit)
        {
            return new DataSize(KiloBytes_to_Bytes(kbit / 8.0));
        }

        public static DataSize FromKibiBits(double kibibit)
        {
            return new DataSize(KibiBytes_to_Bytes(kibibit / 8.0));
        }

        public static DataSize FromKiloBytes(double kB)
		{
			return new DataSize(KiloBytes_to_Bytes(kB));
		}

        public static DataSize FromKibiBytes(double KiB)
        {
            return new DataSize(KibiBytes_to_Bytes(KiB));
        }

        // Mega
        public static DataSize FromMegaBitss(double Mbit)
        {
            return new DataSize(MegaBytes_to_Bytes(Mbit / 8.0));
        }

        public static DataSize FromMebiBitss(double mebibit)
        {
            return new DataSize(MebiBytes_to_Bytes(mebibit / 8.0));
        }

        public static DataSize FromMegaBytes(double MB)
		{
			return new DataSize(MegaBytes_to_Bytes(MB));
		}

        public static DataSize FromMebiBytes(double MiB)
        {
            return new DataSize(MebiBytes_to_Bytes(MiB));
        }

        // Giga
        public static DataSize FromGigaBitss(double Gbit)
        {
            return new DataSize(GigaBytes_to_Bytes(Gbit / 8.0));
        }

        public static DataSize FromGibiBitss(double gibibit)
        {
            return new DataSize(GibiBytes_to_Bytes(gibibit / 8.0));
        }

        public static DataSize FromGigaBytes(double GB)
		{
			return new DataSize(GigaBytes_to_Bytes(GB));
		}

        public static DataSize FromGibiBytes(double GiB)
        {
            return new DataSize(GibiBytes_to_Bytes(GiB));
        }

        // Tera
        public static DataSize FromTeraBitss(double Tbit)
        {
            return new DataSize(TeraBytes_to_Bytes(Tbit / 8.0));
        }

        public static DataSize FromTebiBitss(double tebibit)
        {
            return new DataSize(TebiBytes_to_Bytes(tebibit / 8.0));
        }

        public static DataSize FromTeraBytes(double TB)
		{
			return new DataSize(TeraBytes_to_Bytes(TB));
		}

        public static DataSize FromTebiBytes(double TiB)
        {
            return new DataSize(TebiBytes_to_Bytes(TiB));
        }

        // Peta
        public static DataSize FromPetaBitss(double Pbit)
        {
            return new DataSize(PetaBytes_to_Bytes(Pbit / 8.0));
        }

        public static DataSize FromPebiBitss(double pebibit)
        {
            return new DataSize(PebiBytes_to_Bytes(pebibit / 8.0));
        }

        public static DataSize FromPetaBytes(double PB)
        {
            return new DataSize(PetaBytes_to_Bytes(PB));
        }

        public static DataSize FromPebiBytes(double PiB)
        {
            return new DataSize(PebiBytes_to_Bytes(PiB));
        }


        public static double KiloBytes_to_Bytes(double KB)
		{
			return KB / Multiplier_Kilo;
		}

        public static double KibiBytes_to_Bytes(double KiB)
        {
            return KiB / Multiplier_Kibi;
        }

        public static double MegaBytes_to_Bytes(double MB)
		{
			return MB / Multiplier_Mega;
		}

        public static double MebiBytes_to_Bytes(double MiB)
        {
            return MiB / Multiplier_Mebi;
        }

        public static double GigaBytes_to_Bytes(double GB)
		{
			return GB / Multiplier_Giga;
		}

        public static double GibiBytes_to_Bytes(double GiB)
        {
            return GiB / Multiplier_Gibi;
        }

        public static double TeraBytes_to_Bytes(double TB)
		{
			return TB / Multiplier_Tera;
		}

        public static double TebiBytes_to_Bytes(double TiB)
        {
            return TiB / Multiplier_Tebi;
        }

        public static double PetaBytes_to_Bytes(double PB)
        {
            return PB / Multiplier_Peta;
        }

        public static double PebiBytes_to_Bytes(double PiB)
        {
            return PiB / Multiplier_Pebi;
        }

        private static string ToString(string format, double value, int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            ValidateMaxDecimalsCount(maxDecimalsCount);

            string numberFormatter = GetNumberFormatter(maxDecimalsCount);
            return string.Format(format, value.ToString(numberFormatter, numberFormatProvider ?? CultureInfo.InvariantCulture));
        }
        #endregion STATIC		END

        #region	CONSTRUCTORS
        public DataSize(double bytes)
		{
			this._bytes = bytes;
		}
		public DataSize(DataSize size)
		{
			this._bytes = size._bytes;
		}
		#endregion CONSTRUCTORS		END
		
		#region	PROPERTIES
        public double Bits
        {
            get { return _bytes * 8.0; }
        }

		private double _bytes;
        /// <summary>
        /// Gets the data size in Byte
        /// </summary>
		public double Bytes
		{
			get { return _bytes; }
		}

        public double KiloBits
        {
            get { return (_bytes * 8.0) / Multiplier_Kilo; }
        }
		public double KiloBytes
		{
			get { return _bytes / Multiplier_Kilo; }
		}

        public double KibiBits
        {
            get { return (_bytes * 8.0) / Multiplier_Kibi; }
        }
        public double KibiBytes
        {
            get { return _bytes / Multiplier_Kibi; }
        }

        public double MegaBits
        {
            get { return (_bytes * 8.0) / Multiplier_Mega; }
        }
		public double MegaBytes
		{
			get { return _bytes / Multiplier_Mega; }
		}

        public double MebiBits
        {
            get { return (_bytes * 8.0) / Multiplier_Mebi; }
        }
        public double MebiBytes
        {
            get { return _bytes / Multiplier_Mebi; }
        }

        public double GigaBits
        {
            get { return (_bytes * 8.0) / Multiplier_Giga; }
        }
		public double GigaBytes
		{
			get { return _bytes / Multiplier_Giga; }
		}

        public double GibiBits
        {
            get { return (_bytes * 8.0) / Multiplier_Gibi; }
        }
        public double GibiBytes
        {
            get { return _bytes / Multiplier_Gibi; }
        }

        public double TeraBits
        {
            get { return (_bytes * 8.0) / Multiplier_Tera; }
        }
		public double TeraBytes
		{
			get { return _bytes / Multiplier_Tera; }
		}

        public double TebiBits
        {
            get { return (_bytes * 8.0) / Multiplier_Tebi; }
        }
        public double TebiBytes
        {
            get { return _bytes / Multiplier_Tebi; }
        }

        public double PetaBits
        {
            get { return (_bytes * 8.0) / Multiplier_Peta; }
        }
        public double PetaBytes
        {
            get { return _bytes / Multiplier_Peta; }
        }

        public double PebiBits
        {
            get { return (_bytes * 8.0) / Multiplier_Pebi; }
        }
        public double PebiBytes
        {
            get { return _bytes / Multiplier_Pebi; }
        }
        #endregion PROPERTIES		END

        #region	METHODS
        public override string ToString()
        {
            return ToString(DataSizeBaseUnitEnum.Byte, 2);
        }

        public string ToString(DataSizeBaseUnitEnum baseUnit, int maxDecimalsCount = 2, IFormatProvider numberFormatProvider = null)
        {
            switch (baseUnit)
            {
                case DataSizeBaseUnitEnum.Bit:
                    double absBits = Math.Abs(_bytes * 8.0);
                    if (absBits < Multiplier_Kilo)
                        return ToBitString(maxDecimalsCount, numberFormatProvider);
                    else if (absBits < Multiplier_Mega)
                        return ToKiloBitString(maxDecimalsCount, numberFormatProvider);
                    else if (absBits < Multiplier_Giga)
                        return ToMegaBitString(maxDecimalsCount, numberFormatProvider);
                    else if (absBits < Multiplier_Tera)
                        return ToGigaBitString(maxDecimalsCount, numberFormatProvider);
                    else if (absBits < Multiplier_Peta)
                        return ToTeraBitString(maxDecimalsCount, numberFormatProvider);
                    else
                        return ToPetaBitString(maxDecimalsCount, numberFormatProvider);

                case DataSizeBaseUnitEnum.Byte:
                    double absBytes = Math.Abs(_bytes);
                    if (absBytes < Multiplier_Kilo)
                        return ToByteString(maxDecimalsCount, numberFormatProvider);
                    else if (absBytes < Multiplier_Mega)
                        return ToKiloByteString(maxDecimalsCount, numberFormatProvider);
                    else if (absBytes < Multiplier_Giga)
                        return ToMegaByteString(maxDecimalsCount, numberFormatProvider);
                    else if (absBytes < Multiplier_Tera)
                        return ToGigaByteString(maxDecimalsCount, numberFormatProvider);
                    else if (absBytes < Multiplier_Peta)
                        return ToTeraByteString(maxDecimalsCount, numberFormatProvider);
                    else
                        return ToPetaByteString(maxDecimalsCount, numberFormatProvider);

                default:
                    throw NotSupportedValueException.New<DataSizeBaseUnitEnum>(baseUnit);
            }
        }

        public string ToBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} Bits", this.Bits, maxDecimalsCount, numberFormatProvider);
        }
		
        public string ToByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
		{
			return ToString("{0} Bytes", this.Bytes, maxDecimalsCount, numberFormatProvider);
        }

        // https://en.wikipedia.org/wiki/Kilobit
        // Kilo
        public string ToKiloBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} KBits", this.KiloBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToKibiBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} KiBits", this.KibiBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToKiloByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} KB", this.KiloBytes, maxDecimalsCount, numberFormatProvider);
        }

        public string ToKibiByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} KiB", this.KibiBytes, maxDecimalsCount, numberFormatProvider);
        }

        // Mega
        public string ToMegaBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} MBits", this.MegaBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToMebiBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} MiBits", this.MebiBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToMegaByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} MB", this.MegaBytes, maxDecimalsCount, numberFormatProvider);
        }

        public string ToMebiByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} MiB", this.MebiBytes, maxDecimalsCount, numberFormatProvider);
        }

        // Giga
        public string ToGigaBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} GBits", this.GigaBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToGibiBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} GiBits", this.GibiBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToGigaByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} GB", this.GigaBytes, maxDecimalsCount, numberFormatProvider);
        }

        public string ToGibiByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} GiB", this.GibiBytes, maxDecimalsCount, numberFormatProvider);
        }

        // Tera
        public string ToTeraBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
			return ToString("{0} TBits", this.TeraBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToTebiBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} TiBits", this.TebiBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToTeraByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} TB", this.TeraBytes, maxDecimalsCount, numberFormatProvider);
        }

        public string ToTebiByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} TiB", this.TebiBytes, maxDecimalsCount, numberFormatProvider);
        }

        // Peta
        public string ToPetaBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} PBits", this.PetaBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToPebiBitString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} PiBits", this.PebiBits, maxDecimalsCount, numberFormatProvider);
        }

        public string ToPetaByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} PB", this.PetaBytes, maxDecimalsCount, numberFormatProvider);
        }

        public string ToPebiByteString(int maxDecimalsCount, IFormatProvider numberFormatProvider = null)
        {
            return ToString("{0} PiB", this.PebiBytes, maxDecimalsCount, numberFormatProvider);
        }
		#endregion METHODS		END
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			if (obj is DataSize)
				return Equals((DataSize)obj);
			else
				return false;
		}
		
		public bool Equals(DataSize other)
		{
			return this._bytes == other._bytes;
		}
		
		public override int GetHashCode()
		{
			return _bytes.GetHashCode();
		}
		#endregion

        #region IComparable<DataSize> Members
        public int CompareTo(DataSize other)
        {
            return Math.Sign(this._bytes - other._bytes);
        }
        #endregion

        #region ---------------------------------		OPERATOR OVERRIDE		----------------------------------------
        public static bool operator ==(DataSize left, DataSize right)
        {
            return (left._bytes == right._bytes);
        }
        public static bool operator !=(DataSize left, DataSize right)
        {
            return (left._bytes != right._bytes);
        }

        public static DataSize operator +(DataSize left, DataSize right)
        {
            return new DataSize(left._bytes + right._bytes);
        }
        public static DataSize operator -(DataSize left, DataSize right)
        {
            return new DataSize(left._bytes - right._bytes);
        }
        
        public static bool operator >(DataSize left, DataSize right)
        {
            return left._bytes > right._bytes;
        }
        public static bool operator >=(DataSize left, DataSize right)
        {
            return left._bytes >= right._bytes;
        }
        public static bool operator <(DataSize left, DataSize right)
        {
            return left._bytes < right._bytes;
        }
        public static bool operator <=(DataSize left, DataSize right)
        {
            return left._bytes <= right._bytes;
        }

        public static DataTransferSpeed operator /(DataSize ds, TimeSpan ts)
        {
            return new DataTransferSpeed(ds, ts);
        }

        public static TimeSpan operator /(DataSize ds, DataTransferSpeed dts)
        {
            return TimeSpan.FromSeconds(ds._bytes / dts.BytesPerSec);
        }
        #endregion ---------------------------------		OPERATOR OVERRIDE	END
    }
}
