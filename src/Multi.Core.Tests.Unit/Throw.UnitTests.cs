using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Multi.Core.Tests.Unit
{
    [TestClass]
    public class ThrowUnitTests
    {
        #region IfArgumentIsNull
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_IfArgumentIsNull_object_throws_if_arg_isNull()
        {
            Throw.IfArgumentIsNull(null as object, "arg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_IfArgumentIsNull_string_throws_if_arg_isNull()
        {
            Throw.IfArgumentIsNull(null as string, "arg");
        }

        [TestMethod]
        public void Throw_IfArgumentIsNull_object_does_not_throw_if_arg_isNotNull()
        {
            Throw.IfArgumentIsNull(new object(), "arg");
        }

        [TestMethod]
        public void Throw_IfArgumentIsNull_string_does_not_throw_if_arg_isNotNull()
        {
            Throw.IfArgumentIsNull("123", "arg");
        }

        [TestMethod]
        public void Throw_IfArgumentIsNull_thrown_ArgumentNullException_with_the_right_argument_name()
        {
            try
            {
                Throw.IfArgumentIsNull(null, "arg");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("arg", ex.ParamName, false, "ArgumentNullException has been thrown with wrong ParamName.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNull_thrown_ArgumentNullException_with_the_right_argument_name_and_message()
        {
            try
            {
                Throw.IfArgumentIsNull(null, "arg", "123msg");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentNullException has been thrown with wrong message.");
            }
        }
        #endregion IfArgumentIsNull

        #region IfArgumentIsNullOrEmpty
        [TestMethod]
        public void Throw_IfArgumentIsNullOrEmpty_thrown_ArgumentNullException_if_arg_isNull()
        {
            try
            {
                Throw.IfArgumentIsNullOrEmpty(null, "arg", "123msg");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentNullException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrEmpty_thrown_ArgumentException_if_arg_isEmpty()
        {
            try
            {
                Throw.IfArgumentIsNullOrEmpty(string.Empty, "arg", "123msg");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(typeof(ArgumentException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrEmpty_thrown_ArgumentException_with_the_right_argument_name_and_message_if_arg_isNull()
        {
            try
            {
                Throw.IfArgumentIsNull(null, "arg", "123msg");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentNullException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrEmpty_thrown_ArgumentException_with_the_right_argument_name_and_message_if_arg_isEmpty()
        {
            try
            {
                Throw.IfArgumentIsNull(string.Empty, "arg", "123msg");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(typeof(ArgumentException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.AreEqual("123msg", ex.Message, false, "ArgumentException has been thrown with wrong message.");
            }
        }
        #endregion IfArgumentIsNullOrEmpty

        #region IfArgumentIsNullOrWhiteSpace
        [TestMethod]
        public void Throw_IfArgumentIsNullOrWhiteSpace_thrown_ArgumentNullException_if_arg_isNull()
        {
            try
            {
                Throw.IfArgumentIsNullOrWhiteSpace(null, "arg", "123msg");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentNullException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrWhiteSpace_thrown_ArgumentException_if_arg_isEmpty()
        {
            try
            {
                Throw.IfArgumentIsNullOrWhiteSpace(string.Empty, "arg", "123msg");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(typeof(ArgumentException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrWhiteSpace_thrown_ArgumentException_if_arg_isWhiteSpaceOnly()
        {
            try
            {
                Throw.IfArgumentIsNullOrWhiteSpace(" \t ", "arg", "123msg");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(typeof(ArgumentException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrWhiteSpace_thrown_ArgumentNullException_with_the_right_argument_name_and_message_if_arg_isNull()
        {
            try
            {
                Throw.IfArgumentIsNull(null, "arg", "123msg");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("123msg"), "ArgumentNullException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrWhiteSpace_thrown_ArgumentException_with_the_right_argument_name_and_message_if_arg_isEmpty()
        {
            try
            {
                Throw.IfArgumentIsNull(string.Empty, "arg", "123msg");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(typeof(ArgumentException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.AreEqual("123msg", ex.Message, false, "ArgumentException has been thrown with wrong message.");
            }
        }

        [TestMethod]
        public void Throw_IfArgumentIsNullOrWhiteSpace_thrown_ArgumentException_with_the_right_argument_name_and_message_if_arg_isWhiteSpaceOnly()
        {
            try
            {
                Throw.IfArgumentIsNull(" \t ", "arg", "123msg");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(typeof(ArgumentException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.AreEqual("123msg", ex.Message, false, "ArgumentException has been thrown with wrong message.");
            }
        }
        #endregion IfArgumentIsNullOrWhiteSpace

        #region IfArgumentLengthGreaterThan
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Throw_IfArgumentLengthGreaterThan_thrown_NullReferenceException_if_arg_isNull()
        {
            Throw.IfArgumentLengthGreaterThan(null, "arg", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_IfArgumentLengthGreaterThan_thrown_ArgumentException_if_arg_length_isGreaterThan_max()
        {
            Throw.IfArgumentLengthGreaterThan("123456789", "arg", 3);
        }

        [TestMethod]
        public void Throw_IfArgumentLengthGreaterThan_does_not_throw_if_arg_length_equals_max()
        {
            Throw.IfArgumentLengthGreaterThan("123456789", "arg", 9);
        }

        [TestMethod]
        public void Throw_IfArgumentLengthGreaterThan_does_not_throw_if_arg_length_isLesserThan_max()
        {
            Throw.IfArgumentLengthGreaterThan("123456789", "arg", 10);
        }

        [TestMethod]
        public void Throw_IfArgumentLengthGreaterThan_thrown_ArgumentException_with_additionalData()
        {
            try
            {
                Throw.IfArgumentLengthGreaterThan("123456789", "arg", 3);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(typeof(ArgumentException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.AreEqual(true, ex.Data.Contains("argLength"), "Additional exception data with key 'argLength' not found!");
                Assert.AreEqual(9, ex.Data["argLength"]);
                Assert.AreEqual(true, ex.Data.Contains("maxLength"), "Additional exception data with key 'maxLength' not found!");
                Assert.AreEqual(3, ex.Data["maxLength"]);
            }
        }
        #endregion IfArgumentLengthGreaterThan

        #region IfArgumentOutOfRangeMinMax
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Throw_IfArgumentOutOfRangeMinMax_thrown_ArgumentException_if_arg_isOutOfRange()
        {
            Throw.IfArgumentOutOfRangeMinMax(10, "arg", 1, 8);
        }

        [TestMethod]
        public void Throw_IfArgumentOutOfRangeMinMax_does_not_throw_if_arg_isInRange()
        {
            Throw.IfArgumentOutOfRangeMinMax(10, "arg", 10, 15);
            Throw.IfArgumentOutOfRangeMinMax(10, "arg", 1, 10);
            Throw.IfArgumentOutOfRangeMinMax(10, "arg", 1, 15);
        }

        [TestMethod]
        public void Throw_IfArgumentOutOfRangeMinMax_thrown_ArgumentException_with_additionalData()
        {
            try
            {
                Throw.IfArgumentOutOfRangeMinMax(10, "arg", 1, 8);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.AreEqual(true, ex.Data.Contains("argValue"), "Additional exception data with key 'argValue' not found!");
                Assert.AreEqual(10, ex.Data["argValue"]);
                Assert.AreEqual(true, ex.Data.Contains("maxValue"), "Additional exception data with key 'maxLength' not found!");
                Assert.AreEqual(8, ex.Data["maxValue"]);
            }

            try
            {
                Throw.IfArgumentOutOfRangeMinMax(1, "arg", 2, 8);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException).FullName, ex.GetType().FullName, false, "Not the right exception type was thrown!");
                Assert.AreEqual(true, ex.Data.Contains("argValue"), "Additional exception data with key 'argValue' not found!");
                Assert.AreEqual(1, ex.Data["argValue"]);
                Assert.AreEqual(true, ex.Data.Contains("minValue"), "Additional exception data with key 'minValue' not found!");
                Assert.AreEqual(2, ex.Data["minValue"]);
            }
        }
        #endregion IfArgumentOutOfRangeMinMax
    }
}
