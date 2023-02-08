using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexValidator.Exceptions;

namespace FlexValidator {
    /// <summary>
    /// Do not use. It's the base class of a sectioned validator that holds all logic and has no visible members outside of its assembly.
    /// </summary>
    public abstract class SectionedValidator<T> : Validator, IValidator<T> {
        private readonly IDictionary<string, Action<T>> _sections = new Dictionary<string, Action<T>>();
        private readonly IDictionary<string, Func<T, Task>> _asyncSections = new Dictionary<string, Func<T, Task>>();

        public IValidationResult Validate(T model) {
            Reset();
            RunSections(model);
            RunAsyncSections(model);
            return Result;
        }

        public async Task<IValidationResult> ValidateAsync(T model) {
            Reset();
            var task = RunAsyncSectionsAsync(model);
            RunSections(model);
            await task;
            return Result;
        }

        public IValidationResult ValidateSection(string name, T model) {
            Reset();
            if (RunSection(name, model))
                return Result;
            if (RunAsyncSection(name, model))
                return Result;
            throw new ValidatorSectionNotFoundException(name);
        }

        public async Task<IValidationResult> ValidateSectionAsync(string name, T model) {
            Reset();
            if (await RunAsyncSectionAsync(name, model))
                return Result;
            if (RunSection(name, model))
                return Result;
            throw new ValidatorSectionNotFoundException(name);
        }

        public void Section(string name, Action<T> section) {
            _sections.Add(name, section);
        }

        public void AsyncSection(string name, Func<T, Task> asyncSection) {
            _asyncSections.Add(name, asyncSection);
        }

        private void RunSections(T model) {
            foreach (var section in _sections) {
                var action = section.Value;
                action(model);
            }
        }

        private void RunAsyncSections(T model) {
            foreach (var asynSection in _asyncSections) {
                var action = asynSection.Value;
                action(model).GetAwaiter().GetResult();
            }
        }

        private Task RunAsyncSectionsAsync(T model) {
            return Task.WhenAll(_asyncSections.Select(x => x.Value(model)));
        }

        private bool RunSection(string name, T model) {
            if (!_sections.ContainsKey(name))
                return false;

            var action = _sections[name];
            action(model);
            return true;
        }

        private bool RunAsyncSection(string name, T model) {
            if (!_asyncSections.ContainsKey(name))
                return false;

            var action = _asyncSections[name];
            action(model).GetAwaiter().GetResult();
            return true;
        }

        private async Task<bool> RunAsyncSectionAsync(string name, T model) {
            if (!_asyncSections.ContainsKey(name))
                return false;

            var action = _asyncSections[name];
            await action(model);
            return true;
        }
    }
}