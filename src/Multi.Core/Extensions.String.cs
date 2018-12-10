using System;
using System.Linq;
using System.Text;
using Multi;

namespace System
{
    public static partial class Extensions
    {
        public static string Reverse(this string s)
        {
            if (s == null)
                return null;

            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string NextString(this System.Random rnd, int length, string charactersToChooseFrom = "abcdefghijklmnopqrstuvwxyz0123456789")
        {
            Throw.IfArgumentIsNull(rnd, "rnd");
            Throw.IfArgumentIsNullOrEmpty(charactersToChooseFrom, "charactersToChooseFrom");
            if (length < 0)
#if PCL
                throw new ArgumentOutOfRangeException("length", "Length must be a non negative integer value!");
#else
                throw new ArgumentOutOfRangeException("length", length, "Length must be a non negative integer value!");
#endif
            if (length == 0)
                return string.Empty;
            if (length == 1)
                return charactersToChooseFrom[rnd.Next(charactersToChooseFrom.Length)].ToString();
            else
            {
                StringBuilder sb = new StringBuilder(length);
                for (int i = 0; i < length; i++)
                {
                    sb.Append(charactersToChooseFrom[rnd.Next(charactersToChooseFrom.Length)]);
                }
                return sb.ToString();
            }
        }

        public static bool ContainsWhiteSpace(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsWhiteSpace(s[i]))
                        return true;
                }
            }
            return false;
        }

        public static bool ContainsNonWhiteSpace(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (!char.IsWhiteSpace(s[i]))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Returns a value indicating whether the specified System.String object occurs within this string using the provided comparison type.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">The comparison type to use</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool Contains(this string str, string value, StringComparison comparisonType)
        {
            Throw.IfArgumentIsNull(str, "str");
            Throw.IfArgumentIsNull(value, "value");
            if (value == string.Empty)
                return true;

            return (-1 < str.IndexOf(value, comparisonType));
        }

        public static string RemoveWhitespaces(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                int whiteSpaceCharCount = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsWhiteSpace(s[i]))
                        whiteSpaceCharCount++;
                }
                if (whiteSpaceCharCount > 0)
                {
                    // if it is whitespace only
                    if (s.Length == whiteSpaceCharCount)
                        return string.Empty;

                    var newStrChars = new char[s.Length - whiteSpaceCharCount];
                    int idx = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (!char.IsWhiteSpace(s[i]))
                            newStrChars[idx++] = s[i];
                    }
                    s = new string(newStrChars);
                }
            }
            return s;
        }
    }
}
