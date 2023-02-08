using System.Threading.Tasks;

namespace FlexValidator
{
    /// <summary>
    /// Do not use. Base implementation of a simple validator that holds all logic and has no visible members outside of its assembly. 
    /// </summary>
    public class SimpleValidator<T> : Validator, IValidator<T>
    {
        /// <summary>
        /// This validate call sets the state correctly and will execute both <see cref="DoValidate"/> and <see cref="DoValidateAsync"/> synchronously.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IValidationResult Validate(T model)
        {
            Reset();
            DoValidate(model);
            DoValidateAsync(model).GetAwaiter().GetResult();
            return Result;
        }

        /// <summary>
        /// Async version of <see cref="Validate"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IValidationResult> ValidateAsync(T model)
        {
            Reset();
            var task = DoValidateAsync(model);
            DoValidate(model);
            await task;
            return Result;
        }

        /// <summary>
        /// The sync to-be implemented method for you own validations. You can implement both sync and async version and both will run.
        /// </summary>
        /// <param name="model"></param>
        protected virtual void DoValidate(T model)
        {
        }

        /// <summary>
        /// The async to-be implemented method for you own validations. You can implement both sync and async version and both will run.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual Task DoValidateAsync(T model)
        {
            return Task.CompletedTask;
        }
    }
}