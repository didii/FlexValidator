using System;
using System.Threading.Tasks;

namespace FlexValidator.Tests.Validators {
    class TestSimpleValidator<T> : SimpleValidator<T> {
        public Action<T> ValidateFunc { get; set; }
        public Func<T, Task> ValidateAsyncFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T model) {
            ValidateFunc?.Invoke(model);
        }

        /// <inheritdoc />
        protected override Task DoValidateAsync(T model) {
            return ValidateAsyncFunc?.Invoke(model) ?? Task.CompletedTask;
        }
    }
}
