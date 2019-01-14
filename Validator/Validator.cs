using System;

namespace Validator {
    /// <summary>
    /// The base of a simple validator holding common functions of the typed versions
    /// </summary>
    /// <remarks>
    /// In it's simplest form, this is how the validation should be used.
    /// <code><see cref="Start"/>(new <see cref="ValidationInfo"/>("guid", "Id must be positive"));
    /// if (obj.Id > 0) {
    ///     <see cref="Pass"/>();
    /// else
    ///     <see cref="Fail"/>();
    /// <see cref="Complete"/>();</code>
    /// In words: Start every test with a single <see cref="Start"/> with information about the validation you want to do. Then write the
    /// validation logic and make sure this leads to a single call to <see cref="Pass"/> or <see cref="Fail"/>. Then complete the validation using
    /// <see cref="Complete"/>.
    /// </remarks>
    public abstract class Validator : BaseValidator {
        private ValidationInfo _lastValidation;

        /// <summary>
        /// The result of the ongoing validations. This is what keeps the state and should be treated with caution.
        /// </summary>
        internal ValidationResult Result { get; private set; }

        /// <summary>
        /// Start a single validation. Must be followed by a single <see cref="Pass"/> or <see cref="Fail"/> and is best ended with a
        /// <see cref="Complete"/>.
        /// </summary>
        /// <param name="info">Information regarding this specific validation rule</param>
        /// <exception cref="InvalidValidatorStateException">Thrown when a previous validation was not completed</exception>
        protected internal void Start(ValidationInfo info) {
            if (_lastValidation != null) {
                throw new InvalidValidatorStateException(
                    "Cannot start a new validation since the last one was not completed. Did you forget to call CompleteValidation, Pass or Fail?");
            }
            Complete();

            _lastValidation = info;
        }

        /// <summary>
        /// Ends a single validation. Optionally provide <paramref name="assumePass"/> to auto-pass or fail the test if <see cref="Pass"/> or <see cref="Fail"/>
        /// was not called.
        /// </summary>
        /// <param name="assumePass">
        /// True to assume the test to be passed if <see cref="Pass"/> or <see cref="Fail"/> was not called. False to assume the test to
        /// be failed.
        /// </param>
        /// <exception cref="InvalidValidatorStateException">
        /// Thrown when a validation was started, no <see cref="Pass"/> or <see cref="Fail"/> was called and no
        /// <paramref name="assumePass"/> given.
        /// </exception>
        protected internal void Complete(bool? assumePass = null) {
            if (_lastValidation == null) return;

            if (assumePass == null)
                throw new InvalidValidatorStateException(
                    "Cannot complete validation with CompleteValidation validation was not completed. Pass or Fail must be called before completion.");
            if (assumePass.Value)
                Pass();
            else
                Fail();
            ResetValidation();
        }

        /// <summary>
        /// Passes the validation started with <see cref="Start"/>. You can still call <see cref="Complete"/> after this.
        /// </summary>
        /// <exception cref="InvalidValidatorStateException">
        /// Thrown when no validation was started or already completed by another <see cref="Pass"/>,
        /// <see cref="Fail"/> or <see cref="Complete"/>
        /// </exception>
        protected internal void Pass() {
            if (_lastValidation == null)
                throw new InvalidValidatorStateException(
                    "A validation was not started when Pass was called. Every StartValidation can only be followed by a max of a single Pass or Fail.");
            Result.AddPass(_lastValidation);
            ResetValidation();
        }

        /// <summary>
        /// Fails the validation started with <see cref="Start"/>. You can still call <see cref="Complete"/> after this.
        /// </summary>
        /// <exception cref="InvalidValidatorStateException">
        /// Thrown when no validation was started or already completed by another <see cref="Pass"/>,
        /// <see cref="Fail"/> or <see cref="Complete"/>
        /// </exception>
        protected internal void Fail() {
            if (_lastValidation == null)
                throw new InvalidValidatorStateException(
                    "A validation was not started when Fail was called. Every StartValidation can only be followed by a max of a single Pass or Fail");
            Result.AddFail(_lastValidation);
            ResetValidation();
        }

        /// <summary>
        /// Check if the given validation was a success
        /// </summary>
        /// <param name="guid"></param>
        /// <exception cref="InvalidValidatorStateException">Thrown when the given validation does not exist</exception>
        /// <returns></returns>
        protected internal bool Passed(string guid) {
            var result = Result.Check(guid);
            if (result == null)
                throw new InvalidValidatorStateException("Cannot check Passed on a validation that does not exist. Check the GUID.");
            return result.Value;
        }

        /// <summary>
        /// Check if the given validatino failed
        /// </summary>
        /// <param name="guid"></param>
        /// <exception cref="InvalidValidatorStateException">Thrown when the given validation does not exist</exception>
        /// <returns></returns>
        protected internal bool Failed(string guid) {
            var result = Result.Check(guid);
            if (result == null)
                throw new InvalidValidatorStateException("Cannot check Failed on a validation that does not exist. Check the GUID.");
            return result.Value;
        }

        protected internal void RunValidator<T>(Validator<T> validator, T obj) {
            var subResult = validator.Validate(obj);
            Result.AddRange(subResult);
        }

        protected internal void RunValidator<T1, T2>(Validator<T1, T2> validator, T1 obj1, T2 obj2) {
            var subResult = validator.Validate(obj1, obj2);
            Result.AddRange(subResult);
        }

        /// <summary>
        /// Resets all validations built up in <see cref="Result"/>. Should normally not be used by you.
        /// </summary>
        protected void Reset() {
            Result = new ValidationResult();
        }

        private void ResetValidation() {
            _lastValidation = null;
        }
    }

    public abstract class Validator<T> : Validator {
        protected Validator() {
            ValidatorFunc = objs => Validate((T)objs[0]);
        }

        /// <inheritdoc />
        public abstract ValidationResult Validate(T obj);
    }

    public abstract class Validator<T1, T2> : Validator{
        protected Validator() {
            ValidatorFunc = objs => Validate((T1)objs[0], (T2)objs[1]);
        }

        /// <inheritdoc />
        public abstract ValidationResult Validate(T1 obj1, T2 obj2);
    }

    public abstract class Validator<T1, T2, T3> : Validator {
        protected Validator() {
            ValidatorFunc = objs => Validate((T1)objs[0], (T2)objs[1], (T3)objs[2]);
        }

        /// <inheritdoc />
        public abstract ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3);
    }
}