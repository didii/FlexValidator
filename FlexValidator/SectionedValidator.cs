using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using FlexValidator.Exceptions;

namespace FlexValidator {
    public abstract class SectionedValidator : Validator {
        private readonly IDictionary<string, Action<object[]>> _sections = new Dictionary<string, Action<object[]>>();
        private readonly IDictionary<string, Func<object[], Task>> _asyncSections = new Dictionary<string, Func<object[], Task>>();

        internal IValidationResult Validate(params object[] models) {
            Reset();
            RunSections(models);
            RunAsyncSections(models);
            return Result;
        }

        internal async Task<IValidationResult> ValidateAsync(params object[] models) {
            var task = RunAsyncSectionsAsync(models);
            RunSections(models);
            await task;
            return Result;
        }

        internal IValidationResult ValidateSection(string name, params object[] models) {
            Reset();
            if (RunSection(name, models))
                return Result;
            if (RunAsyncSection(name, models))
                return Result;
            throw new ValidatorSectionNotFoundException(name);
        }

        internal async Task<IValidationResult> ValidateSectionAsync(string name, params object[] models) {
            Reset();
            if (await RunAsyncSectionAsync(name, models))
                return Result;
            if (RunSection(name, models))
                return Result;
            throw new ValidatorSectionNotFoundException(name);
        }

        internal void Section(string name, Action<object[]> section) {
            _sections.Add(name, section);
        }

        internal void AsyncSection(string name, Func<object[], Task> asyncSection) {
            _asyncSections.Add(name, asyncSection);
        }

        private void RunSections(params object[] models) {
            foreach (var section in _sections) {
                var action = section.Value;
                action(models);
            }
        }

        private void RunAsyncSections(params object[] models) {
            foreach (var asynSection in _asyncSections) {
                var action = asynSection.Value;
                action(models).GetAwaiter().GetResult();
            }
        }

        private Task RunAsyncSectionsAsync(params object[] models) {
            return Task.WhenAll(_asyncSections.Select(x => x.Value(models)));
        }

        private bool RunSection(string name, params object[] models) {
            if (!_sections.ContainsKey(name))
                return false;

            var action = _sections[name];
            action(models);
            return true;
        }

        private bool RunAsyncSection(string name, params object[] models) {
            if (!_asyncSections.ContainsKey(name))
                return false;

            var action = _asyncSections[name];
            action(models).GetAwaiter().GetResult();
            return true;
        }

        private async Task<bool> RunAsyncSectionAsync(string name, params object[] models) {
            if (!_asyncSections.ContainsKey(name))
                return false;

            var action = _asyncSections[name];
            await action(models);
            return true;
        }
    }
}