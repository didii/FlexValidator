﻿using System;
using System.Threading.Tasks;
using FlexValidator.Helpers;

namespace FlexValidator {
    public abstract class SectionedValidator<T> : SectionedValidator, ISectionedValidator<T> {
        /// <inheritdoc/>
        public IValidationResult Validate(T model) {
            return Validate(Helper.Pack(model));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateAsync(T model) {
            return ValidateAsync(Helper.Pack(model));
        }

        /// <inheritdoc/>
        public IValidationResult ValidateSection(string section, T model) {
            return ValidateSection(section, Helper.Pack(model));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateSectionAsync(string section, T model) {
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