namespace Validator {
    public interface ISectionedValidator<T> : IValidator<T> {
        ValidationResult ValidateSection(string sectionName, T obj);
    }

    public interface ISectionedValidator<T1, T2> : IValidator<T1, T2> {
        ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2);
    }

    public interface ISectionedValidator<T1, T2, T3> : IValidator<T1, T2, T3> {
        ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2, T3 obj3);
    }
}