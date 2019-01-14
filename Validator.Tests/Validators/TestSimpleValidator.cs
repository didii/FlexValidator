using System;

namespace Validator.Tests.Validators {
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
        protected override void DoValidate(T1 obj1, T2 obj2) {
            ValidateFunc(obj1, obj2);
        }
    }

    class TestSimpleValidator<T1, T2, T3> : SimpleValidator<T1, T2, T3> {
        private Action<T1, T2, T3> ValidateFunc { get; set; }

        /// <inheritdoc />
        protected override void DoValidate(T1 obj1, T2 obj2, T3 obj3) {
            ValidateFunc(obj1, obj2, obj3);
        }
    }
}
