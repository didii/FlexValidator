using System;
using System.Threading.Tasks;

namespace FlexValidator {
    public abstract class SectionedValidator<T> : SectionedValidator, ISectionedValidator<T> {
        /// <inheritdoc/>
        public ValidationResult Validate(T model) {
            return Validate(Helper.Pack(model));
        }

        /// <inheritdoc/>
        public Task<ValidationResult> ValidateAsync(T model) {
            return ValidateAsync(Helper.Pack(model));
        }

        /// <inheritdoc/>
        public ValidationResult ValidateSection(string section, T model) {
            return ValidateSection(section, Helper.Pack(model));
        }

        /// <inheritdoc/>
        public Task<ValidationResult> ValidateSectionAsync(string section, T model) {
            return ValidateSectionAsync(section, Helper.Pack(model));
        }

        protected internal void Section(string name, Action<T> section) {
            Section(name, Helper.Convert(section));
        }

        protected internal void AsyncSection(string name, Func<T, Task> asyncSection) {
            AsyncSection(name, Helper.Convert(asyncSection));
        }
    }
}