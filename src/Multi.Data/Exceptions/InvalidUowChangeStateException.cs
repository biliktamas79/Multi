using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Multi.Data
{
#if !PCL
    [Serializable]
#endif
    public class InvalidUowChangeStateException : ApplicationException//, ISerializable
    {
        public static string GetDefaultMessageOfInvalidUowChangeStateException(UowChangeStateEnum currentChangeState)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "The method or operation is not valid when the UnitOfWork is in the '{0}' change state.", currentChangeState);
        }

        #region CONSTRUCTORS
        protected InvalidUowChangeStateException()
        {}
        public InvalidUowChangeStateException(UowChangeStateEnum currentChangeState)
            : base(GetDefaultMessageOfInvalidUowChangeStateException(currentChangeState))
        {
            this.CurrentChangeState = currentChangeState;
        }

        public InvalidUowChangeStateException(UowChangeStateEnum currentChangeState, string message)
            : base(message)
        {
            this.CurrentChangeState = currentChangeState;
        }

        public InvalidUowChangeStateException(UowChangeStateEnum currentChangeState, string message, Exception innerException)
            : base(message, innerException)
        {
            this.CurrentChangeState = currentChangeState;
        }

        public InvalidUowChangeStateException(UowChangeStateEnum currentChangeState, Exception innerException)
            : base(GetDefaultMessageOfInvalidUowChangeStateException(currentChangeState), innerException)
        {
            this.CurrentChangeState = currentChangeState;
        }

#if !PCL
        protected InvalidUowChangeStateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.CurrentChangeState = (UowChangeStateEnum)info.GetInt32("CurrentChangeState");
            }
        }
#endif
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the current change state
        /// </summary>
        public UowChangeStateEnum CurrentChangeState { get; private set; }
        #endregion PROPERTIES

#if !PCL
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("CurrentChangeState", (Int32)this.CurrentChangeState);
            }
        }
#endif
    }
}