namespace FlexValidator {
    public abstract class SimpleValidator<T> : Validator, IValidator<T> {
        /// <inheritdoc/>
        public ValidationResult Validate(T obj) {
            Reset();
            DoValidate(obj);
            return Result;
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="model">The object given with <see cref="Validate"/></param>
        protected abstract void DoValidate(T model);
    }

    public abstract class SimpleValidator<T1, T2> : Validator, IValidator<T1, T2> {
        /// <inheritdoc/>
        public ValidationResult Validate(T1 obj1, T2 obj2) {
            Reset();
            DoValidate(obj1, obj2);
            return Result;
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="obj1">The first argument given to <see cref="Validate"/></param>
        /// <param name="obj2">The second argument given to <see cref="Validate"/></param>
        protected abstract void DoValidate(T1 obj1, T2 obj2);
    }

    public abstract class SimpleValidator<T1, T2, T3> : Validator, IValidator<T1, T2, T3> {
        /// <inheritdoc/>
        public ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3) {
            Reset();
            DoValidate(obj1, obj2, obj3);
            return Result;
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

    public abstract class SimpleValidator<T1, T2, T3, T4> : Validator, IValidator<T1, T2, T3, T4> {
        /// <inheritdoc />
        public ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3, T4 obj4) {
            Reset();
            DoValidate(obj1, obj2, obj3, obj4);
            return Result;
        }

        /// <summary>
        /// Implement your <see cref="Validator.Start"/>/<see cref="Validator.Pass"/>/<see cref="Validator.Fail"/>/
        /// <see cref="Validator.Complete"/> logic here. Is called when <see cref="Validate"/> is called.
        /// </summary>
        /// <param name="obj1">The first argument given to <see cref="Validate"/></param>
        /// <param name="obj2">The second argument given to <see cref="Validate"/></param>
        /// <param name="obj3">The third argument given to <see cref="Validate"/></param>
        /// <param name="obj4">The fourth argument given to <see cref="Validate"/></param>
        protected abstract void DoValidate(T1 obj1, T2 obj2, T3 obj3, T4 obj4);
    }
}