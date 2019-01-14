using System;

namespace Validator.Tests.Validators {
    class TestSectionedValidator<T> : SectionedValidator<T> {
        public void Init(Action<TestSectionedValidator<T>> init) {
            init(this);
        }
    }
}