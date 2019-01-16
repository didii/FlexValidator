using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlexValidator {
    public abstract class SimpleValidator<T1, T2, T3, T4> : SimpleValidator, IValidator<T1, T2, T3, T4> {
        /// <inheritdoc/>
        public ValidationResult Validate(T1 model1, T2 model2, T3 model3, T4 model4) {
            return Validate(Helper.Pack(model1, model2, model3, model4));
        }

        /// <inheritdoc/>
        public Task<ValidationResult> ValidateAsync(T1 model1, T2 model2, T3 model3, T4 model4) {
            return ValidateAsync(Helper.Pack(model1, model2, model3, model4));
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="obj1">The first argument given to <see cref="Validate"/></param>
        /// <param name="obj2">The second argument given to <see cref="Validate"/></param>
        /// <param name="obj3">The third argument given to <see cref="Validate"/></param>
        /// <param name="obj4">The fourth argument given to <see cref="Validate"/></param>
        protected virtual void DoValidate(T1 obj1, T2 obj2, T3 obj3, T4 obj4) {}

        protected virtual async Task DoValidateAsync(T1 obj1, T2 obj2, T3 obj3, T4 obj4) {}

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
