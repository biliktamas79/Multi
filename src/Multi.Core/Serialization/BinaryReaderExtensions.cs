using System;
using System.IO;

namespace Multi.Serialization
{
    public static partial class BinaryReaderExtensions
    {
        public static bool? ReadNullableBoolean(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadBoolean();
            else
                return null;
        }
        public static byte? ReadNullableByte(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadByte();
            else
                return null;
        }
        public static sbyte? ReadNullableSByte(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadSByte();
            else
                return null;
        }
        public static char? ReadNullableChar(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadChar();
            else
                return null;
        }
        public static DateTime? ReadNullableDateTime(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadDateTime();
            else
                return null;
        }
        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            //long ticks = reader.ReadInt64();
            DateTimeKind kind = (DateTimeKind)reader.ReadByte();
            //return new DateTime(ticks, kind);
            return new DateTime(reader.ReadInt64(), kind);
        }
        public static decimal? ReadNullableDecimal(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadDecimal();
            else
                return null;
        }
        public static double? ReadNullableDouble(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadDouble();
            else
                return null;
        }
        public static float? ReadNullableFloat(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadSingle();
            else
                return null;
        }
        public static Guid? ReadNullableGuid(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadGuid();
            else
                return null;
        }
        public static Guid ReadGuid(this BinaryReader reader)
        {
            return new Guid(reader.ReadBytes(16));
        }
        public static Int16? ReadNullableInt16(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadInt16();
            else
                return null;
        }
        public static UInt16? ReadNullableUInt16(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadUInt16();
            else
                return null;
        }
        public static Int32? ReadNullableInt32(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadInt32();
            else
                return null;
        }
        public static UInt32? ReadNullableUInt32(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadUInt32();
            else
                return null;
        }
        public static Int64? ReadNullableInt64(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadInt64();
            else
                return null;
        }
        public static UInt64? ReadNullableUInt64(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadUInt64();
            else
                return null;
        }
        public static string ReadNullableString(this BinaryReader reader)
        {
            if (reader.ReadBoolean())
                return reader.ReadString();
            else
                return null;
        }
    }
}
