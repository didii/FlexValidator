using System.Threading.Tasks;

namespace FlexValidator {
    public abstract class SimpleValidator : Validator {
        protected internal IValidationResult Validate(object[] models) {
            Reset();
            DoValidate(models);
            DoValidateAsync(models).GetAwaiter().GetResult();
            return Result;
        }

        protected internal async Task<IValidationResult> ValidateAsync(object[] models) {
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