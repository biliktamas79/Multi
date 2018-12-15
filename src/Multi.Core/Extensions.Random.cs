using System;
using System.Linq;
using System.Text;
using Multi;

namespace System
{
    public static partial class Extensions
    {
        public static string NextString(this System.Random rnd, int length, string charactersToChooseFrom = "abcdefghijklmnopqrstuvwxyz0123456789")
        {
            Throw.IfArgumentIsNull(rnd, "rnd");
            Throw.IfArgumentIsNullOrEmpty(charactersToChooseFrom, "charactersToChooseFrom");
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "Length must be a non negative integer value!");
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
    }
}
