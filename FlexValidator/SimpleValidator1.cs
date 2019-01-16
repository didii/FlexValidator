﻿using System.Threading.Tasks;

namespace FlexValidator {
    public abstract class SimpleValidator<T> : SimpleValidator, IValidator<T> {
        /// <inheritdoc/>
        public ValidationResult Validate(T model) {
            return Validate(Helper.Pack(model));
        }

        /// <inheritdoc/>
        public Task<ValidationResult> ValidateAsync(T model) {
            return ValidateAsync(Helper.Pack(model));
        }

        protected virtual void DoValidate(T model) { }

        protected virtual async Task DoValidateAsync(T model) { }

        /// <inheritdoc/>
        protected internal override void DoValidate(object[] models) {
            Helper.UnPack<T>(DoValidate, models);
        }

        /// <inheritdoc/>
        protected internal override Task DoValidateAsync(object[] models) {
            return Helper.UnPackAsync<T>(DoValidateAsync, models);
        }
    }
}