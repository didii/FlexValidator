using System.Threading.Tasks;

namespace FlexValidator {
    /// <summary>
    /// Validate a single model in sections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISectionedValidator<in T> : IValidator<T> {
        /// <summary>
        /// Validate call with only the single section <paramref name="section"/>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        IValidationResult ValidateSection(string section, T model);

        /// <summary>
        /// Validate async call with only the single section <paramref name="section"/>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IValidationResult> ValidateSectionAsync(string section, T model);
    }

    /// <summary>
    /// Validate a duo of models in sections
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface ISectionedValidator<in T1, in T2> : IValidator<T1, T2> {
        /// <inheritdoc cref="ISectionedValidator{T}.ValidateSection"/>
        IValidationResult ValidateSection(string section, T1 model1, T2 model2);

        /// <inheritdoc cref="ISectionedValidator{T}.ValidateSectionAsync"/>
        Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2);
    }

    /// <summary>
    /// Validate a trio of models in sections
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public interface ISectionedValidator<in T1, in T2, in T3> : IValidator<T1, T2, T3> {
        /// <inheritdoc cref="ISectionedValidator{T}.ValidateSection"/>
        IValidationResult ValidateSection(string section, T1 model1, T2 model2, T3 model3);

        /// <inheritdoc cref="ISectionedValidator{T}.ValidateSectionAsync"/>
        Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2, T3 model3);
    }

    /// <summary>
    /// Validate a quarted of models in sections
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public interface ISectionedValidator<in T1, in T2, in T3, in T4> : IValidator<T1, T2, T3, T4> {
        /// <inheritdoc cref="ISectionedValidator{T}.ValidateSection"/>
        IValidationResult ValidateSection(string section, T1 model1, T2 model2, T3 model3, T4 model4);

        /// <inheritdoc cref="ISectionedValidator{T}.ValidateSectionAsync"/>
        Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2, T3 model3, T4 model4);
    }
}