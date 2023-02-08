using System;

namespace FlexValidator.Tests.Validators {
    class TestSectionedValidator<T> : SectionedValidator<T> {
        public void Init(Action init) {
            init();
        }
    }
}