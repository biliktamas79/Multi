using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Multi.Serialization;

namespace Multi.Core.Tests.Unit.Serialization
{
    [TestClass]
    public class ProtocolBuffersEncodingUnitTests
    {
        #region EncodeZigZag32_and_DecodeZigZag32
        [TestMethod]
        public void ProtocolBuffersEncoding_EncodeZigZag32_can_encode_and_DecodeZigZag32_can_decode_the_most_important_Int32_values()
        {
            foreach (var i in SerializationTestGlobals.Int32ValuesToCheck)
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
            foreach (var i in SerializationTestGlobals.Int64ValuesToCheck)
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
