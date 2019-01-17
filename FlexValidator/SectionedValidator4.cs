using System;
using System.Threading.Tasks;
using FlexValidator.Helpers;

namespace FlexValidator {
    public abstract class SectionedValidator<T1, T2, T3, T4> : SectionedValidator, ISectionedValidator<T1, T2, T3, T4> {
        /// <inheritdoc/>
        public IValidationResult Validate(T1 model1, T2 model2, T3 model3, T4 model4) {
            return Validate(Helper.Pack(model1, model2, model3, model4));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateAsync(T1 model1, T2 model2, T3 model3, T4 model4) {
            return ValidateAsync(Helper.Pack(model1, model2, model3, model4));
        }

        /// <inheritdoc/>
        public IValidationResult ValidateSection(string section, T1 model1, T2 model2, T3 model3, T4 model4) {
            return ValidateSection(section, Helper.Pack(model1, model2, model3, model4));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2, T3 model3, T4 model4) {
            return ValidateSectionAsync(section, Helper.Pack(model1, model2, model3, model4));
        }

        /// <summary>
        /// Define a sync section by name to be run together will other defined sections when <see cref="Validate"/> or <see cref="ValidateAsync"/> is called. Can be
        /// run individually using <see cref="ValidateSection"/> or <see cref="ValidateSectionAsync"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="section"></param>
        protected internal void Section(string name, Action<T1, T2, T3, T4> section) {
            Section(name, Helper.Convert(section));
        }

        /// <summary>
        /// Define an async section by name to be run together will other defined sections when <see cref="Validate"/> or <see cref="ValidateAsync"/> is called. Can be
        /// run individually using <see cref="ValidateSection"/> or <see cref="ValidateSectionAsync"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="asyncSection"></param>
        protected internal void AsyncSection(string name, Func<T1, T2, T3, T4, Task> asyncSection) {
            AsyncSection(name, Helper.Convert(asyncSection));
        }
    }
}