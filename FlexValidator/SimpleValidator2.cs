using System.Threading.Tasks;

namespace FlexValidator {
    public abstract class SimpleValidator<T1, T2> : SimpleValidator, IValidator<T1, T2> {
        /// <inheritdoc/>
        public ValidationResult Validate(T1 model1, T2 model2) {
            return Validate(Helper.Pack(model1, model2));
        }

        /// <inheritdoc/>
        public async Task<ValidationResult> ValidateAsync(T1 model1, T2 model2) {
            return Validate(Helper.Pack(model1, model2));
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="model1">The first argument given to <see cref="Validate"/></param>
        /// <param name="model2">The second argument given to <see cref="Validate"/></param>
        protected virtual void DoValidate(T1 model1, T2 model2) { }

        protected virtual async Task DoValidateAsync(T1 model1, T2 model2) { }

        /// <inheritdoc/>
        protected internal override void DoValidate(object[] models) {
            Helper.UnPack<T1, T2>(DoValidate, models);
        }

        /// <inheritdoc/>
        protected internal override Task DoValidateAsync(object[] models) {
            return Helper.UnPackAsync<T1, T2>(DoValidateAsync, models);
        }
    }
}