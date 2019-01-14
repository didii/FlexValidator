using FlexValidator.Example.App.Models;

namespace FlexValidator.Example.App.Validators {
    /// <summary>
    /// A simple validator checking if the Id is valid
    /// </summary>
    class BaseModelValidator : SimpleValidator<BaseModel> {
        /// <inheritdoc />
        protected override void DoValidate(BaseModel model) {
            // Simple validation to check if Id is negative
            //Start the validation check
            Start(new ValidationInfoBase("20dedb00-4bbd-4ed0-8dc6-1b975e5cafb5", "Id cannot be negative"));
            //Custom logic making calls to Fail() or Pass()
            if (model.Id < 0)
                Fail();
            else
                Pass();
            //Complete the validation, not required when either Fail or Pass is called, but encouraged to use it
            Complete();

            // Other simple validation to check if Id is not zero
            //Start the validation check
            Start(new ValidationInfoBase("94558b8e-5b5b-433c-bf9f-2c398727baf4", "Id cannot be zero"));
            //Custom logic making a call to Fail()
            if (model.Id == 0)
                Fail();
            //Complete the validation where we set assumePass to true to assume the validation was passed if no Pass() or Fail() was encountered before
            Complete(Assume.Pass);
        }
    }
}