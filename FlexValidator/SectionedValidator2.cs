using System;
using System.Threading.Tasks;
using FlexValidator.Helpers;

namespace FlexValidator {
    public abstract class SectionedValidator<T1, T2> : SectionedValidator, ISectionedValidator<T1, T2> {
        /// <inheritdoc/>
        public IValidationResult Validate(T1 model1, T2 model2) {
            return Validate(Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateAsync(T1 model1, T2 model2) {
            return ValidateAsync(Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public IValidationResult ValidateSection(string section, T1 model1, T2 model2) {
            return ValidateSection(section, Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2) {
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