using System.Threading.Tasks;

namespace FlexValidator {
    /// <summary>
    /// Base implementation of a simple validator
    /// </summary>
    public abstract class SimpleValidator : Validator {
        /// <summary>
        /// This validate call sets the state correctly and will execute both <see cref="DoValidate"/> and <see cref="DoValidateAsync"/> synchronously.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        protected internal IValidationResult Validate(object[] models) {
            Reset();
            DoValidate(models);
            DoValidateAsync(models).GetAwaiter().GetResult();
            return Result;
        }

        /// <summary>
        /// Async version of <see cref="Validate"/>
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        protected internal async Task<IValidationResult> ValidateAsync(object[] models) {
            Reset();
            var task = DoValidateAsync(models);
            DoValidate(models);
            await task;
            return Result;
        }

        /// <summary>
        /// The sync to-be implemented method for you own validations. You can implement both sync and async version and both will run.
        /// </summary>
        /// <param name="models"></param>
        protected internal abstract void DoValidate(object[] models);

        /// <summary>
        /// The async to-be implemented method for you own validations. You can implement both sync and async version and both will run.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        protected internal abstract Task DoValidateAsync(object[] models);
    }
}