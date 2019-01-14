namespace Validator {
    public abstract class SimpleValidator<T> : Validator<T> {
        protected SimpleValidator() {
            ValidatorFunc = objs => Validate((T)objs[0]);
        }

        /// <inheritdoc/>
        public sealed override ValidationResult Validate(T obj) {
            Reset();
            DoValidate(obj);
            return Result;
        }

        /// <inheritdoc/>
        internal override ValidationResult Validate(params object[] objs) {
            Reset();
            return base.Validate(objs);
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="model">The object given with <see cref="Validate"/></param>
        protected abstract void DoValidate(T model);
    }

    public abstract class SimpleValidator<T1, T2> : Validator<T1, T2> {
        public SimpleValidator() {
            ValidatorFunc = objs => Validate((T1)objs[0], (T2)objs[1]);
        }

        /// <inheritdoc/>
        public sealed override ValidationResult Validate(T1 obj1, T2 obj2) {
            Reset();
            DoValidate(obj1, obj2);
            return Result;
        }

        /// <inheritdoc/>
        internal override ValidationResult Validate(params object[] objs) {
            Reset();
            return base.Validate(objs);
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="obj1">The first argument given to <see cref="Validate"/></param>
        /// <param name="obj2">The second argument given to <see cref="Validate"/></param>
        protected abstract void DoValidate(T1 obj1, T2 obj2);
    }

    public abstract class SimpleValidator<T1, T2, T3> : Validator<T1, T2, T3> {
        public SimpleValidator() {
            ValidatorFunc = objs => Validate((T1)objs[0], (T2)objs[1], (T3)objs[2]);
        }

        /// <inheritdoc/>
        public sealed override ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3) {
            Reset();
            DoValidate(obj1, obj2, obj3);
            return Result;
        }

        /// <inheritdoc/>
        internal override ValidationResult Validate(params object[] objs) {
            Reset();
            return base.Validate(objs);
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="obj1">The first argument given to <see cref="Validate"/></param>
        /// <param name="obj2">The second argument given to <see cref="Validate"/></param>
        /// <param name="obj3">The third argument given to <see cref="Validate"/></param>
        protected abstract void DoValidate(T1 obj1, T2 obj2, T3 obj3);
    }
}