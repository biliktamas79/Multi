using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Multi.Serialization;

namespace Multi.Core.Tests.Unit.Serialization
{
    [TestClass]
    public class ProtocolBuffersEncodingUnitTests
    {
        private static readonly Random Rnd = new Random(Environment.TickCount);

        #region EncodeZigZag32_and_DecodeZigZag32
        [TestMethod]
        public void ProtocolBuffersEncoding_EncodeZigZag32_can_encode_and_DecodeZigZag32_can_decode_the_most_important_Int32_values()
        {
            int[] valuesToCheck = new int[]
                {
                    int.MinValue, 
                    int.MinValue + 1,
                    int.MinValue + 2,
                    int.MinValue + 3,
                    int.MinValue + 4,

                    (int)Int16.MinValue - 2,
                    (int)Int16.MinValue - 1,
                    (int)Int16.MinValue,
                    (int)Int16.MinValue + 1,
                    (int)Int16.MinValue + 2,

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

                    (int)Int16.MaxValue - 2,
                    (int)Int16.MaxValue - 1,
                    (int)Int16.MaxValue,
                    (int)Int16.MaxValue + 1,
                    (int)Int16.MaxValue + 2,

                    int.MaxValue - 4,
                    int.MaxValue - 3,
                    int.MaxValue - 2,
                    int.MaxValue - 1,
                    int.MaxValue,
                };

            foreach (var i in valuesToCheck)
            {
                var encoded = ProtocolBuffersEncoding.EncodeZigZag32(i);
                System.Diagnostics.Debug.WriteLine($"Int32 value {i} encoded to {encoded}.");
                var result = ProtocolBuffersEncoding.DecodeZigZag32(encoded);
                Assert.AreEqual(i, result);
            }
        }

        [Ignore("Ignored this as it takes very long time to execute.")]
        [TestMethod]
        public void ProtocolBuffersEncoding_EncodeZigZag32_can_encode_and_DecodeZigZag32_can_decode_all_Int32_values()
        {
            for (int i = int.MinValue; i < int.MaxValue; i++)
            {
                var encoded = ProtocolBuffersEncoding.EncodeZigZag32(i);
                var result = ProtocolBuffersEncoding.DecodeZigZag32(encoded);
                Assert.AreEqual(i, result);
            }
        }
        #endregion EncodeZigZag32_and_DecodeZigZag32

        #region EncodeZigZag64_and_DecodeZigZag64
        [TestMethod]
        public void ProtocolBuffersEncoding_EncodeZigZag64_can_encode_and_DecodeZigZag64_can_decode_the_most_important_Int64_values()
        {
            long[] valuesToCheck = new long[]
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

                    (long)Int32.MaxValue - 2,
                    (long)Int32.MaxValue - 1,
                    (long)Int32.MaxValue,
                    (long)Int32.MaxValue + 1,
                    (long)Int32.MaxValue + 2,

                    long.MaxValue - 4,
                    long.MaxValue - 3,
                    long.MaxValue - 2,
                    long.MaxValue - 1,
                    long.MaxValue,
                };

            foreach (var i in valuesToCheck)
            {
                var encoded = ProtocolBuffersEncoding.EncodeZigZag64(i);
                System.Diagnostics.Debug.WriteLine($"Int64 value {i} encoded to {encoded}.");
                var result = ProtocolBuffersEncoding.DecodeZigZag64(encoded);
                Assert.AreEqual(i, result);
            }
        }

        [Ignore("Ignored this as it takes very long time to execute.")]
        [TestMethod]
        public void ProtocolBuffersEncoding_EncodeZigZag64_can_encode_and_DecodeZigZag64_can_decode_all_Int64_values()
        {
            for (long i = long.MinValue; i < long.MaxValue; i++)
            {
                var encoded = ProtocolBuffersEncoding.EncodeZigZag64(i);
                var result = ProtocolBuffersEncoding.DecodeZigZag64(encoded);
                Assert.AreEqual(i, result);
            }
        }
        #endregion EncodeZigZag64_and_DecodeZigZag64
    }
}
