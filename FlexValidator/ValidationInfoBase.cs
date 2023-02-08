using System;

namespace FlexValidator {
    public class ValidationInfoBase {
        public ValidationInfoBase(string id) {
            Id = id;
            Message = null;
        }

        public ValidationInfoBase(string id, string message) {
            Id = id;
            Message = message;
        }

        public string Id { get; }

        public string Message { get; }
    }
}