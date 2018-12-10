using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Multi.Serialization
{
    public class ProtocolBuffersEncodingStreamReaderWriter : IBinaryReader, IBinaryWriter
    {
        private Stream _stream;

        //private bool _isStreamOwner;
        //public bool IsStreamOwner
        //{
        //    get { return _isStreamOwner; }
        //    //set { _isStreamOwner = value; }
        //}

        #region CONSTRUCTORS
        public ProtocolBuffersEncodingStreamReaderWriter(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            _stream = stream;
        }
        #endregion CONSTRUCTORS

        #region IBinaryReader Members
        public bool ReadBoolean()
        {
            return ProtocolBuffersEncoding.ReadBoolean(_stream);
        }

        public bool? ReadNullableBoolean()
        {
            return ProtocolBuffersEncoding.ReadNullableBoolean(_stream);
        }

        public byte ReadByte()
        {
            return ProtocolBuffersEncoding.ReadByte(_stream);
        }

        public byte? ReadNullableByte()
        {
            return ProtocolBuffersEncoding.ReadNullableByte(_stream);
        }

        public sbyte ReadSByte()
        {
            return ProtocolBuffersEncoding.ReadSByte(_stream);
        }

        public sbyte? ReadNullableSByte()
        {
            return ProtocolBuffersEncoding.ReadNullableSByte(_stream);
        }

        public short ReadInt16()
        {
            return ProtocolBuffersEncoding.ReadInt16(_stream);
        }

        public short? ReadNullableInt16()
        {
            return ProtocolBuffersEncoding.ReadNullableInt16(_stream);
        }

        public ushort ReadUInt16()
        {
            return ProtocolBuffersEncoding.ReadUInt16(_stream);
        }

        public ushort? ReadNullableUInt16()
        {
            return ProtocolBuffersEncoding.ReadNullableUInt16(_stream);
        }

        public int ReadInt32()
        {
            return ProtocolBuffersEncoding.ReadInt32(_stream);
        }

        public int? ReadNullableInt32()
        {
            return ProtocolBuffersEncoding.ReadNullableInt32(_stream);
        }

        public uint ReadUInt32()
        {
            return ProtocolBuffersEncoding.ReadUInt32(_stream);
        }

        public uint? ReadNullableUInt32()
        {
            return ProtocolBuffersEncoding.ReadNullableUInt32(_stream);
        }

        public long ReadInt64()
        {
            return ProtocolBuffersEncoding.ReadInt64(_stream);
        }

        public long? ReadNullableInt64()
        {
            return ProtocolBuffersEncoding.ReadNullableInt64(_stream);
        }

        public ulong ReadUInt64()
        {
            return ProtocolBuffersEncoding.ReadUInt64(_stream);
        }

        public ulong? ReadNullableUInt64()
        {
            return ProtocolBuffersEncoding.ReadNullableUInt64(_stream);
        }

        public float ReadFloat()
        {
            return ProtocolBuffersEncoding.ReadFloat(_stream);
        }

        public float? ReadNullableFloat()
        {
            return ProtocolBuffersEncoding.ReadNullableFloat(_stream);
        }

        public double ReadDouble()
        {
            return ProtocolBuffersEncoding.ReadDouble(_stream);
        }

        public double? ReadNullableDouble()
        {
            return ProtocolBuffersEncoding.ReadNullableDouble(_stream);
        }

        public decimal ReadDecimal()
        {
            return ProtocolBuffersEncoding.ReadDecimal(_stream);
        }

        public decimal? ReadNullableDecimal()
        {
            return ProtocolBuffersEncoding.ReadNullableDecimal(_stream);
        }

        public char ReadChar()
        {
            return ProtocolBuffersEncoding.ReadChar(_stream);
        }

        public char? ReadNullableChar()
        {
            return ProtocolBuffersEncoding.ReadNullableChar(_stream);
        }

        public string ReadString()
        {
            return ProtocolBuffersEncoding.ReadString(_stream);
        }

        public DateTime ReadDateTime()
        {
            return ProtocolBuffersEncoding.ReadDateTime(_stream);
        }

        public DateTime? ReadNullableDateTime()
        {
            return ProtocolBuffersEncoding.ReadNullableDateTime(_stream);
        }

        public Guid ReadGuid()
        {
            return ProtocolBuffersEncoding.ReadGuid(_stream);
        }

        public Guid? ReadNullableGuid()
        {
            return ProtocolBuffersEncoding.ReadNullableGuid(_stream);
        }

        public byte[] ReadByteArray()
        {
            return ProtocolBuffersEncoding.ReadByteArray(_stream);
        }

        public Stream ReadLengthPrefixedStream(int bufferSize)
        {
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException("bufferSize", string.Format("The provided value '{0}' is not a positive integer.", bufferSize));

            var length = ProtocolBuffersEncoding.ReadUInt64(_stream);
            if (length > int.MaxValue)
                throw new InvalidOperationException("Cannot read length prefixed stream with size more than int.MaxValue.");
                //throw new ArgumentOutOfRangeException(string.Empty, "Cannot read length prefixed stream with size more than int.MaxValue.");

            // TODO implement using http://www.codeproject.com/Articles/13061/StreamMuxer
            var ms = new MemoryStream((int)length);
            _stream.CopyTo(ms, bufferSize);
            return ms;
        }
        #endregion

        #region IBinaryWriter Members
        public void Write(bool value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(bool? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(byte value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(byte? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(sbyte value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(sbyte? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(short value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(short? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(ushort value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(ushort? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(int value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(int? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(uint value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(uint? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(long value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(long? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(ulong value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(ulong? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(float value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(float? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(double value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(double? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(decimal value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(decimal? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(char value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(char? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(string value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(DateTime value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(DateTime? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(Guid value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(Guid? value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void Write(byte[] value)
        {
            ProtocolBuffersEncoding.Write(_stream, value);
        }

        public void WriteLengthPrefixed(Stream stream, int bufferSize)
        {
            Throw.IfArgumentIsNull(stream, nameof(stream));
            if (stream.Length > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(stream), "Cannot write length prefixed stream with size more than int.MaxValue.");

            long srcPositionBefore = stream.Position;
            long sizeToCopy = Math.Max(0, stream.Length - srcPositionBefore);
            Write(sizeToCopy);
            if (sizeToCopy > 0)
            {
                stream.CopyTo(_stream, bufferSize); // TODO: this can copy more than the sizeToCopy value if stream grows during te copying!
                var copiedSize = stream.Position - srcPositionBefore;
                if (copiedSize > sizeToCopy)
                    throw new IOException("The source stream length increased during copying!");
                if (copiedSize < sizeToCopy)
                    throw new IOException(string.Format(CultureInfo.InvariantCulture, "Could not copy all the {0} bytes from the source stream!", sizeToCopy));
            }
        }

        public void Flush()
        {
            _stream.Flush();
        }
        #endregion
    }
}
