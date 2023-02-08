using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexValidator {
    public class ValidationResult : IValidationResult {
        private readonly IDictionary<string, ValidationInfoBase> _fails;
        private readonly IDictionary<string, ValidationInfoBase> _passes;

        /// <inheritdoc />
        public IEnumerable<ValidationInfoBase> Fails => _fails.Values;

        /// <inheritdoc />
        public IEnumerable<ValidationInfoBase> Passes => _passes.Values;

        /// <inheritdoc />
        public bool IsValid => _fails.Count == 0;

        public ValidationResult(IEnumerable<ValidationInfoBase> fails = null, IEnumerable<ValidationInfoBase> passes = null) {
            _fails = fails == null ? new Dictionary<string, ValidationInfoBase>() : fails.ToDictionary(x => x.Id, x => x);
            _passes = passes == null ? new Dictionary<string, ValidationInfoBase>() : passes.ToDictionary(x => x.Id, x => x);
        }

        /// <inheritdoc />
        public void AddFail(ValidationInfoBase info) {
            _fails.Add(info.Id, info);
        }

        /// <inheritdoc />
        public void AddPass(ValidationInfoBase info) {
            _passes.Add(info.Id, info);
        }

        /// <inheritdoc />
        public void Combine(IValidationResult other) {
            foreach (var fail in other.Fails)
                _fails.Add(fail.Id, fail);
            foreach (var pass in other.Passes)
                _passes.Add(pass.Id, pass);
        }

        /// <inheritdoc />
        public bool? Check(string id) {
            if (_fails.ContainsKey(id))
                return false;
            if (_passes.ContainsKey(id))
                return true;
            return null;
        }
    }
}