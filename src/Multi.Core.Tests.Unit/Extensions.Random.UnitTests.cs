using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Multi.Core.Tests.Unit
{
    [TestClass]
    public class SystemExtensionsRandomUnitTests
    {
        private static readonly Random Rnd = new Random(Environment.TickCount);

        #region NextString
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Random_NextString_without_charactersToChooseFrom_throws_ArgumentNullException_if_random_isNull()
        {
            Random rnd = null;
            rnd.NextString(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Random_NextString_with_charactersToChooseFrom_throws_ArgumentNullException_if_random_isNull()
        {
            Random rnd = null;
            rnd.NextString(1, "abcd123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Random_NextString_with_charactersToChooseFrom_throws_ArgumentNullException_if_charactersToChooseFrom_isNull()
        {
            Rnd.NextString(1, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Random_NextString_with_charactersToChooseFrom_throws_ArgumentException_if_charactersToChooseFrom_isEmpty()
        {
            Rnd.NextString(1, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Random_NextString_with_charactersToChooseFrom_throws_ArgumentOutOfRangeException_if_length_is_negative()
        {
            Rnd.NextString(-1, "abcd123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Random_NextString_without_charactersToChooseFrom_throws_ArgumentOutOfRangeException_if_length_is_negative()
        {
            Rnd.NextString(-1);
        }

        [TestMethod]
        public void Random_NextString_without_charactersToChooseFrom_returns_emptyString_if_length_equals_zero()
        {
            Assert.AreEqual(string.Empty, Rnd.NextString(0));
        }

        [TestMethod]
        public void Random_NextString_with_charactersToChooseFrom_returns_emptyString_if_length_equals_zero()
        {
            Assert.AreEqual(string.Empty, Rnd.NextString(0, "abcd123"));
        }

        [TestMethod]
        public void Random_NextString_with_charactersToChooseFrom_returns_the_right_string_if_length_isPositive()
        {
            int[] lengthsToGenerate = new int[]
                {
                    1, 2, 3, 10, 100, 1000, 10000
                };

            string[] charactersToChooseFromArrays = new string[]
                {
                    "1234567890",
                    "abcdefghijklmnopqrstuvwxyz",
                    "abcdefghijklmnopqrstuvwxyz0123456789"
                };

            foreach (var length in lengthsToGenerate)
            {
                foreach (var charsToChooseFrom in charactersToChooseFromArrays)
                {
                    var rndString = Rnd.NextString(length, charsToChooseFrom);

                    Assert.AreEqual(length, rndString.Length);

                    // all chars in the generated string exists in charsToChooseFrom string
                    foreach (char c in rndString)
                    {
                        Assert.IsTrue(charsToChooseFrom.Contains(c));
                    }

                    // for lengths big enough, all chars in the generated string should not be the same
                    if (length > 3)
                    {
                        char firstChar = rndString[0];
                        for (int i = 1; i < length; i++)
                        {
                            // if we find at least 2 different chars, than we're fine
                            if (rndString[i] != firstChar)
                                break;

                            // if we reached the last char without exiting
                            if (i == length - 1)
                                Assert.Fail("All characters are the same in the string returned by Random.NextString.");
                        }
                    }
                }
            }
        }
        #endregion NextString
    }
}
