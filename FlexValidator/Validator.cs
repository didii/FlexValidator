using System;
using FlexValidator.Exceptions;

namespace FlexValidator {
    /// <summary>
    /// The base of a validator holding common functions
    /// </summary>
    /// <remarks>
    /// In it's simplest form, this is how the validation should be used.
    /// <code>Start(new ValidationInfoBase("guid", "Id must be positive"));
    /// if (obj.Id > 0) {
    ///     Pass();
    /// else
    ///     Fail();
    /// Complete();</code>
    /// In words: Start every test with a single <see cref="Start"/> with information about the validation you want to do. Then write the
    /// validation logic and make sure this leads to a single call to <see cref="Pass"/> or <see cref="Fail"/>. Then complete the validation using
    /// <see cref="Complete"/>.
    /// </remarks>
    public abstract class Validator {
        private ValidationInfoBase _lastValidation;

        /// <summary>
        /// Only use when creating new validator types. The result of the ongoing validations. This is what keeps the state and should be treated with caution.
        /// </summary>
        protected internal ValidationResult Result { get; private set; }

        /// <summary>
        /// Start a single validation. Must be followed by a single <see cref="Pass"/> or <see cref="Fail"/> and is best ended with a
        /// <see cref="Complete"/>.
        /// </summary>
        /// <param name="info">Information regarding this specific validation rule</param>
        /// <exception cref="InvalidValidatorStateException">Thrown when a previous validation was not completed</exception>
        protected internal void Start(ValidationInfoBase info) {
            if (_lastValidation != null) {
                throw new InvalidValidatorStateException(
                    "Cannot start a new validation since the last one was not completed. Did you forget to call CompleteValidation, Pass or Fail?");
            }
            Complete();

            _lastValidation = info;
        }

        /// <summary>
        /// Ends a single validation. Optionally provide <paramref name="assume"/> to auto-pass or fail the test if <see cref="Pass"/> or <see cref="Fail"/>
        /// was not called.
        /// </summary>
        /// <param name="assume">
        /// <see cref="Assume.Pass"/> to assume the test to pass or <see cref="Assume.Fail"/> to be failed.
        /// </param>
        /// <exception cref="InvalidValidatorStateException">
        /// Thrown when a validation was started, no <see cref="Pass"/> or <see cref="Fail"/> was called and no
        /// <paramref name="assume"/> given.
        /// </exception>
        protected internal void Complete(Assume? assume = null) {
            if (_lastValidation == null) return;

            if (assume == null)
                throw new InvalidValidatorStateException(
                    "Cannot complete validation with CompleteValidation validation was not completed. Pass or Fail must be called before completion.");

            switch (assume) {
                case Assume.Pass:
                    Pass();
                    break;
                case Assume.Fail:
                    Fail();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(assume), assume, null);
            }
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
        /// <param name="id"></param>
        /// <exception cref="InvalidValidatorStateException">Thrown when the given validation does not exist</exception>
        /// <returns></returns>
        protected internal bool Passed(string id) {
            var result = Result.Check(id);
            if (result == null)
                throw new ValidationNotFoundException(id);
            return result.Value;
        }

        /// <summary>
        /// Check if the given validation failed
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="InvalidValidatorStateException">Thrown when the given validation does not exist</exception>
        /// <returns></returns>
        protected internal bool Failed(string id) {
            var result = Result.Check(id);
            if (result == null)
                throw new ValidationNotFoundException(id);
            return !result.Value;
        }

        /// <summary>
        /// Run a sub-validator on the given model and capture its results
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator"></param>
        /// <param name="obj"></param>
        protected internal void RunValidator<T>(IValidator<T> validator, T obj) {
            var subResult = validator.Validate(obj);
            Result.Combine(subResult);
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
}