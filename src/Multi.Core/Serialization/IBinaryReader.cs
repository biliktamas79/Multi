using System;
using System.IO;

namespace Multi.Serialization
{
    public interface IBinaryReader
    {
        bool ReadBoolean();
        bool? ReadNullableBoolean();

        byte ReadByte();
        byte? ReadNullableByte();

        sbyte ReadSByte();
        sbyte? ReadNullableSByte();

        Int16 ReadInt16();
        Int16? ReadNullableInt16();

        UInt16 ReadUInt16();
        UInt16? ReadNullableUInt16();

        Int32 ReadInt32();
        Int32? ReadNullableInt32();

        UInt32 ReadUInt32();
        UInt32? ReadNullableUInt32();

        Int64 ReadInt64();
        Int64? ReadNullableInt64();

        UInt64 ReadUInt64();
        UInt64? ReadNullableUInt64();

        float ReadFloat();
        float? ReadNullableFloat();

        double ReadDouble();
        double? ReadNullableDouble();

        decimal ReadDecimal();
        decimal? ReadNullableDecimal();

        char ReadChar();
        char? ReadNullableChar();

        string ReadString();
        //string? ReadNullableString();

        DateTime ReadDateTime();
        DateTime? ReadNullableDateTime();

        Guid ReadGuid();
        Guid? ReadNullableGuid();

        byte[] ReadByteArray();
        //int ReadByteArrayBuffered(byte[] buffer);

        Stream ReadLengthPrefixedStream(int bufferSize);
    }
}
