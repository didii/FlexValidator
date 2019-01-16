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
        ValidationResult Validate(T model);

        /// <summary>
        /// Async version of <see cref="Validate"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ValidationResult> ValidateAsync(T model);
    }

    /// <summary>
    /// Validates 2 elements at once. This should only be used if the validation of <typeparamref name="T1"/> depends on <typeparamref name="T2"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IValidator<in T1, in T2> {
        /// <summary>
        /// Validates the pair of models
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <returns></returns>
        ValidationResult Validate(T1 model1, T2 model2);

        Task<ValidationResult> ValidateAsync(T1 model1, T2 model2);
    }

    /// <summary>
    /// Validates 3 objects at once. This should only be used if all three types depend on one and other
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public interface IValidator<in T1, in T2, in T3> {
        /// <summary>
        /// Validates the trio of models
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <param name="model3"></param>
        /// <returns></returns>
        ValidationResult Validate(T1 model1, T2 model2, T3 model3);

        Task<ValidationResult> ValidateAsync(T1 model1, T2 model2, T3 model3);
    }

    /// <summary>
    /// Validate 4 objects at once. This should only be used if all four objects validations depend on each other
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public interface IValidator<in T1, in T2, in T3, in T4> {
        /// <summary>
        /// Validates the quartet of models
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <param name="model3"></param>
        /// <param name="model4"></param>
        /// <returns></returns>
        ValidationResult Validate(T1 model1, T2 model2, T3 model3, T4 model4);

        Task<ValidationResult> ValidateAsync(T1 model1, T2 model2, T3 model3, T4 model4);
    }
}