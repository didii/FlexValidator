using System.Threading.Tasks;

namespace FlexValidator {
    /// <summary>
    /// Validate a single model in sections
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISectionedValidator<in T> : IValidator<T> {
        /// <summary>
        /// Validate on the single <paramref name="section"/>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        IValidationResult ValidateSection(string section, T model);

        /// <summary>
        /// Validate async on the single <paramref name="section"/>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IValidationResult> ValidateSectionAsync(string section, T model);
    }
}