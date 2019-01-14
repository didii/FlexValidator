using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Example.App.Models;

namespace Validator.Example.App.Validators {
    class SubModelTypeValidator : SimpleValidator<SubModelType> {
        /// <inheritdoc />
        protected override void DoValidate(SubModelType model) {
            //Rule: Type cannot be Prohibited
            Start(new ValidationInfo("fa605148-f2d7-4671-8732-bb392c57ccc7", "SubType cannot be prohibited", "submodeltype"));
            if (model == SubModelType.Prohibited)
                Fail();
            Complete(Assume.Pass);
        }
    }
}
