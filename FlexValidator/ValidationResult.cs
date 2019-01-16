using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexValidator {
    public class ValidationResult : IValidationResult {
        private readonly IDictionary<Guid, ValidationInfoBase> _fails;
        private readonly IDictionary<Guid, ValidationInfoBase> _passes;

        /// <inheritdoc />
        public IEnumerable<ValidationInfoBase> Fails => _fails.Values;

        /// <inheritdoc />
        public IEnumerable<ValidationInfoBase> Passes => _passes.Values;

        /// <inheritdoc />
        public bool IsValid => _fails.Count == 0;

        public ValidationResult(IEnumerable<ValidationInfoBase> fails = null, IEnumerable<ValidationInfoBase> passes = null) {
            _fails = fails == null ? new Dictionary<Guid, ValidationInfoBase>() : fails.ToDictionary(x => x.Guid, x => x);
            _passes = passes == null ? new Dictionary<Guid, ValidationInfoBase>() : passes.ToDictionary(x => x.Guid, x => x);
        }

        /// <inheritdoc />
        public void AddFail(ValidationInfoBase info) {
            _fails.Add(info.Guid, info);
        }

        /// <inheritdoc />
        public void AddPass(ValidationInfoBase info) {
            _passes.Add(info.Guid, info);
        }

        /// <inheritdoc />
        public void Combine(IValidationResult other) {
            foreach (var fail in other.Fails)
                _fails.Add(fail.Guid, fail);
            foreach (var pass in other.Passes)
                _passes.Add(pass.Guid, pass);
        }

        /// <inheritdoc />
        public bool? Check(Guid guid) {
            if (_fails.ContainsKey(guid))
                return false;
            if (_passes.ContainsKey(guid))
                return true;
            return null;
        }
    }
}