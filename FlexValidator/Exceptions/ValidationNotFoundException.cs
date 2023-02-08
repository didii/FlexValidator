using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FlexValidator.Exceptions {
    public class ValidationNotFoundException : Exception {
        private static string CreateMessage(string id) {
            return $"Validation with ID {id} was not found";
        }

        /// <inheritdoc />
        public ValidationNotFoundException(string id) : base(CreateMessage(id)) { }

        /// <inheritdoc />
        public ValidationNotFoundException(string id, Exception innerException) : base(CreateMessage(id), innerException) { }
    }
}
