using System;

namespace FlexValidator.Tests.Validators {
    class TestSimpleValidator<T> : SimpleValidator<T> {
        public Action<T> ValidateFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T model) {
            ValidateFunc(model);
        }
    }

    class TestSimpleValidator<T1, T2> : SimpleValidator<T1, T2> {
        public Action<T1, T2> ValidateFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T1 model1, T2 model2) {
            ValidateFunc(model1, model2);
        }
    }

    class TestSimpleValidator<T1, T2, T3> : SimpleValidator<T1, T2, T3> {
        private Action<T1, T2, T3> ValidateFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T1 model1, T2 model2, T3 model3) {
            ValidateFunc(model1, model2, model3);
        }
    }

    class TestSimpleValidator<T1, T2, T3, T4> : SimpleValidator<T1, T2, T3, T4> {
        private Action<T1, T2, T3> ValidateFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T1 model1, T2 model2, T3 model3, T4 model4) {
            ValidateFunc(model1, model2, model3);
        }
    }
}
