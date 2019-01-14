using System;

namespace FlexValidator.Exceptions {
    public class ValidatorSectionNotFoundException : Exception {
        private static string CreateMessage(string sectionName) {
            return $"The section '{sectionName}' was not found in this sectioned validator";
        }

        /// <inheritdoc />
        public ValidatorSectionNotFoundException(string sectionName) : base(CreateMessage(sectionName)) { }

        /// <inheritdoc />
        public ValidatorSectionNotFoundException(string sectionName, Exception innerException) : base(CreateMessage(sectionName), innerException) { }
    }
}
