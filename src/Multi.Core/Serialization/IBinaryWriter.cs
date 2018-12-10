using System;
using System.IO;

namespace Multi.Serialization
{
    public interface IBinaryWriter
    {
        void Write(bool value);
        void Write(bool? value);
        void Write(byte value);
        void Write(byte? value);
        void Write(sbyte value);
        void Write(sbyte? value);
        void Write(Int16 value);
        void Write(Int16? value);
        void Write(UInt16 value);
        void Write(UInt16? value);
        void Write(Int32 value);
        void Write(Int32? value);
        void Write(UInt32 value);
        void Write(UInt32? value);
        void Write(Int64 value);
        void Write(Int64? value);
        void Write(UInt64 value);
        void Write(UInt64? value);
        void Write(float value);
        void Write(float? value);
        void Write(double value);
        void Write(double? value);
        void Write(decimal value);
        void Write(decimal? value);
        void Write(char value);
        void Write(char? value);
        void Write(string value);
        void Write(DateTime value);
        void Write(DateTime? value);
        void Write(Guid value);
        void Write(Guid? value);
        void Write(byte[] value);
        void WriteLengthPrefixed(Stream stream, int bufferSize);

        void Flush();
    }
}
