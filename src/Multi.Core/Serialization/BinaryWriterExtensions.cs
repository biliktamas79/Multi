using System;
using System.IO;

namespace Multi.Serialization
{
    public static partial class BinaryWriterExtensions
    {
        public static void Write<T>(this BinaryWriter writer, Nullable<T> value, Action<T> writeNonNullValue)
            where T : struct
        {
            if (value == null)
                writer.Write(false);
            else
            {
                if (writeNonNullValue == null)
                    throw new ArgumentNullException("writeNonNullValue");

                writer.Write(true);
                writeNonNullValue(value.Value);
            }
        }
        public static void Write<T>(this BinaryWriter writer, T value, Action<T> writeNonNullValue)
            where T : class
        {
            if (value == null)
                writer.Write(false);
            else
            {
                if (writeNonNullValue == null)
                    throw new ArgumentNullException("writeNonNullValue");

                writer.Write(true);
                writeNonNullValue(value);
            }
        }
        private static bool WriteIsNotNullBit<T>(BinaryWriter writer, T? value)
            where T : struct
        {
            if (value == null)
            {
                writer.Write(false);
                return false;
            }
            else
            {
                writer.Write(true);
                return true;
            }
        }

        public static void Write(this BinaryWriter writer, DateTime value)
        {
            writer.Write((byte)value.Kind);
            writer.Write((long)value.Ticks);
        }
        public static void Write(this BinaryWriter writer, Guid value)
        {
            writer.Write(value.ToByteArray());
        }

        public static void WriteNullable(this BinaryWriter writer, bool? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, byte? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, sbyte? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, char? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, DateTime? value)
        {
            if (WriteIsNotNullBit(writer, value))
            {
                writer.Write((byte)value.Value.Kind);
                writer.Write((long)value.Value.Ticks);
            }
        }
        public static void WriteNullable(this BinaryWriter writer, decimal? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, double? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, float? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, Guid? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value.ToByteArray());
        }
        public static void WriteNullable(this BinaryWriter writer, Int16? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, UInt16? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, Int32? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, UInt32? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, Int64? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, UInt64? value)
        {
            if (WriteIsNotNullBit(writer, value))
                writer.Write(value.Value);
        }
        public static void WriteNullable(this BinaryWriter writer, string value)
        {
            if (value == null)
                writer.Write(false);
            else
            {
                writer.Write(true);
                writer.Write(value);
            }
        }
    }
}
