using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Example.App.Validators {
    class DateTimeValidator : SimpleValidator<DateTime> {
        /// <inheritdoc />
        protected override void DoValidate(DateTime model) {
            //Rule: Date must be after 1999
            Start(new ValidationInfo("8d7af18b-1a13-4b7e-963d-68c6cd6636ca", "Date must be after 01-01-2000", "datetime"));
            if (model < new DateTime(2000, 1, 1))
                Fail();
            Complete(true);
        }
    }
}
