namespace FlexValidator {
    public interface ISectionedValidator<in T> : IValidator<T> {
        ValidationResult ValidateSection(string sectionName, T obj);
    }

    public interface ISectionedValidator<in T1, in T2> : IValidator<T1, T2> {
        ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2);
    }

    public interface ISectionedValidator<in T1, in T2, in T3> : IValidator<T1, T2, T3> {
        ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2, T3 obj3);
    }

    public interface ISectionedValidator<in T1, in T2, in T3, in T4> : IValidator<T1, T2, T3, T4> {
        ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2, T3 obj3, T4 obj4);
    }
}