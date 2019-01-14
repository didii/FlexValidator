using System;

namespace Validator {
    public class ValidationInfoBase {
        public ValidationInfoBase(string guid, string message) {
            Guid = new Guid(guid);
            Message = message;
        }

        public ValidationInfoBase(Guid guid, string message) {
            Guid = guid;
            Message = message;
        }

        internal ValidationInfoBase(Guid guid) {
            Guid = guid;
            Message = null;
        }

        public Guid Guid { get; }
        public string Message { get; }
    }
}