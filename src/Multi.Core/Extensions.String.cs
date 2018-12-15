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

        public static bool ContainsWhiteSpace(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                for (int i = 0; i < s.Length; i++)
                {
                    // as soon as we find a whitespace char
                    if (char.IsWhiteSpace(s[i]))
                        // it contains whitespace
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
                    // as soon as we find a non-whitespace char
                    if (!char.IsWhiteSpace(s[i]))
                        // it contains non-whitespace
                        return true;
                }
            }
            return false;
        }

        public static string RemoveWhiteSpaces(this string s)
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
