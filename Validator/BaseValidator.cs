using System;
using System.Linq;

namespace Validator {
    /// <summary>
    /// Base info for validations. Only for internal use.
    /// </summary>
    public class BaseValidator {
        /// <summary>
        /// The function that validates all given objects and returns a ValidationResult based on that
        /// </summary>
        internal Func<object[], ValidationResult> ValidatorFunc { get; set; }

        /// <summary>
        /// Runs <see cref="ValidatorFunc"/> on the given <paramref name="objs"/>.
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        internal virtual ValidationResult Validate(params object[] objs) {
            return ValidatorFunc(objs);
        }
    }
}