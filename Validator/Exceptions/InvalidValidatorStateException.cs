using System;
using System.Runtime.Serialization;

namespace Validator {
    public class InvalidValidatorStateException : Exception {
        /// <inheritdoc />
        public InvalidValidatorStateException(string message) : base(message) { }

        /// <inheritdoc />
        public InvalidValidatorStateException(string message, Exception innerException) : base(message, innerException) { }

        /// <inheritdoc />
        protected InvalidValidatorStateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}