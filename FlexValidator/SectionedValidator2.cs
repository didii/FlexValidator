using System;
using System.Threading.Tasks;

namespace FlexValidator {
    public abstract class SectionedValidator<T1, T2> : SectionedValidator, ISectionedValidator<T1, T2> {
        /// <inheritdoc/>
        public ValidationResult Validate(T1 model1, T2 model2) {
            return Validate(Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public Task<ValidationResult> ValidateAsync(T1 model1, T2 model2) {
            return ValidateAsync(Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public ValidationResult ValidateSection(string section, T1 model1, T2 model2) {
            return ValidateSection(section, Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public Task<ValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2) {
            return ValidateSectionAsync(section, Helper.Pack(model1, model2));
        }

        protected internal void Section(string name, Action<T1, T2> section) {
            Section(name, Helper.Convert(section));
        }

        protected internal void SectionAsync(string name, Func<T1, T2, Task> asyncSection) {
            AsyncSection(name, Helper.Convert(asyncSection));
        }
    }
}