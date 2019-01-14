using System;
using System.Collections.Generic;
using System.Resources;
using Validator.Exceptions;

namespace Validator {
    public abstract class SectionedValidator<T> : Validator, ISectionedValidator<T> {
        private readonly IDictionary<string, Action<T>> _sections = new Dictionary<string, Action<T>>();

        /// <inheritdoc/>
        public ValidationResult Validate(T obj) {
            Reset();
            foreach (var section in _sections) {
                var action = section.Value;
                action(obj);
            }
            return Result;
        }

        /// <inheritdoc/>
        public ValidationResult ValidateSection(string sectionName, T obj) {
            Reset();
            var action = _sections[sectionName];
            action(obj);
            return Result;
        }

        protected internal void Section(string sectionName, Action<T> validateFunc) {
            _sections.Add(sectionName, validateFunc);
        }
    }

    public abstract class SectionedValidator<T1, T2> : Validator, ISectionedValidator<T1, T2> {
        private IDictionary<string, Action<T1, T2>> _sections = new Dictionary<string, Action<T1, T2>>();

        /// <inheritdoc />
        public ValidationResult Validate(T1 obj1, T2 obj2) {
            Reset();
            foreach (var section in _sections) {
                var action = section.Value;
                action(obj1, obj2);
            }
            return Result;
        }

        /// <inheritdoc />
        public ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2) {
            Reset();
            Action<T1, T2> action;
            try {
                action = _sections[sectionName];
            }
            catch (KeyNotFoundException e) {
                throw new ValidatorSectionNotFoundException(sectionName, e);
            }
            action(obj1, obj2);
            return Result;
        }

        protected internal void Section(string sectionName, Action<T1, T2> validateFunc) {
            _sections.Add(sectionName, validateFunc);
        }
    }

    public abstract class SectionedValidator<T1, T2, T3> : Validator, ISectionedValidator<T1, T2, T3> {
        private readonly IDictionary<string, Action<T1, T2, T3>> _sections = new Dictionary<string, Action<T1, T2, T3>>();

        /// <inheritdoc />
        public ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3) {
            Reset();
            foreach (var section in _sections) {
                var action = section.Value;
                action(obj1, obj2, obj3);
            }
            return Result;
        }

        /// <inheritdoc />
        public ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2, T3 obj3) {
            Reset();
            var action = _sections[sectionName];
            action(obj1, obj2, obj3);
            return Result;
        }

        protected internal void Section(string sectionName, Action<T1, T2, T3> validateFunc) {
            _sections.Add(sectionName, validateFunc);
        }
    }

    public abstract class SectionedValidator<T1, T2, T3, T4> : Validator, ISectionedValidator<T1, T2, T3, T4> {
        private readonly IDictionary<string, Action<T1, T2, T3, T4>> _sections = new Dictionary<string, Action<T1, T2, T3, T4>>();

        /// <inheritdoc />
        public ValidationResult Validate(T1 obj1, T2 obj2, T3 obj3, T4 obj4) {
            Reset();
            foreach (var section in _sections) {
                var action = section.Value;
                action(obj1, obj2, obj3, obj4);
            }
            return Result;
        }

        /// <inheritdoc />
        public ValidationResult ValidateSection(string sectionName, T1 obj1, T2 obj2, T3 obj3, T4 obj4) {
            Reset();
            var action = _sections[sectionName];
            action(obj1, obj2, obj3, obj4);
            return Result;
        }

        protected internal void Section(string sectionName, Action<T1, T2, T3, T4> validateFunc) {
            _sections.Add(sectionName, validateFunc);
        }
    }
}