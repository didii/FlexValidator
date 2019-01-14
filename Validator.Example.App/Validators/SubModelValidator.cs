using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Example.App.Models;

namespace Validator.Example.App.Validators {
    class SubModelValidator : SectionedValidator<SubModel> {
        public const string NameSection = "name";
        public const string TypeSection = "type";
        public const string DateTimeAndTypeSection = "datetime|type";

        public SubModelValidator() {
            // Create an inline-section
            //Not recommended but possible nonetheless
            Section(NameSection, x => {
                //Rule: Name cannot be null or empty
                Start(new ValidationInfo("a6eb736f-6d7e-47dc-a5cf-4bc6cffe3a63", "Name must have a value", "sub"));
                if (string.IsNullOrEmpty(x.Name))
                    Fail();
                Complete(true);
            });

            // Run a sub-validator
            Section(TypeSection, m => RunValidator(new SubModelTypeValidator(), m.Type));

            // Run another sub-validator with a condition
            Section(DateTimeAndTypeSection, ValidateDateTimeAndType);
        }

        private void ValidateDateTimeAndType(SubModel model) {
            //Rule: if Allowed, Date cannot be null
            Start(new ValidationInfo("5ce91719-965a-425b-a7b3-2683ccaeedbd", "Date is mandatory when Allowed is selected", "sub"));
            if (model.Type == SubModelType.Allowed && model.DateTime == null)
                Fail();
            Complete(true);
        }
    }
}
