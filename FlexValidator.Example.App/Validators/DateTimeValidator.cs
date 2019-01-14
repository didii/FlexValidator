using System;

namespace FlexValidator.Example.App.Validators {
    class DateTimeValidator : SimpleValidator<DateTime> {
        /// <inheritdoc/>
        protected override void DoValidate(DateTime model) {
            //Rule: Date must be after 1999
            Start(new ValidationInfoBase("8d7af18b-1a13-4b7e-963d-68c6cd6636ca", "Date must be after 01-01-2000"));
            if (model < new DateTime(2000, 1, 1))
                Fail();
            Complete(Assume.Pass);
        }
    }
}