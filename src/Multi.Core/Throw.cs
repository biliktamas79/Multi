using System;
using System.Text;

namespace System
{
    public static class Throw
    {
        public static void IfArgumentIsNull(object arg, string name)
        {
            if (arg == null)
                throw new ArgumentNullException(name);
        }
        public static void IfArgumentIsNull(object arg, string name, string message)
        {
            if (arg == null)
                throw new ArgumentNullException(name, message);
        }

        public static void IfArgumentIsNullOrEmpty(string arg, string name)
        {
            //if (name == null)
            //    throw new ArgumentNullException("name");
            //if (string.IsNullOrWhiteSpace(name))
            //    throw new ArgumentException("String is empty or contains whitespace characters only.", name);

            if (arg == null)
                throw new ArgumentNullException(name);
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException("String is empty.", name);
        }
        public static void IfArgumentIsNullOrEmpty(string arg, string name, string message)
        {
            //if (name == null)
            //    throw new ArgumentNullException("name");
            //if (string.IsNullOrWhiteSpace(name))
            //    throw new ArgumentException("String is empty or contains whitespace characters only.", name);

            if (arg == null)
                throw new ArgumentNullException(name, message);
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException(message, name);
        }

        public static void IfArgumentIsNullOrWhiteSpace(string arg, string name)
        {
            //if (name == null)
            //    throw new ArgumentNullException("name");
            //if (string.IsNullOrWhiteSpace(name))
            //    throw new ArgumentException("String is empty or contains whitespace characters only.", name);

            if (arg == null)
                throw new ArgumentNullException(name);
            if (string.IsNullOrWhiteSpace(arg))
                throw new ArgumentException("String is empty or contains whitespace characters only.", name);
        }
        public static void IfArgumentIsNullOrWhiteSpace(string arg, string name, string message)
        {
            //if (name == null)
            //    throw new ArgumentNullException("name");
            //if (string.IsNullOrWhiteSpace(name))
            //    throw new ArgumentException("String is empty or contains whitespace characters only.", name);

            if (arg == null)
                throw new ArgumentNullException(name, message);
            if (string.IsNullOrWhiteSpace(arg))
                throw new ArgumentException(message, name);
        }

        ///// <summary>
        ///// </summary>
        ///// <param name="arg"></param>
        ///// <param name="argName"></param>
        ///// <param name="maxLength"></param>
        ///// <exception cref="System.ArgumentNullException">Thrown when <paramref name="arg" /> is null.</exception>
        ///// <exception cref="System.ArgumentException">Thrown when <paramref name="arg" /> is not null, but contains whitespace characters only.</exception>
        //public static void IfArgumentIsNullOrLengthGreaterThan(string arg, string argName, int maxLength)
        //{
        //    if (arg == null)
        //        throw new ArgumentNullException(argName ?? "arg");
        //    if (arg.Length > maxLength)
        //    {
        //        throw new ArgumentException("The provided string is longer than the maximum allowed length.",
        //            argName ?? "arg");
        //    }
        //}

        /// <summary>
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="argName"></param>
        /// <param name="maxLength"></param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="arg" /> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the <paramref name="arg" />.Length is greater than the <paramref name="maxLength" /> value.</exception>
        public static void IfArgumentLengthGreaterThan(string arg, string argName, int maxLength)
        {
            if (arg.Length > maxLength)
            {
                var ex = new ArgumentException("The provided string is longer than the maximum allowed length.",
                    argName ?? "arg");
                ex.Data["argLength"] = arg.Length;
                ex.Data["maxLength"] = maxLength;

                throw ex;
            }
        }

        public static void IfArgumentOutOfRangeMinMax(int arg, string argName, int min, int max)
        {
            if (min > arg)
            {
                var ex = new ArgumentOutOfRangeException(argName ?? "arg",
                    "The provided value is lesser than the minimum allowed value.");

                ex.Data["argValue"] = arg;
                ex.Data["minValue"] = min;

                throw ex;
            }
            if (arg > max)
            {
                var ex = new ArgumentOutOfRangeException(argName ?? "arg",
                    "The provided value is greater than the maximum allowed value.");

                ex.Data["argValue"] = arg;
                ex.Data["maxValue"] = max;

                throw ex;
            }
        }
    }
}
