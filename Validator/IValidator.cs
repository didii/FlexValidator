namespace Validator {
    /// <summary>
    /// Validates a single object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<in T> {
        ValidationResult Validate(T obj);
    }

    /// <summary>
    /// Validates 2 elements at once. This should only be used if the validation of <typeparamref name="T1"/> depends on <typeparamref name="T2"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IValidator<in T1, in T2> {
        ValidationResult Validate(T1 obj1, T2 obj2);
    }

    /// <summary>
    /// Validates 3 objects at once. This should only be used if all three types depend on one and other
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public interface IValidator<in T1, in T2, in T3> {
        ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3);
    }
}