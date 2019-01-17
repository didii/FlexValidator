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

    class TestSimpleValidator<T1, T2> : SimpleValidator<T1, T2> {
        public Action<T1, T2> ValidateFunc { get; set; }
        public Func<T1, T2, Task> ValidateAsyncFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T1 model1, T2 model2) {
            ValidateFunc?.Invoke(model1, model2);
        }

        /// <inheritdoc />
        protected override Task DoValidateAsync(T1 model1, T2 model2) {
            return ValidateAsyncFunc?.Invoke(model1, model2) ?? Task.CompletedTask;
        }
    }

    class TestSimpleValidator<T1, T2, T3> : SimpleValidator<T1, T2, T3> {
        public Action<T1, T2, T3> ValidateFunc { get; set; }
        public Func<T1, T2, T3, Task> ValidateAsyncFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T1 model1, T2 model2, T3 model3) {
            ValidateFunc?.Invoke(model1, model2, model3);
        }

        /// <inheritdoc />
        protected override Task DoValidateAsync(T1 model1, T2 model2, T3 model3) {
            return ValidateAsyncFunc?.Invoke(model1, model2, model3) ?? Task.CompletedTask;
        }
    }

    class TestSimpleValidator<T1, T2, T3, T4> : SimpleValidator<T1, T2, T3, T4> {
        public Action<T1, T2, T3, T4> ValidateFunc { get; set; }
        public Func<T1, T2, T3, T4, Task> ValidateAsyncFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T1 model1, T2 model2, T3 model3, T4 model4) {
            ValidateFunc?.Invoke(model1, model2, model3, model4);
        }

        /// <inheritdoc />
        protected override Task DoValidateAsync(T1 model1, T2 model2, T3 model3, T4 model4) {
            return ValidateAsyncFunc?.Invoke(model1, model2, model3, model4) ?? Task.CompletedTask;
        }
    }
}
