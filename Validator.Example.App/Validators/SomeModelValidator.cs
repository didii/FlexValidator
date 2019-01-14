using System;
using System.Text.RegularExpressions;
using Validator.Example.App.Models;

namespace Validator.Example.App.Validators {
    /// <summary>
    /// Sectioned validator with complex calls
    /// </summary>
    class SomeModelValidator : SectionedValidator<SomeModel> {
        // Don't use magic strings, place the section names in constants
        public const string NameSection = "Name";
        public const string SubSection = "Sub";
        public const string BaseSection = "Base";
        public const string DoubleSection = "Double";

        /// <summary>
        /// With a sectioned validator define the sections of validators in the constructor
        /// </summary>
        public SomeModelValidator() {
            // Create a section that validates the base class of the model
            Section(BaseSection, ValidateBase);

            // Create a section with a locally declared validation method
            Section(NameSection, ValidateName);

            // Create a section with a locally declared validation method using a sub-validator
            Section(SubSection, ValidateSub);

            // Create a section that calls a double-validator (isolates 2 related properties)
            Section(DoubleSection, ValidateDouble);
        }

        private void ValidateBase(SomeModel model) {
            // Simply pass this model to the BaseModelValidator
            //RunValidators simply catches the result of the given validator
            //Do not use 'new' in your application, use your favorite DI container instead so you can mock this sub-validator
            RunValidator(new BaseModelValidator(), model);
        }

        private void ValidateName(SomeModel model) {
            // Rule: Name cannot be null or empty
            Start(new ValidationInfo("a40ef203-6e93-4341-887a-1618b2bb3e07", "Name must have a value", "some"));
            if (string.IsNullOrEmpty(model.Name)) {
                Fail();
                // Short-circuit here, if Name is null or empty, all other validations are moot
                return;
            }
            Complete(Assume.Pass);

            // Rule: Name must be at least 2 characters in length
            Start(new ValidationInfo("a3839435-f869-431c-9877-f425d8e9ea2c", "Name must have at least 2 characters", "some"));
            if (model.Name.Length >= 2)
                Pass();
            Complete(Assume.Fail);

            // Rule: Name must start with an alphabetical letter
            Start(new ValidationInfo("63df66cd-7138-4548-95b2-7fa6a1902ee6", "Name must start with an alphabetical letter", "some"));
            if (new Regex(@"^[a-zA-Z]").IsMatch(model.Name))
                Pass();
            Complete(Assume.Fail);

            // Rule: When length of Name is long enough and does not contain alphabetical letters, Name cannot be the developers one
            if (Passed("a3839435-f869-431c-9877-f425d8e9ea2c") && Passed("63df66cd-7138-4548-95b2-7fa6a1902ee6")) {
                Start(new ValidationInfo("cf214916-564c-4f98-a16f-9876f2343fbf", "Name cannot be didii", "some"));
                if (model.Name == "didii")
                    Fail();
                Complete(Assume.Pass);
            }
        }

        private void ValidateSub(SomeModel model) {
            //Rule: Sub cannot be null and must be valid
            Start(new ValidationInfo("395273d0-cae6-45e8-832b-05d3b2794728", "Sub cannot be null", "some"));
            if (model.Sub != null) {
                Pass();
                RunValidator(new SubModelValidator(), model.Sub);
            }
            Complete(Assume.Fail);
        }

        private void ValidateDouble(SomeModel model) {
            //Rule: DoubleLeft cannot be null
            Start(new ValidationInfo("8d0a0e11-c794-42bf-89e1-5e0969c3b59f", "Left cannot be null", "some"));
            if (model.DoubleLeft == null)
                Fail();
            Complete(Assume.Pass);

            //Rule: DoubleRight cannot be null
            Start(new ValidationInfo("29c0672a-72db-4894-b250-bd40d054cc76", "Right cannot be null", "some"));
            if (model.DoubleRight == null)
                Fail();
            Complete(Assume.Pass);

            //Rule: Run coupled validator if both are non-null
            if (Passed("8d0a0e11-c794-42bf-89e1-5e0969c3b59f") && Passed("29c0672a-72db-4894-b250-bd40d054cc76")) {
                RunValidator(new DoubleLeftRightValidator(), model.DoubleLeft, model.DoubleRight);
            }
        }
    }
}