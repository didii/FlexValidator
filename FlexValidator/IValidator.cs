namespace FlexValidator {
    /// <summary>
    /// Validates a single object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<in T> {
        /// <summary>
        /// Validates the model
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        ValidationResult Validate(T obj);
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
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        ValidationResult Validate(T1 obj1, T2 obj2);
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
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <param name="obj3"></param>
        /// <returns></returns>
        ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3);
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
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <param name="obj3"></param>
        /// <param name="obj4"></param>
        /// <returns></returns>
        ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3, T4 obj4);
    }
}