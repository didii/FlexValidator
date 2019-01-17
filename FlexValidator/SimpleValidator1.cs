using System.Threading.Tasks;
using FlexValidator.Base;
using FlexValidator.Helpers;

namespace FlexValidator {
    public abstract class SimpleValidator<T> : SimpleValidator, IValidator<T> {
        /// <inheritdoc/>
        public IValidationResult Validate(T model) {
            return Validate(Helper.Pack(model));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateAsync(T model) {
            return ValidateAsync(Helper.Pack(model));
        }

        /// <summary>
        /// Implement this method if you want sync validation. This method will be executed when you call <see cref="Validate"/> or <see cref="ValidateAsync"/>
        /// </summary>
        /// <param name="model"></param>
        protected virtual void DoValidate(T model) { }

        /// <summary>
        /// Implement this method if you async validation. This method will be executed when you call <see cref="Validate"/> or <see cref="ValidateAsync"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual async Task DoValidateAsync(T model) { }

        /// <inheritdoc/>
        internal override void DoValidate(object[] models) {
            Helper.UnPack<T>(DoValidate, models);
        }

        /// <inheritdoc/>
        internal override Task DoValidateAsync(object[] models) {
            return Helper.UnPackAsync<T>(DoValidateAsync, models);
        }
    }
}