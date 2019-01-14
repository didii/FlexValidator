using FlexValidator.Example.App.Models;

namespace FlexValidator.Example.App.Validators {
    class SubModelTypeValidator : SimpleValidator<SubModelType> {
        /// <inheritdoc />
        protected override void DoValidate(SubModelType model) {
            //Rule: Type cannot be Prohibited
            Start(new ValidationInfoBase("fa605148-f2d7-4671-8732-bb392c57ccc7", "SubType cannot be prohibited"));
            if (model == SubModelType.Prohibited)
                Fail();
            Complete(Assume.Pass);
        }
    }
}
