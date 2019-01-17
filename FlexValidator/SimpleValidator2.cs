using System.Threading.Tasks;
using FlexValidator.Helpers;

namespace FlexValidator {
    public abstract class SimpleValidator<T1, T2> : SimpleValidator, IValidator<T1, T2> {
        /// <inheritdoc/>
        public IValidationResult Validate(T1 model1, T2 model2) {
            return Validate(Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public Task<IValidationResult> ValidateAsync(T1 model1, T2 model2) {
            return ValidateAsync(Helper.Pack(model1, model2));
        }

        protected virtual void DoValidate(T1 model1, T2 model2) { }

        protected virtual async Task DoValidateAsync(T1 model1, T2 model2) { }

        /// <inheritdoc/>
        internal override void DoValidate(object[] models) {
            Helper.UnPack<T1, T2>(DoValidate, models);
        }

        /// <inheritdoc/>
        internal override Task DoValidateAsync(object[] models) {
            return Helper.UnPackAsync<T1, T2>(DoValidateAsync, models);
        }
    }
}