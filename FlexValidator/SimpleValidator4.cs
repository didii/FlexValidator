using System.Threading.Tasks;
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

        protected virtual void DoValidate(T1 model1, T2 model2, T3 model3, T4 model4) { }

        protected virtual async Task DoValidateAsync(T1 model1, T2 model2, T3 model3, T4 model4) { }

        /// <inheritdoc/>
        protected internal override void DoValidate(object[] models) {
            Helper.UnPack<T1, T2, T3, T4>(DoValidate, models);
        }

        /// <inheritdoc/>
        protected internal override Task DoValidateAsync(object[] models) {
            return Helper.UnPackAsync<T1, T2, T3, T4>(DoValidateAsync, models);
        }
    }
}