using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexValidator {
    public class ValidationResult {
        private readonly IDictionary<Guid, ValidationInfoBase> _fails;
        private readonly IDictionary<Guid, ValidationInfoBase> _passes;

        public IEnumerable<ValidationInfoBase> Fails => _fails.Values;
        public IEnumerable<ValidationInfoBase> Passes => _passes.Values;

        public bool IsValid => _fails.Count == 0;

        public ValidationResult(IEnumerable<ValidationInfoBase> fails = null, IEnumerable<ValidationInfoBase> passes = null) {
            _fails = fails == null ? new Dictionary<Guid, ValidationInfoBase>() : fails.ToDictionary(x => x.Guid, x => x);
            _passes = passes == null ? new Dictionary<Guid, ValidationInfoBase>() : passes.ToDictionary(x => x.Guid, x => x);
        }

        internal void AddFail(ValidationInfoBase info) {
            _fails.Add(info.Guid, info);
        }

        internal void AddPass(ValidationInfoBase info) {
            _passes.Add(info.Guid, info);
        }

        internal void AddRange(ValidationResult other) {
            foreach (var fail in other.Fails)
                _fails.Add(fail.Guid, fail);
            foreach (var pass in other.Passes)
                _passes.Add(pass.Guid, pass);
        }

        /// <summary>
        /// Utility function that returns false if the given test failed, true if the given test passed and null if not present
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        internal bool? Check(Guid guid) {
            if (_fails.ContainsKey(guid))
                return false;
            if (_passes.ContainsKey(guid))
                return true;
            return null;
        }
    }
}