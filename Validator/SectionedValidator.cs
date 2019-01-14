using System;
using System.Collections.Generic;
using Validator.Exceptions;

namespace Validator {
    public abstract class SectionedValidator<T> : Validator<T>, ISectionedValidator<T> {
        private readonly IDictionary<string, BaseValidator> _sections = new Dictionary<string, BaseValidator>();

        /// <inheritdoc/>
        public override ValidationResult Validate(T obj) {
            Reset();
            var result = new ValidationResult();
            foreach (var section in _sections) {
                var validator = section.Value;
                var subResult = validator.Validate(obj);
                result.AddRange(subResult);
            }
            return result;
        }

        /// <inheritdoc/>
        public ValidationResult ValidateSection(string sectionName, T obj) {
            Reset();
            var validator = _sections[sectionName];
            return validator.Validate(obj);
        }

        protected internal void Section(string sectionName, Action<T> validateFunc) {
            _sections.Add(sectionName, new BaseValidator() {
                ValidatorFunc = objs => {
                    Reset();
                    validateFunc((T)objs[0]);
                    return Result;
                }
            });
        }
    }

    public abstract class SectionedValidator<T1, T2> : Validator<T1, T2>, ISectionedValidator<T1, T2> {
        private IDictionary<string, BaseValidator> _sections = new Dictionary<string, BaseValidator>();

        /// <inheritdoc />
        public override ValidationResult Validate(T1 obj1, T2 obj2) {
            Reset();
            var result = new ValidationResult();
            foreach (var section in _sections) {
                var validator = section.Value;
                var subResult = validator.Validate(obj1, obj2);
                result.AddRange(subResult);
            }
            return result;
        }

        /// <inheritdoc />
        public ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2) {
            Reset();
            BaseValidator validator;
            try {
                validator = _sections[sectionName];
            }
            catch (KeyNotFoundException e) {
                throw new ValidatorSectionNotFoundException(sectionName, e);
            }
            return validator.Validate(obj1, obj2);
        }

        protected internal void Section(string sectionName, Action<T1, T2> validateFunc) {
            _sections.Add(sectionName, new BaseValidator() {
                ValidatorFunc = objs => {
                    Reset();
                    validateFunc((T1)objs[0], (T2)objs[1]);
                    return Result;
                }
            });
        }
    }
}