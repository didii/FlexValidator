using System;

namespace FlexValidator {
    public class ValidationInfoBase {
        public ValidationInfoBase(string guid) {
            Guid = new Guid(guid);
            Message = null;
        }

        public ValidationInfoBase(string guid, string message) {
            Guid = new Guid(guid);
            Message = message;
        }

        public ValidationInfoBase(Guid guid) {
            Guid = guid;
            Message = null;
        }

        public ValidationInfoBase(Guid guid, string message) {
            Guid = guid;
            Message = message;
        }

        public Guid Guid { get; }
        public string Message { get; }
    }
}