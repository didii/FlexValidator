﻿using System;
using System.Threading.Tasks;
using FlexValidator.Helpers;

namespace FlexValidator {
    public abstract class SectionedValidator<T1, T2, T3> : SectionedValidator, ISectionedValidator<T1, T2, T3> {
        /// <inheritdoc/>
        public IValidationResult Validate(T1 model1, T2 model2, T3 model3) {
            return Validate(Helper.Pack(model1, model2, model3));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateAsync(T1 model1, T2 model2, T3 model3) {
            return ValidateAsync(Helper.Pack(model1, model2, model3));
        }

        /// <inheritdoc/>
        public IValidationResult ValidateSection(string section, T1 model1, T2 model2, T3 model3) {
            return ValidateSection(section, Helper.Pack(model1, model2, model3));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2, T3 model3) {
            return ValidateSectionAsync(section, Helper.Pack(model1, model2, model3));
        }

        protected internal void Section(string name, Action<T1, T2, T3> section) {
            Section(name, Helper.Convert(section));
        }

        protected internal void AsyncSection(string name, Func<T1, T2, T3, Task> asyncSection) {
            AsyncSection(name, Helper.Convert(asyncSection));
        }
    }
}