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
    public class UowChangeStateMismatchException : InvalidUowChangeStateException//, ISerializable
    {
        public static string GetDefaultMessageOfUowChangeStateMismatchException(UowChangeStateEnum currentChangeState, UowChangeStateEnum requiredChangeState)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Current change state '{0}' of the UnitOfWork does not equal the required value '{1}'.", currentChangeState, requiredChangeState);
        }

        #region CONSTRUCTORS
        protected UowChangeStateMismatchException()
        {}
        public UowChangeStateMismatchException(UowChangeStateEnum currentChangeState, UowChangeStateEnum requiredChangeState)
            : base(currentChangeState, GetDefaultMessageOfUowChangeStateMismatchException(currentChangeState, requiredChangeState))
        {
            this.RequiredChangeState = requiredChangeState;
        }

        public UowChangeStateMismatchException(UowChangeStateEnum currentChangeState, UowChangeStateEnum requiredChangeState, string message)
            : base(currentChangeState, message)
        {
            this.RequiredChangeState = requiredChangeState;
        }

        public UowChangeStateMismatchException(UowChangeStateEnum currentChangeState, UowChangeStateEnum requiredChangeState, string message, Exception innerException)
            : base(currentChangeState, message, innerException)
        {
            this.RequiredChangeState = requiredChangeState;
        }

        public UowChangeStateMismatchException(UowChangeStateEnum currentChangeState, UowChangeStateEnum requiredChangeState, Exception innerException)
            : base(currentChangeState, GetDefaultMessageOfUowChangeStateMismatchException(currentChangeState, requiredChangeState), innerException)
        {
            this.RequiredChangeState = requiredChangeState;
        }

#if !PCL
        protected UowChangeStateMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.RequiredChangeState = (UowChangeStateEnum)info.GetInt32("RequiredChangeState");
            }
        }
#endif
        #endregion CONSTRUCTORS

        #region PROPERTIES
        /// <summary>
        /// Gets the required change state
        /// </summary>
        public UowChangeStateEnum RequiredChangeState { get; private set; }
        #endregion PROPERTIES

#if !PCL
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("RequiredChangeState", (Int32)this.RequiredChangeState);
            }
        }
#endif
    }
}