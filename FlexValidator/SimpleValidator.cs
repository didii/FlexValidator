using System.Threading.Tasks;

namespace FlexValidator {
    public abstract class SimpleValidator : Validator {
        protected internal ValidationResult Validate(object[] models) {
            Reset();
            DoValidate(models);
            DoValidateAsync(models).GetAwaiter().GetResult();
            return Result;
        }

        protected internal async Task<ValidationResult> ValidateAsync(object[] models) {
            Reset();
            var task = DoValidateAsync(models);
            DoValidate(models);
            await task;
            return Result;
        }

        protected internal abstract void DoValidate(object[] models);
        protected internal abstract Task DoValidateAsync(object[] models);
    }
}