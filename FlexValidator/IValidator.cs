using System.Threading.Tasks;

namespace FlexValidator {
    /// <summary>
    /// Validates a single object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<in T> {
        /// <summary>
        /// Validates the model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IValidationResult Validate(T model);

        /// <summary>
        /// Async version of <see cref="Validate"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IValidationResult> ValidateAsync(T model);
    }
}