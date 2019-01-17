using System;

namespace FlexValidator.Tests.Validators {
    class TestSectionedValidator<T> : SectionedValidator<T> {
        public void Init(Action init) {
            init();
        }
    }

    class TestSectionedValidator<T1, T2> : SectionedValidator<T1, T2> {
        public void Init(Action init) {
            init();
        }
    }

    class TestSectionedValidator<T1, T2, T3> : SectionedValidator<T1, T2, T3> {
        public void Init(Action init) {
            init();
        }
    }

    class TestSectionedValidator<T1, T2, T3, T4> : SectionedValidator<T1, T2, T3, T4> {
        public void Init(Action init) {
            init();
        }
    }
}