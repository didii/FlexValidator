using System;

namespace Validator {
    public class ValidationInfo {
        public Guid Guid { get; }
        public string[] Keys { get; }
        public string Message { get; }

        internal ValidationInfo(Guid guid) {
            Guid = guid;
        }

        public ValidationInfo(string guid, string message, params string[] keys) {
            Guid = new Guid(guid);
            Keys = keys;
            Message = message;
        }

        public ValidationInfo(Guid guid, string message, params string[] keys) {
            Guid = guid;
            Keys = keys;
            Message = message;
        }
    }
}