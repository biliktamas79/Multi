using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Multi.Serialization
{
    //https://developers.google.com/protocol-buffers/docs/encoding
    public static class ProtocolBuffersEncoding
    {
        #region ZigZag
        public static uint EncodeZigZag32(int n)
        {
            return (uint)((n << 1) ^ (n >> 31));
        }

        public static ulong EncodeZigZag64(long n)
        {
            return (ulong)((n << 1) ^ (n >> 63));
        }

        public static int DecodeZigZag32(uint n)
        {
            return (int)(n >> 1) ^ -(int)(n & 1);
        }

        public static long DecodeZigZag64(ulong n)
        {
            return (long)(n >> 1) ^ -(long)(n & 1);
        }
        #endregion ZigZag

        #region Varint32
        public static UInt32 ReadVarint32(Stream stream)
        {
            int result = 0;
            int offset = 0;

            for (; offset < 32; offset += 7)
            {
                int b = stream.ReadByte();
                if (b == -1)
                    throw new EndOfStreamException();

                result |= (b & 0x7f) << offset;

                if ((b & 0x80) == 0)
                    return (UInt32)result;
            }

            throw new InvalidDataException();
        }
        //public static UInt32 ReadVarint32(System.IO.BinaryReader br)
        //{
        //    //int result = 0;
        //    //int offset = 0;

        //    //for (; offset < 32; offset += 7)
        //    //{
        //    //    int b = br.ReadByte();
        //    //    //if (b == -1)
        //    //    //    throw new EndOfStreamException();

        //    //    result |= (b & 0x7f) << offset;

        //    //    if ((b & 0x80) == 0)
        //    //        return (UInt32)result;
        //    //}

        //    //throw new InvalidDataException();
        //    return ReadVarint32(br.BaseStream);
        //}

        /*  The compression formula is fairly simple. If the value (which is an unsigned integer) is 0x7F or less, it is represented as 1 byte; if the length is greater than 0x7F but no larger than 0x3FFF, it is represented as a 2-byte unsigned integer with the senior bit set. Otherwise, it is represented as a 4-byte unsigned integer with two senior bits set. Table summarizes this formula.
		Table The Length Compression Formula for the Blob
		Value Range	Compressed Size	Compressed Value
		0–0x7F	1 byte	<value>
		0x80–0x3FFF	2 bytes	0x8000 │ <value>
		0x4000–0x1FFFFFFF	4 bytes	0xC0000000 │ <value>
	 */
        public static void WriteVarint32(Stream stream, UInt32 value)
        {
            for (; value >= 0x80u; value >>= 7)
                stream.WriteByte((byte)(value | 0x80u));

            stream.WriteByte((byte)value);
        }
        //public static void WriteVarint32(System.IO.BinaryWriter bw, UInt32 value)
        //{
        //    WriteVarint32(bw.BaseStream, value);
        //}
        #endregion Varint32

        #region Varint64
        public static ulong ReadVarint64(Stream stream)
        {
            long result = 0;
            int offset = 0;

            for (; offset < 64; offset += 7)
            {
                int b = stream.ReadByte();
                if (b == -1)
                    throw new EndOfStreamException();

                result |= ((long)(b & 0x7f)) << offset;

                if ((b & 0x80) == 0)
                    return (ulong)result;
            }

            throw new InvalidDataException();
        }
        //public static ulong ReadVarint64(System.IO.BinaryReader br)
        //{
        //    return ReadVarint64(br.BaseStream);
        //}

        public static void WriteVarint64(Stream stream, ulong value)
        {
            for (; value >= 0x80u; value >>= 7)
                stream.WriteByte((byte)(value | 0x80u));

            stream.WriteByte((byte)value);
        }
        //public static void WriteVarint64(System.IO.BinaryWriter bw, ulong value)
        //{
        //    WriteVarint64(bw.BaseStream, value);
        //}
        #endregion Varint64

        #region Boolean
        public static void Write(Stream stream, bool value)
        {
            stream.WriteByte(value ? (byte)1 : (byte)0);
        }
        //public static void Write(System.IO.BinaryWriter bw, bool value)
        //{
        //    bw.Write(value ? (byte)1 : (byte)0);
        //}

        public static bool ReadBoolean(Stream stream)
        {
            var i = stream.ReadByte();
            if (i == -1)
                throw new EndOfStreamException();
            return i != 0;
        }
        //public static bool ReadBoolean(System.IO.BinaryReader br)
        //{
        //    var b = br.ReadByte();
        //    return b != 0;
        //}
        #endregion Boolean

        #region Boolean?
        public static void Write(Stream stream, bool? value)
        {
            if (value == null)
                stream.WriteByte((byte)0);
            else
                stream.WriteByte(value.Value ? (byte)2 : (byte)1);
        }

        public static bool? ReadNullableBoolean(Stream stream)
        {
            var i = stream.ReadByte();
            if (i == -1)
                throw new EndOfStreamException();
            if (i == 0)
                return null;
            else
                return (i - 1) != 0;
        }
        #endregion Boolean

        #region Byte
        public static void Write(Stream stream, byte value)
        {
            stream.WriteByte(value);
        }
        //public static void Write(System.IO.BinaryWriter bw, byte value)
        //{
        //    bw.Write(value);
        //}

        public static byte ReadByte(Stream stream)
        {
            int i = stream.ReadByte();
            if (i == -1)
                throw new EndOfStreamException();
            return (byte)i;
        }
        //public static byte ReadByte(System.IO.BinaryReader br)
        //{
        //    return br.ReadByte();
        //}
        #endregion Byte

        #region Byte?
        public static void Write(Stream stream, byte? value)
        {
            if (value == null)
                Write(stream, (uint)0);
            else
                Write(stream, ((uint)value.Value) + 1u);
        }

        public static byte? ReadNullableByte(Stream stream)
        {
            uint l = ReadUInt32(stream);
            if (l == 0)
                return null;
            else
                return (byte)(l - 1u);
        }
        #endregion Byte?

        #region SByte
        public static void Write(Stream stream, sbyte value)
        {
            stream.WriteByte(unchecked((byte)value));
        }
        //public static void Write(System.IO.BinaryWriter bw, sbyte value)
        //{
        //    bw.Write(unchecked((byte)value));
        //}

        public static sbyte ReadSByte(Stream stream)
        {
            int i = stream.ReadByte();
            if (i == -1)
                throw new EndOfStreamException();
            byte b = (byte)i;
            return unchecked((sbyte)b);
        }
        //public static sbyte ReadSByte(System.IO.BinaryReader br)
        //{
        //    return unchecked((sbyte)br.ReadByte());
        //}
        #endregion SByte

        #region SByte?
        public static void Write(Stream stream, sbyte? value)
        {
            if (value == null)
                Write(stream, (uint)0);
            else
                Write(stream, ((uint)(unchecked((byte)value.Value))) + 1u);
        }

        public static sbyte? ReadNullableSByte(Stream stream)
        {
            uint l = ReadUInt32(stream);
            if (l == 0)
                return null;
            else
                return (sbyte)(l - 1u);
        }
        #endregion SByte?

        #region Char
        public static void Write(Stream stream, char value)
        {
            WriteVarint32(stream, value);
        }
        //public static void Write(System.IO.BinaryWriter bw, char value)
        //{
        //    WriteVarint32(bw, value);
        //}

        public static char ReadChar(Stream stream)
        {
            return (char)ReadVarint32(stream);
        }
        //public static char ReadChar(System.IO.BinaryReader br)
        //{
        //    return (char)ReadVarint32(br);
        //}
        #endregion Char

        #region Char?
        public static void Write(Stream stream, char? value)
        {
            if (value == null)
                Write(stream, (uint)0);
            else
                Write(stream, ((uint)value.Value) + 1u);
        }

        public static char? ReadNullableChar(Stream stream)
        {
            uint l = ReadUInt32(stream);
            if (l == 0)
                return null;
            else
                return (char)(l - 1u);
        }
        #endregion Char?

        #region UInt16
        public static void Write(Stream stream, UInt16 value)
        {
            WriteVarint32(stream, value);
        }
        //public static void Write(System.IO.BinaryWriter bw, UInt16 value)
        //{
        //    WriteVarint32(bw, value);
        //}

        public static UInt16 ReadUInt16(Stream stream)
        {
            return (UInt16)ReadVarint32(stream);
        }
        //public static UInt16 ReadUInt16(System.IO.BinaryReader br)
        //{
        //    return (UInt16)ReadVarint32(br);
        //}
        #endregion UInt16

        #region UInt16?
        public static void Write(Stream stream, UInt16? value)
        {
            if (value == null)
                Write(stream, (uint)0);
            else
                Write(stream, ((uint)value.Value) + 1);
        }

        public static UInt16? ReadNullableUInt16(Stream stream)
        {
            uint l = ReadUInt32(stream);
            if (l == 0)
                return null;
            else
                return (UInt16)(l - 1);
        }
        #endregion UInt16?

        #region Int16
        public static void Write(Stream stream, Int16 value)
        {
            WriteVarint32(stream, EncodeZigZag32(value));
        }
        //public static void Write(System.IO.BinaryWriter bw, Int16 value)
        //{
        //    WriteVarint32(bw, EncodeZigZag32(value));
        //}

        public static Int16 ReadInt16(Stream stream)
        {
            return (Int16)DecodeZigZag32(ReadVarint32(stream));
        }
        //public static Int16 ReadInt16(System.IO.BinaryReader br)
        //{
        //    return (Int16)DecodeZigZag32(ReadVarint32(br));
        //}
        #endregion Int16

        #region Int16?
        public static void Write(Stream stream, Int16? value)
        {
            if (value == null)
                Write(stream, (int)0);
            else
            {
                Write(stream, ((int)value.Value) + 1);
            }
        }

        public static Int16? ReadNullableInt16(Stream stream)
        {
            int l = ReadInt32(stream);
            if (l == 0)
                return null;
            else
                return (Int16)(l - 1);
        }
        #endregion Int16?

        #region UInt32
        public static void Write(Stream stream, UInt32 value)
        {
            WriteVarint32(stream, value);
        }
        //public static void Write(System.IO.BinaryWriter bw, UInt32 value)
        //{
        //    WriteVarint32(bw, value);
        //}

        public static UInt32 ReadUInt32(Stream stream)
        {
            return ReadVarint32(stream);
        }
        //public static UInt32 ReadUInt32(System.IO.BinaryReader br)
        //{
        //    return ReadVarint32(br);
        //}
        #endregion UInt32

        #region UInt32?
        public static void Write(Stream stream, UInt32? value)
        {
            if (value == null)
                Write(stream, (ulong)0);
            else
            {
                Write(stream, ((ulong)value.Value) + 1);
            }
        }

        public static UInt32? ReadNullableUInt32(Stream stream)
        {
            ulong l = ReadUInt64(stream);
            if (l == 0)
                return null;
            else
                return (UInt32)(l - 1);
        }
        #endregion UInt32?

        #region Int32
        public static void Write(Stream stream, Int32 value)
        {
            WriteVarint32(stream, EncodeZigZag32(value));
        }
        //public static void Write(System.IO.BinaryWriter bw, Int32 value)
        //{
        //    WriteVarint32(bw, EncodeZigZag32(value));
        //}

        public static Int32 ReadInt32(Stream stream)
        {
            return DecodeZigZag32(ReadVarint32(stream));
        }
        //public static Int32 ReadInt32(System.IO.BinaryReader br)
        //{
        //    return DecodeZigZag32(ReadVarint32(br));
        //}
        #endregion Int32

        #region Int32?
        public static void Write(Stream stream, Int32? value)
        {
            if (value == null)
                Write(stream, (long)0);
            else
            {
                Write(stream, ((long)value.Value) + 1);
            }
        }

        public static Int32? ReadNullableInt32(Stream stream)
        {
            long l = ReadInt64(stream);
            if (l == 0)
                return null;
            else
                return (Int32)(l - 1);
        }
        #endregion Int32

        #region UInt64
        public static void Write(Stream stream, UInt64 value)
        {
            WriteVarint64(stream, value);
        }

        public static UInt64 ReadUInt64(Stream stream)
        {
            return ReadVarint64(stream);
        }
        #endregion UInt64

        #region UInt64?
        public static void Write(Stream stream, UInt64? value)
        {
            if (value == null)
                Write(stream, false);
            else
            {
                Write(stream, true);
                Write(stream, value.Value);
            }
        }

        public static UInt64? ReadNullableUInt64(Stream stream)
        {
            if (ReadBoolean(stream))
                return ReadUInt64(stream);
            else
                return null;
        }
        #endregion UInt64?

        #region Int64
        public static void Write(Stream stream, Int64 value)
        {
            WriteVarint64(stream, EncodeZigZag64(value));
        }

        public static Int64 ReadInt64(Stream stream)
        {
            return DecodeZigZag64(ReadVarint64(stream));
        }
        #endregion Int64

        #region Int64?
        public static void Write(Stream stream, Int64? value)
        {
            if (value == null)
                Write(stream, false);
            else
            {
                Write(stream, true);
                Write(stream, value.Value);
            }
        }

        public static Int64? ReadNullableInt64(Stream stream)
        {
            if (ReadBoolean(stream))
                return ReadInt64(stream);
            else
                return null;
        }
        #endregion Int64?

        //#if UNSAFE
        //        #region Float
        //        public static unsafe void Write(Stream stream, float value)
        //        {
        //            uint v = *(uint*)(&value);
        //            WriteVarint32(stream, v);
        //        }

        //        public static unsafe float ReadFloat(Stream stream)
        //        {
        //            uint v = ReadVarint32(stream);
        //            return *(float*)(&v);
        //        }
        //        #endregion Float

        //        #region Double
        //        public static unsafe void Write(Stream stream, double value)
        //        {
        //            ulong v = *(ulong*)(&value);
        //            WriteVarint64(stream, v);
        //        }

        //        public static unsafe double ReadDouble(Stream stream)
        //        {
        //            ulong v = ReadVarint64(stream);
        //            return *(double*)(&v);
        //        }
        //        #endregion Double
        //#else
        #region Float
        public static void Write(Stream stream, float value)
        {
            Write(stream, (double)value);
            //WriteVarint32(stream, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }

        public static float ReadFloat(Stream stream)
        {
            return (float)ReadDouble(stream);
            //return BitConverter.ToSingle(BitConverter.GetBytes(ReadVarint32(stream)), 0);
        }
        #endregion Float

        #region Float?
        public static void Write(Stream stream, float? value)
        {
            if (value == null)
                Write(stream, false);
            else
            {
                Write(stream, true);
                Write(stream, value.Value);
            }
        }

        public static float? ReadNullableFloat(Stream stream)
        {
            if (ReadBoolean(stream))
                return ReadFloat(stream);
            else
                return null;
        }
        #endregion Float?

        #region Double
        public static void Write(Stream stream, double value)
        {
            ulong v = (ulong)BitConverter.DoubleToInt64Bits(value);
            WriteVarint64(stream, v);
        }

        public static double ReadDouble(Stream stream)
        {
            ulong v = ReadVarint64(stream);
            return BitConverter.Int64BitsToDouble((long)v);
        }
        #endregion Double

        #region Double?
        public static void Write(Stream stream, double? value)
        {
            if (value == null)
                Write(stream, false);
            else
            {
                Write(stream, true);
                Write(stream, value.Value);
            }
        }

        public static double? ReadNullableDouble(Stream stream)
        {
            if (ReadBoolean(stream))
                return ReadDouble(stream);
            else
                return null;
        }
        #endregion Double?

        #region Decimal
        public static void Write(Stream stream, decimal value)
        {
            foreach (Int32 i in Decimal.GetBits(value))
                Write(stream, i);
        }

        public static decimal ReadDecimal(Stream stream)
        {
            int[] bits = new int[4];
            bits[0] = ReadInt32(stream);
            bits[1] = ReadInt32(stream);
            bits[2] = ReadInt32(stream);
            bits[3] = ReadInt32(stream);
            return new decimal(bits);
        }
        #endregion Decimal

        #region Decimal?
        public static void Write(Stream stream, decimal? value)
        {
            if (value == null)
                Write(stream, false);
            else
            {
                Write(stream, true);
                Write(stream, value.Value);
            }
        }

        public static decimal? ReadNullableDecimal(Stream stream)
        {
            if (ReadBoolean(stream))
                return ReadDecimal(stream);
            else
                return null;
        }
        #endregion Decimal?
        //#endif

        #region DateTime
        public static void Write(Stream stream, DateTime value)
        {
            //long v = value.ToBinary();
            //Write(stream, v);
            Write(stream, (byte)value.Kind);
            switch (value.Kind)
            {
                case DateTimeKind.Utc:
                    Write(stream, value.Ticks);
                    break;

                case DateTimeKind.Local:
                    Write(stream, value.ToUniversalTime().Ticks);
                    break;

                case DateTimeKind.Unspecified:
                    Write(stream, value.Ticks);
                    break;

                default:
                    throw NotSupportedValueException.New(value.Kind);
            }
        }

        public static DateTime ReadDateTime(Stream stream)
        {
            //long v = ReadInt64(stream);
            //return DateTime.FromBinary(v);
            int b = stream.ReadByte();
            if (b == -1)
                throw new EndOfStreamException();

            DateTimeKind kind = (DateTimeKind)b;
            switch (kind)
            {
                case DateTimeKind.Utc:
                    return new DateTime(ReadInt64(stream), kind);
                //break;

                case DateTimeKind.Local:
                    return new DateTime(ReadInt64(stream), DateTimeKind.Utc).ToLocalTime();
                //break;

                case DateTimeKind.Unspecified:
                    return new DateTime(ReadInt64(stream), DateTimeKind.Unspecified);
                //break;

                default:
                    throw NotSupportedValueException.New(kind);
            }
        }
        #endregion DateTime

        #region DateTime?
        public static void Write(Stream stream, DateTime? value)
        {
            //if (value == null)
            //    Write(stream, false);
            //else
            //{
            //    Write(stream, true);
            //    Write(stream, value.Value);
            //}

            if (value == null)
                Write(stream, (byte)0);
            else
            {
                Write(stream, (byte)((byte)value.Value.Kind + (byte)1));
                switch (value.Value.Kind)
                {
                    case DateTimeKind.Utc:
                        Write(stream, value.Value.Ticks);
                        break;

                    case DateTimeKind.Local:
                        Write(stream, value.Value.ToUniversalTime().Ticks);
                        break;

                    case DateTimeKind.Unspecified:
                        Write(stream, value.Value.Ticks);
                        break;

                    default:
                        throw NotSupportedValueException.New(value.Value.Kind);
                }
            }
        }

        public static DateTime? ReadNullableDateTime(Stream stream)
        {
            //if (ReadBoolean(stream))
            //    return ReadDateTime(stream);
            //else
            //    return null;

            var b = stream.ReadByte();
            if (b == -1)
                throw new EndOfStreamException();
            if (b == 0)
                return null;
            else
            {
                DateTimeKind kind = (DateTimeKind)((byte)(b - 1));
                switch (kind)
                {
                    case DateTimeKind.Utc:
                        return new DateTime(ReadInt64(stream), kind);
                    //break;

                    case DateTimeKind.Local:
                        return new DateTime(ReadInt64(stream), DateTimeKind.Utc).ToLocalTime();
                    //break;

                    case DateTimeKind.Unspecified:
                        return new DateTime(ReadInt64(stream), DateTimeKind.Unspecified);
                    //break;

                    default:
                        throw NotSupportedValueException.New(kind);
                }
            }
        }
        #endregion DateTime?

        #region String
        public static void Write(Stream stream, string value)
        {
            if (value == null)
                Write(stream, (uint)0);
            else
            {
                Write(stream, ((uint)value.Length) + 1);

                foreach (char c in value)
                    Write(stream, c);
            }
        }

        public static string ReadString(Stream stream)
        {
            uint len = ReadUInt32(stream);
            if (len == 0)
                return null;
            else if (len == 1)
                return string.Empty;

            len -= 1;

            var arr = new char[len];
            for (uint i = 0; i < len; ++i)
                arr[i] = ReadChar(stream);

            return new string(arr);
        }
        #endregion String

        #region byte[]
        public static void Write(Stream stream, byte[] value)
        {
            if (value == null)
                Write(stream, (uint)0);
            else
            {
                Write(stream, ((uint)value.Length) + 1);
                stream.Write(value, 0, value.Length);
            }
        }

        private static readonly byte[] _emptyByteArray = new byte[0];

        public static byte[] ReadByteArray(Stream stream)
        {
            uint len = ReadUInt32(stream);
            if (len == 0)
                return null;
            else if (len == 1)
                return _emptyByteArray;

            len -= 1;

            byte[] value = new byte[len];
            int index = 0;
            while (index < len)
            {
                int r = stream.Read(value, index, (int)len - index);
                if (r == 0)
                    throw new EndOfStreamException();
                index += r;
            }
            return value;
        }
        #endregion byte[]

        #region Guid
        public static void Write(Stream stream, Guid value)
        {
            stream.Write(value.ToByteArray(), 0, 16);
        }

        public static Guid ReadGuid(Stream stream)
        {
            byte[] b = new byte[16];
            int index = 0;
            while (index < 16)
            {
                int r = stream.Read(b, index, (int)16 - index);
                if (r == 0)
                    throw new EndOfStreamException();
                index += r;
            }
            return new Guid(b);
        }
        #endregion Guid

        #region Guid?
        public static void Write(Stream stream, Guid? value)
        {
            if (value == null)
                Write(stream, false);
            else
            {
                Write(stream, true);
                Write(stream, value.Value);
            }
        }

        public static Guid? ReadNullableGuid(Stream stream)
        {
            if (ReadBoolean(stream))
                return ReadGuid(stream);
            else
                return null;
        }
        #endregion Guid?

    }
}
