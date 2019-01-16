using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FlexValidator.Exceptions {
    public class ValidationNotFoundException : Exception {
        private static string CreateMessage(Guid guid) {
            return $"Validation with ID {guid} was not found";
        }

        /// <inheritdoc />
        public ValidationNotFoundException(Guid guid) : base(CreateMessage(guid)) { }

        /// <inheritdoc />
        public ValidationNotFoundException(Guid guid, Exception innerException) : base(CreateMessage(guid), innerException) { }
    }
}
