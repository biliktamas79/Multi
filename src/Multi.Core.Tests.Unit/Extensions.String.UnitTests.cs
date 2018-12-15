using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Multi.Core.Tests.Unit
{
    [TestClass]
    public class SystemExtensionsStringUnitTests
    {
        #region Reverse
        [TestMethod]
        public void String_Reverse_returns_null_if_string_isNull()
        {
            string s = null;
            Assert.IsNull(s.Reverse());
        }

        public void String_Reverse_returns_the_string_reversed()
        {
            string s = "123abc456DEF";
            string reversed = (s as IEnumerable<char>).Reverse().ToString();
            Assert.AreEqual(reversed, s.Reverse());
        }
        #endregion Reverse

        #region ContainsWhiteSpace
        [TestMethod]
        public void String_ContainsWhiteSpace_returns_false_if_string_isNull()
        {
            string s = null;
            Assert.IsFalse(s.ContainsWhiteSpace());
        }

        [TestMethod]
        public void String_ContainsWhiteSpace_returns_false_if_string_isEmpty()
        {
            string s = string.Empty;
            Assert.IsFalse(s.ContainsWhiteSpace());
        }

        [TestMethod]
        public void String_ContainsWhiteSpace_returns_false_if_string_does_not_contain_whitespace()
        {
            string s = "abcdefg1234567";
            Assert.IsFalse(s.ContainsWhiteSpace());
        }

        [TestMethod]
        public void String_ContainsWhiteSpace_returns_true_if_string_isWhitespaceOnly()
        {
            string[] testStrings = new string[]
                {
                    " ",
                    "\t",
                    "\r",
                    "\n",
                    "\r\n",
                    "   \t  \r  \n  \r\n",
                };

            foreach (var s in testStrings)
            {
                Assert.IsTrue(s.ContainsWhiteSpace());
            }
        }
        #endregion ContainsWhiteSpace

        #region ContainsNonWhiteSpace
        [TestMethod]
        public void String_ContainsNonWhiteSpace_returns_false_if_string_isNull()
        {
            string s = null;
            Assert.IsFalse(s.ContainsNonWhiteSpace());
        }

        [TestMethod]
        public void String_ContainsNonWhiteSpace_returns_false_if_string_isEmpty()
        {
            string s = string.Empty;
            Assert.IsFalse(s.ContainsNonWhiteSpace());
        }

        [TestMethod]
        public void String_ContainsNonWhiteSpace_returns_false_if_string_isWhiteSpaceOnly()
        {
            string[] testStrings = new string[]
                {
                    " ",
                    "\t",
                    "\r",
                    "\n",
                    "\r\n",
                    "   \t  \r  \n  \r\n",
                };

            foreach (var s in testStrings)
            {
                Assert.IsFalse(s.ContainsNonWhiteSpace());
            }
        }

        [TestMethod]
        public void String_ContainsNonWhiteSpace_returns_true_if_string_contains_at_least_one_nonWhiteSpace_character()
        {
            string[] testStrings = new string[]
                {
                    " a",
                    "1\t",
                    "\r2",
                    "b\n",
                    "\r\n3",
                    "   \t  \rc  \n  \r\n",
                };

            foreach (var s in testStrings)
            {
                Assert.IsTrue(s.ContainsNonWhiteSpace());
            }
        }
        #endregion ContainsNonWhiteSpace

        #region RemoveWhiteSpaces
        [TestMethod]
        public void String_RemoveWhiteSpaces_returns_the_same_string_instance_if_string_has_no_whiteSpaces()
        {
            string[] testStrings = new string[]
                {
                    null,
                    string.Empty,
                    "a",
                    "1",
                    "abc123",
                };

            foreach (var s in testStrings)
            {
                Assert.AreSame(s, s.RemoveWhiteSpaces());
            }
        }

        [TestMethod]
        public void String_RemoveWhiteSpaces_returns_empty_string_if_string_isWhiteSpaceOnly()
        {
            string[] testStrings = new string[]
                {
                    " ",
                    "\t",
                    "\r",
                    "\n",
                    "\r\n",
                    "   \t  \r  \n  \r\n",
                };

            foreach (var s in testStrings)
            {
                Assert.AreSame(string.Empty, s.RemoveWhiteSpaces());
                Assert.AreSame("", s.RemoveWhiteSpaces()); // testing JIT optimizing empty string instances :)
            }
        }

        [TestMethod]
        public void String_RemoveWhiteSpaces_returns_new_string_instance_with_whiteSpaces_removed()
        {
            string[] testStrings = new string[]
                {
                    " abc123",
                    "abc123 ",
                    " abc123 ",

                    "\tabc123",
                    "abc123\t",
                    "\tabc123\t",

                    "\rabc123",
                    "abc123\r",
                    "\rabc123\r",

                    "\nabc123",
                    "abc123\n",
                    "\nabc123\n",

                    "\r\nabc123",
                    "abc123\r\n",
                    "\r\nabc123\r\n",

                    "   \t  \r abc123 \n  \r\n",
                };

            foreach (var s in testStrings)
            {
                Assert.AreEqual("ABC123", s.RemoveWhiteSpaces(), true);
                Assert.AreEqual("ABC123", s.ToUpper().RemoveWhiteSpaces(), false);
                Assert.AreEqual("abc123", s.RemoveWhiteSpaces(), false);
            }
        }
        #endregion RemoveWhiteSpaces
    }
}
