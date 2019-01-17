using System.Threading.Tasks;
using FlexValidator.Base;
using FlexValidator.Helpers;

namespace FlexValidator {
    public abstract class SimpleValidator<T1, T2, T3, T4> : SimpleValidator, IValidator<T1, T2, T3, T4> {
        /// <inheritdoc/>
        public IValidationResult Validate(T1 model1, T2 model2, T3 model3, T4 model4) {
            return Validate(Helper.Pack(model1, model2, model3, model4));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateAsync(T1 model1, T2 model2, T3 model3, T4 model4) {
            return ValidateAsync(Helper.Pack(model1, model2, model3, model4));
        }

        /// <summary>
        /// Implement this method if you want sync validation. This method will be executed when you call <see cref="Validate"/> or <see cref="ValidateAsync"/>
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <param name="model3"></param>
        /// <param name="model4"></param>
        protected virtual void DoValidate(T1 model1, T2 model2, T3 model3, T4 model4) { }

        /// <summary>
        /// Implement this method if you async validation. This method will be executed when you call <see cref="Validate"/> or <see cref="ValidateAsync"/>
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <param name="model3"></param>
        /// <param name="model4"></param>
        /// <returns></returns>
        protected virtual async Task DoValidateAsync(T1 model1, T2 model2, T3 model3, T4 model4) { }

        /// <inheritdoc/>
        internal override void DoValidate(object[] models) {
            Helper.UnPack<T1, T2, T3, T4>(DoValidate, models);
        }

        /// <inheritdoc/>
        internal override Task DoValidateAsync(object[] models) {
            return Helper.UnPackAsync<T1, T2, T3, T4>(DoValidateAsync, models);
        }
    }
}