using System;
using System.Text;
using System.Threading;

namespace Multi
{
    //http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P
    //https://gist.github.com/jcdickinson/4196875
    //http://blog.tcx.be/2012/11/properly-using-and-implementing.html
    /// <summary>
    /// Abstract base class for disposable classes
    /// </summary>
	public abstract class DisposableBase : IDisposable
    {
        private const int DISPOSING = 1;
        private const int DISPOSED = 2;

        private int _isDisposed = 0;
        protected bool IsDisposed
        {
            get { return _isDisposed == DISPOSED; }
            private set
            {
                int wasDisposed = Interlocked.Exchange(ref _isDisposed, DISPOSED);
            }
        }
        protected bool IsDisposing
        {
            get { return _isDisposed == DISPOSING; }
        }
        protected bool IsDisposingOrDisposed
        {
            get { return _isDisposed != 0; }
        }
        private bool TryStartDisposing()
        {
            int wasDisposed = Interlocked.CompareExchange(ref _isDisposed, DISPOSING, 0);
            return wasDisposed == 0;
        }

        protected void ThrowIfDisposingOrDisposed()
        {
            if (this.IsDisposingOrDisposed)
                throw new ObjectDisposedException(this.GetType().GetFriendlyTypeName(), GetDisposedErrMsg());
        }
        protected virtual string GetDisposedErrMsg()
        {
            return new StringBuilder().AppendFriendlyTypeName(this.GetType()).Append(" is disposing or has already been disposed!").ToString();
        }

        /// Tries to dispose the given object reference and set to null in a thread-safe way.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="IDisposable"/> object.</typeparam>
        /// <param name="d">The disposable object reference.</param>
        /// <returns>Returns true if the <paramref name="d"/> object instance was not null and was diposed successfully.</returns>
        protected bool TryDisposeAndSetToNull<T>(ref T d)
            where T : class, IDisposable
        {
            // http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P
            // https://gist.github.com/jcdickinson/4196875
            // 1. set to null
            T tmp = Interlocked.Exchange<T>(ref d, null);
            // 2. if it was not null
            if (tmp != null)
            {
                // 3. then dispose
                tmp.Dispose();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Disposes this object instance if it's not null and implements the <see cref="IDisposable"/> interface.
        /// </summary>
        /// <param name="o">The object to dispose</param>
        /// <returns>Returns true if the object was not null and was diposed successfully.</returns>
        protected bool TryDispose(object o)
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

        #region IDisposable Members        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (TryStartDisposing())
            {
                try
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
                finally
                {
                    this.IsDisposed = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);
    }
}
