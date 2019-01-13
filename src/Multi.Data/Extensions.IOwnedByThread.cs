using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multi.Data
{
    public static partial class Extensions
	{
        /// <summary>
        /// Throws <see cref="WrongThreadException"/> if the current thread identifier not equals the <see cref="OwnerThreadID"/>
        /// </summary>
        /// <param name="obt"></param>
        /// <exception cref="WrongThreadException">Thrown if the current thread identifier not equals the <see cref="OwnerThreadID"/></exception>
        public static void EnsureOwnerThread(this IOwnedByThread obt)
        {
            if (obt == null)
                return;

            if (obt.OwnerThreadId != System.Threading.Thread.CurrentThread.ManagedThreadId)
                throw new WrongThreadException();
        }
        /// <summary>
        /// Throws <see cref="WrongThreadException"/> with the provided error message if the current thread identifier not equals the <see cref="OwnerThreadID"/>
        /// </summary>
        /// <param name="obt"></param>
        /// <param name="errorMsg">The error message</param>
        /// <exception cref="WrongThreadException">Thrown if the current thread identifier not equals the <see cref="OwnerThreadID"/></exception>
        public static void EnsureOwnerThread(this IOwnedByThread obt, string errorMsg)
        {
            if (obt == null)
                return;

            if (obt.OwnerThreadId != System.Threading.Thread.CurrentThread.ManagedThreadId)
                throw new WrongThreadException(errorMsg);
        }
        /// <summary>
        /// Throws <see cref="WrongThreadException"/> with the provided error message if the current thread identifier not equals the <see cref="OwnerThreadID"/>
        /// </summary>
        /// <param name="obt"></param>
        /// <param name="errorMsgFormat">The error message format</param>
        /// <param name="errorMsgParams">The parameters of the error message</param>
        /// <exception cref="WrongThreadException">Thrown if the current thread identifier not equals the <see cref="OwnerThreadID"/></exception>
        public static void EnsureOwnerThread(this IOwnedByThread obt, string errorMsgFormat, params object[] errorMsgParams)
        {
            if (obt == null)
                return;

            if (obt.OwnerThreadId != System.Threading.Thread.CurrentThread.ManagedThreadId)
                throw new WrongThreadException(string.Format(errorMsgFormat, errorMsgParams));
        }
        /// <summary>
        /// Throws <see cref="WrongThreadException"/> with the provided error message if the current thread identifier not equals the <see cref="OwnerThreadID"/>
        /// </summary>
        /// <param name="obt"></param>
        /// <param name="formatProvider">The format provider to use when formatting error message parameter values</param>
        /// <param name="errorMsgFormat">The error message format</param>
        /// <param name="errorMsgParams">The parameters of the error message</param>
        /// <exception cref="WrongThreadException">Thrown if the current thread identifier not equals the <see cref="OwnerThreadID"/></exception>
        public static void EnsureOwnerThread(this IOwnedByThread obt, System.IFormatProvider formatProvider, string errorMsgFormat, params object[] errorMsgParams)
        {
            if (obt == null)
                return;

            if (obt.OwnerThreadId != System.Threading.Thread.CurrentThread.ManagedThreadId)
                throw new WrongThreadException(string.Format(formatProvider, errorMsgFormat, errorMsgParams));
        }
	}
}
