using System.Threading.Tasks;

namespace FlexValidator {
    public interface ISectionedValidator<in T> : IValidator<T> {
        IValidationResult ValidateSection(string section, T model);

        Task<IValidationResult> ValidateSectionAsync(string section, T model);
    }

    public interface ISectionedValidator<in T1, in T2> : IValidator<T1, T2> {
        IValidationResult ValidateSection(string section, T1 model1, T2 model2);
        Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2);
    }

    public interface ISectionedValidator<in T1, in T2, in T3> : IValidator<T1, T2, T3> {
        IValidationResult ValidateSection(string section, T1 model1, T2 model2, T3 model3);
        Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2, T3 model3);
    }

    public interface ISectionedValidator<in T1, in T2, in T3, in T4> : IValidator<T1, T2, T3, T4> {
        IValidationResult ValidateSection(string section, T1 model1, T2 model2, T3 model3, T4 model4);
        Task<IValidationResult> ValidateSectionAsync(string section, T1 model1, T2 model2, T3 model3, T4 model4);
    }
}