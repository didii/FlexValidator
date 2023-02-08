using System;
using System.Collections.Generic;

namespace FlexValidator {
    /// <summary>
    /// The result set of any validation
    /// </summary>
    public interface IValidationResult {
        /// <summary>
        /// Is this result set valid? Should return true if Fails contains no elements.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Contains all the validations that failed
        /// </summary>
        IEnumerable<ValidationInfoBase> Fails { get; }

        /// <summary>
        /// Contains all the validations that passed
        /// </summary>
        IEnumerable<ValidationInfoBase> Passes { get; }

        /// <summary>
        /// Add a failed validation
        /// </summary>
        /// <param name="info"></param>
        void AddFail(ValidationInfoBase info);

        /// <summary>
        /// Add a passed validation
        /// </summary>
        /// <param name="info"></param>
        void AddPass(ValidationInfoBase info);

        /// <summary>
        /// Combine the result set of another <see cref="IValidationResult"/> into this one
        /// </summary>
        /// <param name="other"></param>
        void Combine(IValidationResult other);

        /// <summary>
        /// Quick check if validation with ID <paramref name="guid"/> passed (true), failed (false) or does not exist (null).
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>True when it passed, false when it failed, null if it doesn't exist</returns>
        bool? Check(string guid);
    }
}