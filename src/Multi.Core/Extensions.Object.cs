using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using Multi;
using System.Threading;

namespace System
{
    public static partial class Extensions
    {
        /// <summary>
        /// Disposes this object instance if it's not null and implements the <see cref="IDisposable"/> interface.
        /// </summary>
        /// <param name="o">The object to dispose</param>
        /// <returns>Returns true if the object was not null and was diposed successfully.</returns>
        public static bool TryDispose(this object o)
        {
            if (object.ReferenceEquals(o, null))
                return false;

            IDisposable d = o as IDisposable;
            if (d != null)
            {
                d.Dispose();
                System.Diagnostics.Debug.WriteLine(string.Format("{0} instance disposed.", d.GetType().GetFriendlyTypeName()), "Dispose");
                return true;
            }

            return false;
        }
    }
}
