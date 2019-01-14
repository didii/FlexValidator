using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Validator {
    public class ValidationResult {
        private readonly IList<ValidationInfoBase> _fails;
        private readonly IList<ValidationInfoBase> _passes;

        public IEnumerable<ValidationInfoBase> Fails => _fails;
        public IEnumerable<ValidationInfoBase> Passes => _passes;

        public bool IsValid => _fails.Count == 0;

        public ValidationResult(IEnumerable<ValidationInfoBase> fails = null, IEnumerable<ValidationInfoBase> passes = null) {
            _fails = fails == null ? new List<ValidationInfoBase>() : fails.ToList();
            _passes = passes == null ? new List<ValidationInfoBase>() : passes.ToList();
        }

        internal void AddFail(ValidationInfoBase info) {
            _fails.Add(info);
        }

        internal void AddPass(ValidationInfoBase info) {
            _passes.Add(info);
        }

        internal void AddRange(ValidationResult other) {
            foreach (var otherFail in other.Fails)
                _fails.Add(otherFail);
            foreach (var otherPass in other.Passes)
                _passes.Add(otherPass);
        }

        internal bool? Check(string guid) {
            return Check(new Guid(guid));
        }

        /// <summary>
        /// Utility function that returns false if the given test failed, true if the given test passed and null if not present
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        internal bool? Check(Guid guid) {
            var fail = _fails.FirstOrDefault(x => x.Guid == guid);
            if (fail != null)
                return false;
            var pass = _passes.FirstOrDefault(x => x.Guid == guid);
            if (pass != null)
                return true;
            return null;
        }

    }
}