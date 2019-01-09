using System;
using System.Collections.Generic;
using System.Text;

namespace Multi.Core.Tests.Unit.Serialization
{
    internal static class SerializationTestGlobals
    {
        internal static readonly byte[] ByteValuesToCheck = new byte[]
                {
                    byte.MinValue,
                    byte.MinValue + 1,
                    byte.MinValue + 2,
                    byte.MinValue + 3,
                    byte.MinValue + 4,

                    5, 6, 7, 8, 9, 10, 99, 100, 101,

                    (byte)sbyte.MaxValue - 2,
                    (byte)sbyte.MaxValue - 1,
                    (byte)sbyte.MaxValue,
                    (byte)sbyte.MaxValue + 1,
                    (byte)sbyte.MaxValue + 2,

                    byte.MaxValue - 4,
                    byte.MaxValue - 3,
                    byte.MaxValue - 2,
                    byte.MaxValue - 1,
                    byte.MaxValue,
                };
        internal static readonly sbyte[] SByteValuesToCheck = new sbyte[]
                {
                    sbyte.MinValue,
                    sbyte.MinValue + 1,
                    sbyte.MinValue + 2,
                    sbyte.MinValue + 3,
                    sbyte.MinValue + 4,

                    -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -99, -100, -101,
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 99, 100, 101,

                    sbyte.MaxValue - 4,
                    sbyte.MaxValue - 3,
                    sbyte.MaxValue - 2,
                    sbyte.MaxValue - 1,
                    sbyte.MaxValue,
                };

        internal static readonly ushort[] UInt16ValuesToCheck = new ushort[]
                {
                    ushort.MinValue,
                    ushort.MinValue + 1,
                    ushort.MinValue + 2,
                    ushort.MinValue + 3,
                    ushort.MinValue + 4,

                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 99, 100, 101, 999, 1000, 1001, 9999, 10000, 10001,

                    (ushort)sbyte.MaxValue - 2,
                    (ushort)sbyte.MaxValue - 1,
                    (ushort)sbyte.MaxValue,
                    (ushort)sbyte.MaxValue + 1,
                    (ushort)sbyte.MaxValue + 2,

                    (ushort)byte.MaxValue - 2,
                    (ushort)byte.MaxValue - 1,
                    (ushort)byte.MaxValue,
                    (ushort)byte.MaxValue + 1,
                    (ushort)byte.MaxValue + 2,

                    ushort.MaxValue - 4,
                    ushort.MaxValue - 3,
                    ushort.MaxValue - 2,
                    ushort.MaxValue - 1,
                    ushort.MaxValue,
                };
        internal static readonly short[] Int16ValuesToCheck = new short[]
                {
                    short.MinValue,
                    short.MinValue + 1,
                    short.MinValue + 2,
                    short.MinValue + 3,
                    short.MinValue + 4,

                    (short)sbyte.MinValue - 2,
                    (short)sbyte.MinValue - 1,
                    (short)sbyte.MinValue,
                    (short)sbyte.MinValue + 1,
                    (short)sbyte.MinValue + 2,

                    -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -99, -100, -101, -999, -1000, -1001, -9999, -10000, -10001,
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 99, 100, 101, 999, 1000, 1001, 9999, 10000, 10001,

                    (short)sbyte.MaxValue - 2,
                    (short)sbyte.MaxValue - 1,
                    (short)sbyte.MaxValue,
                    (short)sbyte.MaxValue + 1,
                    (short)sbyte.MaxValue + 2,

                    (short)byte.MaxValue - 2,
                    (short)byte.MaxValue - 1,
                    (short)byte.MaxValue,
                    (short)byte.MaxValue + 1,
                    (short)byte.MaxValue + 2,

                    short.MaxValue - 4,
                    short.MaxValue - 3,
                    short.MaxValue - 2,
                    short.MaxValue - 1,
                    short.MaxValue,
                };

        internal static readonly uint[] UInt32ValuesToCheck = new uint[]
                {
                    uint.MinValue,
                    uint.MinValue + 1,
                    uint.MinValue + 2,
                    uint.MinValue + 3,
                    uint.MinValue + 4,

                    0u, 1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u, 99u, 100u, 101u, 999u, 1000u, 1001u, 9999u, 10000u, 10001u, 99999u, 100000u, 100001u,

                    (uint)sbyte.MaxValue - 2,
                    (uint)sbyte.MaxValue - 1,
                    (uint)sbyte.MaxValue,
                    (uint)sbyte.MaxValue + 1,
                    (uint)sbyte.MaxValue + 2,

                    (uint)byte.MaxValue - 2,
                    (uint)byte.MaxValue - 1,
                    (uint)byte.MaxValue,
                    (uint)byte.MaxValue + 1,
                    (uint)byte.MaxValue + 2,

                    (uint) Int16.MaxValue - 2,
                    (uint) Int16.MaxValue - 1,
                    (uint) Int16.MaxValue,
                    (uint) Int16.MaxValue + 1,
                    (uint) Int16.MaxValue + 2,

                    (uint) UInt16.MaxValue - 2,
                    (uint) UInt16.MaxValue - 1,
                    (uint) UInt16.MaxValue,
                    (uint) UInt16.MaxValue + 1,
                    (uint) UInt16.MaxValue + 2,

                    uint.MaxValue - 4,
                    uint.MaxValue - 3,
                    uint.MaxValue - 2,
                    uint.MaxValue - 1,
                    uint.MaxValue,
                };
        internal static readonly int[] Int32ValuesToCheck = new int[]
                {
                    int.MinValue,
                    int.MinValue + 1,
                    int.MinValue + 2,
                    int.MinValue + 3,
                    int.MinValue + 4,

                    (int) Int16.MinValue - 2,
                    (int) Int16.MinValue - 1,
                    (int) Int16.MinValue,
                    (int) Int16.MinValue + 1,
                    (int) Int16.MinValue + 2,

                    (int)sbyte.MinValue - 2,
                    (int)sbyte.MinValue - 1,
                    (int)sbyte.MinValue,
                    (int)sbyte.MinValue + 1,
                    (int)sbyte.MinValue + 2,

                    -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -99, -100, -101, -999, -1000, -1001, -9999, -10000, -10001, -99999, -100000, -100001,
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 99, 100, 101, 999, 1000, 1001, 9999, 10000, 10001, 99999, 100000, 100001,

                    (int)sbyte.MaxValue - 2,
                    (int)sbyte.MaxValue - 1,
                    (int)sbyte.MaxValue,
                    (int)sbyte.MaxValue + 1,
                    (int)sbyte.MaxValue + 2,

                    (int)byte.MaxValue - 2,
                    (int)byte.MaxValue - 1,
                    (int)byte.MaxValue,
                    (int)byte.MaxValue + 1,
                    (int)byte.MaxValue + 2,

                    (int) Int16.MaxValue - 2,
                    (int) Int16.MaxValue - 1,
                    (int) Int16.MaxValue,
                    (int) Int16.MaxValue + 1,
                    (int) Int16.MaxValue + 2,

                    int.MaxValue - 4,
                    int.MaxValue - 3,
                    int.MaxValue - 2,
                    int.MaxValue - 1,
                    int.MaxValue,
                };

        internal static readonly ulong[] UInt64ValuesToCheck = new ulong[]
                {
                    ulong.MinValue,
                    ulong.MinValue + 1,
                    ulong.MinValue + 2,
                    ulong.MinValue + 3,
                    ulong.MinValue + 4,

                    5, 6, 7, 8, 9, 10, 99, 100, 101, 999, 1000, 1001, 9999, 10000, 10001, 99999, 100000, 100001,

                    (ulong)sbyte.MaxValue - 2,
                    (ulong)sbyte.MaxValue - 1,
                    (ulong)sbyte.MaxValue,
                    (ulong)sbyte.MaxValue + 1,
                    (ulong)sbyte.MaxValue + 2,

                    (ulong)byte.MaxValue - 2,
                    (ulong)byte.MaxValue - 1,
                    (ulong)byte.MaxValue,
                    (ulong)byte.MaxValue + 1,
                    (ulong)byte.MaxValue + 2,

                    (ulong)Int16.MaxValue - 2,
                    (ulong)Int16.MaxValue - 1,
                    (ulong)Int16.MaxValue,
                    (ulong)Int16.MaxValue + 1,
                    (ulong)Int16.MaxValue + 2,

                    (ulong)UInt16.MaxValue - 2,
                    (ulong)UInt16.MaxValue - 1,
                    (ulong)UInt16.MaxValue,
                    (ulong)UInt16.MaxValue + 1,
                    (ulong)UInt16.MaxValue + 2,

                    (ulong)Int32.MaxValue - 2,
                    (ulong)Int32.MaxValue - 1,
                    (ulong)Int32.MaxValue,
                    (ulong)Int32.MaxValue + 1,
                    (ulong)Int32.MaxValue + 2,

                    (ulong)UInt32.MaxValue - 2,
                    (ulong)UInt32.MaxValue - 1,
                    (ulong)UInt32.MaxValue,
                    (ulong)UInt32.MaxValue + 1,
                    (ulong)UInt32.MaxValue + 2,

                    ulong.MaxValue - 4,
                    ulong.MaxValue - 3,
                    ulong.MaxValue - 2,
                    ulong.MaxValue - 1,
                    ulong.MaxValue,
                };
        internal static readonly long[] Int64ValuesToCheck = new long[]
                {
                    long.MinValue,
                    long.MinValue + 1,
                    long.MinValue + 2,
                    long.MinValue + 3,
                    long.MinValue + 4,

                    (long)Int32.MinValue - 2,
                    (long)Int32.MinValue - 1,
                    (long)Int32.MinValue,
                    (long)Int32.MinValue + 1,
                    (long)Int32.MinValue + 2,

                    (long)Int16.MinValue - 2,
                    (long)Int16.MinValue - 1,
                    (long)Int16.MinValue,
                    (long)Int16.MinValue + 1,
                    (long)Int16.MinValue + 2,

                    (long)sbyte.MinValue - 2,
                    (long)sbyte.MinValue - 1,
                    (long)sbyte.MinValue,
                    (long)sbyte.MinValue + 1,
                    (long)sbyte.MinValue + 2,

                    -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -99, -100, -101, -999, -1000, -1001, -9999, -10000, -10001, -99999, -100000, -100001,
                    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 99, 100, 101, 999, 1000, 1001, 9999, 10000, 10001, 99999, 100000, 100001,

                    (long)sbyte.MaxValue - 2,
                    (long)sbyte.MaxValue - 1,
                    (long)sbyte.MaxValue,
                    (long)sbyte.MaxValue + 1,
                    (long)sbyte.MaxValue + 2,

                    (long)byte.MaxValue - 2,
                    (long)byte.MaxValue - 1,
                    (long)byte.MaxValue,
                    (long)byte.MaxValue + 1,
                    (long)byte.MaxValue + 2,

                    (long)Int16.MaxValue - 2,
                    (long)Int16.MaxValue - 1,
                    (long)Int16.MaxValue,
                    (long)Int16.MaxValue + 1,
                    (long)Int16.MaxValue + 2,

                    (long)UInt16.MaxValue - 2,
                    (long)UInt16.MaxValue - 1,
                    (long)UInt16.MaxValue,
                    (long)UInt16.MaxValue + 1,
                    (long)UInt16.MaxValue + 2,

                    (long)Int32.MaxValue - 2,
                    (long)Int32.MaxValue - 1,
                    (long)Int32.MaxValue,
                    (long)Int32.MaxValue + 1,
                    (long)Int32.MaxValue + 2,

                    (long)UInt32.MaxValue - 2,
                    (long)UInt32.MaxValue - 1,
                    (long)UInt32.MaxValue,
                    (long)UInt32.MaxValue + 1,
                    (long)UInt32.MaxValue + 2,

                    long.MaxValue - 4,
                    long.MaxValue - 3,
                    long.MaxValue - 2,
                    long.MaxValue - 1,
                    long.MaxValue,
                };

        internal static readonly float[] FloatValuesToCheck = new float[]
                {
                    float.NaN,
                    float.NegativeInfinity,

                    float.MinValue,
                    float.MinValue + float.Epsilon,
                    float.MinValue + float.Epsilon + float.Epsilon,

                    float.Epsilon,

                    5, 6, 7, 8, 9, 10, 99, 100, 101,

                    float.MaxValue - float.Epsilon - float.Epsilon,
                    float.MaxValue - float.Epsilon,
                    float.MaxValue,

                    float.PositiveInfinity,
                };

        internal static readonly double[] DoubleValuesToCheck = new double[]
                {
                    double.NaN,
                    double.NegativeInfinity,
                    float.NegativeInfinity,

                    double.MinValue,
                    double.MinValue + double.Epsilon,
                    double.MinValue + double.Epsilon + double.Epsilon,
                    double.MinValue + float.Epsilon,
                    double.MinValue + float.Epsilon + float.Epsilon,

                    float.MinValue,
                    float.MinValue + double.Epsilon,
                    float.MinValue + double.Epsilon + double.Epsilon,
                    float.MinValue + float.Epsilon,
                    float.MinValue + float.Epsilon + float.Epsilon,

                    double.Epsilon,
                    float.Epsilon,

                    5, 6, 7, 8, 9, 10, 99, 100, 101,

                    float.MaxValue - float.Epsilon - float.Epsilon,
                    float.MaxValue - float.Epsilon,
                    float.MaxValue - double.Epsilon - double.Epsilon,
                    float.MaxValue - double.Epsilon,
                    float.MaxValue,

                    double.MaxValue - float.Epsilon - float.Epsilon,
                    double.MaxValue - float.Epsilon,
                    double.MaxValue - double.Epsilon - double.Epsilon,
                    double.MaxValue - double.Epsilon,
                    double.MaxValue,

                    float.PositiveInfinity,
                    double.PositiveInfinity,
                };

        internal static readonly decimal[] DecimalValuesToCheck = new decimal[]
                {
                    decimal.MinValue,
                    decimal.MinValue + (decimal)double.Epsilon,
                    decimal.MinValue + (decimal)double.Epsilon + (decimal)double.Epsilon,
                    decimal.MinValue + (decimal)float.Epsilon,
                    decimal.MinValue + (decimal)float.Epsilon + (decimal)float.Epsilon,

                    //(decimal)double.MinValue,
                    //(decimal)double.MinValue + (decimal)double.Epsilon,
                    //(decimal)double.MinValue + (decimal)double.Epsilon + (decimal)double.Epsilon,
                    //(decimal)double.MinValue + (decimal)float.Epsilon,
                    //(decimal)double.MinValue + (decimal)float.Epsilon + (decimal)float.Epsilon,

                    //(decimal)float.MinValue,
                    //(decimal)float.MinValue + (decimal)double.Epsilon,
                    //(decimal)float.MinValue + (decimal)double.Epsilon + (decimal)double.Epsilon,
                    //(decimal)float.MinValue + (decimal)float.Epsilon,
                    //(decimal)float.MinValue + (decimal)float.Epsilon + (decimal)float.Epsilon,

                    decimal.MinusOne,

                    decimal.Zero,

                    (decimal)double.Epsilon,
                    (decimal)float.Epsilon,

                    decimal.One,

                    5, 6, 7, 8, 9, 10, 99, 100, 101,
                    5.123m, 6.4567m, 7.12345m, 8.9876m, 9.99999m, 10.000001m, 99.99887766m, 100.987654321m, 101.0101010101m,

                    //(decimal)float.MaxValue - (decimal)float.Epsilon - (decimal)float.Epsilon,
                    //(decimal)float.MaxValue - (decimal)float.Epsilon,
                    //(decimal)float.MaxValue - (decimal)double.Epsilon - (decimal)double.Epsilon,
                    //(decimal)float.MaxValue - (decimal)double.Epsilon,
                    //(decimal)float.MaxValue,

                    //(decimal)double.MaxValue - (decimal)float.Epsilon - (decimal)float.Epsilon,
                    //(decimal)double.MaxValue - (decimal)float.Epsilon,
                    //(decimal)double.MaxValue - (decimal)double.Epsilon - (decimal)double.Epsilon,
                    //(decimal)double.MaxValue - (decimal)double.Epsilon,
                    //(decimal)double.MaxValue,

                    decimal.MaxValue - (decimal)float.Epsilon - (decimal)float.Epsilon,
                    decimal.MaxValue - (decimal)float.Epsilon,
                    decimal.MaxValue - (decimal)double.Epsilon - (decimal)double.Epsilon,
                    decimal.MaxValue - (decimal)double.Epsilon,
                    decimal.MaxValue,
                };

        internal static readonly char[] CharValuesToCheck = (@"árvíztűrő tükörfúrógép _?!\| ÁRVÍZTŰRŐ TÜKÖRFÚRÓGÉP" + "\r\n\t \u0000 \x01D").ToCharArray();

        internal static readonly string[] StringValuesToCheck = new string[]
            {
                null,
                "",
                "   ",
                " \t  \r\n\t   \r\n   \r   \n",
                "abc \t  \r\nabc\t   abc\r\n abc  \rabc\nabc",
                @"árvíztűrő tükörfúrógép _?!\| ÁRVÍZTŰRŐ TÜKÖRFÚRÓGÉP",
            };

        internal static readonly DateTime[] DateTimeValuesToCheck = new DateTime[]
            {
                DateTime.MinValue,
                DateTime.UnixEpoch,
                DateTime.Today,
                DateTime.Now,
                DateTime.UtcNow,
                DateTime.MaxValue,

                new DateTime(2018, 1, 1, 12, 12, 12, DateTimeKind.Unspecified),
                new DateTime(2018, 1, 1, 12, 12, 12, DateTimeKind.Local),
                new DateTime(2018, 1, 1, 12, 12, 12, DateTimeKind.Utc),

                DateTime.Parse("1997-07-16"),
                DateTime.Parse("1997-07-16T19:20"),
                DateTime.Parse("1997-07-16T19:20Z"),
                DateTime.Parse("1997-07-16T19:20+01:00"),
                DateTime.Parse("1997-07-16T19:20:30+01:00"),
                DateTime.Parse("1997-07-16T19:20:30.45+01:00"),
                DateTime.Parse("2018-12-25T08:34Z"),
            };

        internal static readonly Guid[] GuidValuesToCheck = new Guid[]
            {
                Guid.Empty,
                Guid.NewGuid(),
                Guid.Parse("19AA6FD0-9A80-4CB7-902B-A826DF8CD291"),
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Guid.Parse("99999999-9999-9999-9999-999999999999"),
            };
    }
}
