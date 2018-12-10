using System;
using System.Runtime.Serialization;

namespace Multi
{
    /// <summary>
    /// Exception that is thrown when a value is not supported.
    /// </summary>
    public class NotSupportedValueException : ApplicationException
    {
        public static string GetDefaultMessageOfNotSupportedValueException(Type type, object value)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "Value '{0}' of type '{1}' is not supported.", value, type.GetUserFriendlyTypeName(true, true));
        }

        public static NotSupportedValueException New<T>(T value)
        {
            return new NotSupportedValueException(typeof(T), value);
        }
        public static NotSupportedValueException New<T>(T value, string message)
        {
            return new NotSupportedValueException(typeof(T), value, message);
        }
        public static NotSupportedValueException New<T>(T value, Exception inner)
        {
            return new NotSupportedValueException(typeof(T), value, inner);
        }
        public static NotSupportedValueException New<T>(T value, string message, Exception inner)
        {
            return new NotSupportedValueException(typeof(T), value, message, inner);
        }

        /// <summary>
        /// Constructs a new instance of NotSupportedValueException with the given type and value.
        /// </summary>
        /// <param name="type">The type of the not spported value.</param>
        /// <param name="value">The not supported value.</param>
        protected NotSupportedValueException(Type type, object value)
            : base(GetDefaultMessageOfNotSupportedValueException(type, value))
        {
            Init(type, value);
        }

        /// <summary>
        /// Constructs a new instance of NotSupportedValueException with the given type, value and message.
        /// </summary>
        /// <param name="type">The type of the not spported value.</param>
        /// <param name="value">The not supported value.</param>
        /// <param name="message">Message for the exception.</param>
        protected NotSupportedValueException(Type type, object value, string message)
            : base(message)
        {
            Init(type, value);
        }

        /// <summary>
        /// Constructs a new instance of NotSupportedValueException with the given type, value and message.
        /// </summary>
        /// <param name="type">The type of the not spported value.</param>
        /// <param name="value">The not supported value.</param>
        /// <param name="inner">Inner exception.</param>
        protected NotSupportedValueException(Type type, object value, Exception inner)
            : base(GetDefaultMessageOfNotSupportedValueException(type, value), inner)
        {
            Init(type, value);
        }

        /// <summary>
        /// Constructs a new instance of NotSupportedValueException with the given type, value, message and inner exception.
        /// </summary>
        /// <param name="type">The type of the not spported value.</param>
        /// <param name="value">The not supported value.</param>
        /// <param name="message">Message for the exception.</param>
        /// <param name="inner">Inner exception.</param>
        protected NotSupportedValueException(Type type, object value, string message, Exception inner)
            : base(message, inner)
        {
            Init(type, value);
        }

        /// <summary>
        /// Constructor provided for serialization purposes.
        /// </summary>
        /// <param name="info">Serialization information</param>
        /// <param name="context">Context</param>
        protected NotSupportedValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.ValueString = info.GetString("ValueString");

                string typeName = info.GetString("Type.FullName");
                this.Type = Type.GetType(typeName, true);

                string typeOfValueName = info.GetString("TypeOfValue.FullName");
                if ((typeOfValueName != null) && (typeOfValueName != typeName))
                    this.TypeOfValue = Type.GetType(typeOfValueName, true);
                else
                    this.TypeOfValue = this.Type;

                try
                {
                    this.Value = info.GetValue("Value", this.TypeOfValue);
                }
                catch (System.Runtime.Serialization.SerializationException)// sEx)
                {
                    //this.PkValue = null;
                }
            }
        }

        /// <summary>
        /// Gets the type of the not supported value
        /// </summary>
        public Type Type { get; private set; }
        /// <summary>
        /// Gets the type of the not supported value instance
        /// </summary>
        public Type TypeOfValue { get; private set; }
        /// <summary>
        /// Gets the not suppported value
        /// </summary>
        public object Value { get; private set; }
        /// <summary>
        /// Gets a string representing the not supported value
        /// </summary>
        public string ValueString { get; private set; }

        private void Init(Type type, object value)
        {
            this.Type = type;
            this.Value = value;
            if (value != null)
            {
                this.TypeOfValue = value.GetType();
                this.ValueString = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", value);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                if (this.ValueString != null)
                    info.AddValue("ValueString", this.ValueString);
                if ((this.TypeOfValue != null) && (this.TypeOfValue.FullName != this.Type.FullName))
                    info.AddValue("TypeOfValue.FullName", this.TypeOfValue.FullName);

                info.AddValue("Type.FullName", this.Type.FullName);

                try
                {
                    info.AddValue("Value", this.Value);
                }
                catch (Exception)// ex)
                {
                    //lenyeljük
                }
            }
        }
    }
}
