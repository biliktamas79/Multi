using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Multi.Serialization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Multi.Core.Tests.Unit.Serialization
{
    [TestClass]
    public class ProtocolBuffersEncodingStreamReaderWriterUnitTests
    {
        [TestMethod]
        public void ProtocolBuffersEncodingStreamReaderWriter_can_write_then_read_back_basic_types()
        {
            using (var ms = new System.IO.MemoryStream())
            {
                // boolean
                WriteReadCompare<bool>(ms, new bool[] { true, false }, (writer, value) => writer.Write(value), reader => reader.ReadBoolean());
                WriteReadCompare<bool?>(ms, new bool?[] { null, true, false }, (writer, value) => writer.Write(value), reader => reader.ReadNullableBoolean());

                // integers
                WriteReadCompare<byte>(ms, SerializationTestGlobals.ByteValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadByte());
                WriteReadCompare<byte?>(ms, SerializationTestGlobals.ByteValuesToCheck.Cast<byte?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableByte());

                WriteReadCompare<sbyte>(ms, SerializationTestGlobals.SByteValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadSByte());
                WriteReadCompare<sbyte?>(ms, SerializationTestGlobals.SByteValuesToCheck.Cast<sbyte?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableSByte());

                WriteReadCompare<short>(ms, SerializationTestGlobals.Int16ValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadInt16());
                WriteReadCompare<short?>(ms, SerializationTestGlobals.Int16ValuesToCheck.Cast<short?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableInt16());

                WriteReadCompare<ushort>(ms, SerializationTestGlobals.UInt16ValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadUInt16());
                WriteReadCompare<ushort?>(ms, SerializationTestGlobals.UInt16ValuesToCheck.Cast<ushort?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableUInt16());

                WriteReadCompare<int>(ms, SerializationTestGlobals.Int32ValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadInt32());
                WriteReadCompare<int?>(ms, SerializationTestGlobals.Int32ValuesToCheck.Cast<int?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableInt32());

                WriteReadCompare<uint>(ms, SerializationTestGlobals.UInt32ValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadUInt32());
                WriteReadCompare<uint?>(ms, SerializationTestGlobals.UInt32ValuesToCheck.Cast<uint?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableUInt32());

                WriteReadCompare<long>(ms, SerializationTestGlobals.Int64ValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadInt64());
                WriteReadCompare<long?>(ms, SerializationTestGlobals.Int64ValuesToCheck.Cast<long?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableInt64());

                WriteReadCompare<ulong>(ms, SerializationTestGlobals.UInt64ValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadUInt64());
                WriteReadCompare<ulong?>(ms, SerializationTestGlobals.UInt64ValuesToCheck.Cast<ulong?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableUInt64());

                // decimals
                WriteReadCompare<float>(ms, SerializationTestGlobals.FloatValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadFloat());
                WriteReadCompare<float?>(ms, SerializationTestGlobals.FloatValuesToCheck.Cast<float?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableFloat());

                WriteReadCompare<double>(ms, SerializationTestGlobals.DoubleValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadDouble());
                WriteReadCompare<double?>(ms, SerializationTestGlobals.DoubleValuesToCheck.Cast<double?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableDouble());

                WriteReadCompare<decimal>(ms, SerializationTestGlobals.DecimalValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadDecimal());
                WriteReadCompare<decimal?>(ms, SerializationTestGlobals.DecimalValuesToCheck.Cast<decimal?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableDecimal());

                // char & string
                WriteReadCompare<char>(ms, SerializationTestGlobals.CharValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadChar());
                WriteReadCompare<char?>(ms, SerializationTestGlobals.CharValuesToCheck.Cast<char?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableChar());

                WriteReadCompare<string>(ms, SerializationTestGlobals.StringValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadString(), StringComparer.InvariantCulture);

                // DateTime
                WriteReadCompare<DateTime>(ms, SerializationTestGlobals.DateTimeValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadDateTime());
                WriteReadCompare<DateTime?>(ms, SerializationTestGlobals.DateTimeValuesToCheck.Cast<DateTime?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableDateTime());

                // Guid
                WriteReadCompare<Guid>(ms, SerializationTestGlobals.GuidValuesToCheck, (writer, value) => writer.Write(value), reader => reader.ReadGuid());
                WriteReadCompare<Guid?>(ms, SerializationTestGlobals.GuidValuesToCheck.Cast<Guid?>().Append(null), (writer, value) => writer.Write(value), reader => reader.ReadNullableGuid());

                // array
                WriteReadCompare<byte[]>(ms, new byte[][] { SerializationTestGlobals.ByteValuesToCheck, null, new byte[]{ } }, (writer, value) => writer.Write(value), reader => reader.ReadByteArray(), ArrayComparer<byte>.Default);
            }
        }

        private static void WriteReadCompare<T>(Stream stream, IEnumerable<T> values, Action<ProtocolBuffersEncodingStreamReaderWriter, T> write, Func<ProtocolBuffersEncodingStreamReaderWriter, T> read, IEqualityComparer<T> comparer = null)
        {
            var readerWriter = new ProtocolBuffersEncodingStreamReaderWriter(stream);

            foreach (var value in values)
            {
                stream.Position = 0;
                write(readerWriter, value);
                var positionAfterWrite = stream.Position;

                stream.Position = 0;
                var readValue = read(readerWriter);
                var positionAfterRead = stream.Position;

                if (comparer == null)
                    Assert.AreEqual<T>(value, readValue, "Assert.AreEqual<{0}> failed. Expected: <{1}>. Actual: <{2}>.", 
                        typeof(T).GetFriendlyTypeName(), 
                        (value == null) ? "(null)" : value.ToString(), 
                        (readValue == null) ? "(null)" : readValue.ToString());
                else
                    Assert.IsTrue(comparer.Equals(value, readValue), "Deserialized value does not equal the original serialized one.");

                Assert.AreEqual(positionAfterWrite, positionAfterRead, "Position after read does not equal to position after write.");
            }
        }
    }
}
