using System;
using System.Linq;
using NUnit.Framework;

namespace FlexValidator.Example.App.Tests.Helpers {
    public static class IValidationResultExtensions {

        public static void ShouldPass(this IValidationResult source, string id) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var pass = source.Passes.FirstOrDefault(x => x.Id == id);
            if (pass != null)
                return;

            var fail = source.Fails.FirstOrDefault(x => x.Id == id);
            if (fail != null)
                Assert.Fail($"Validation {id} failed when it should have passed: {fail.Message}");

            Assert.Fail($"Validation {id} did not run...");
        }

        public static void ShouldFail(this IValidationResult source, string id) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var fail = source.Fails.FirstOrDefault(x => x.Id == id);
            if (fail != null)
                return;

            var pass = source.Passes.FirstOrDefault(x => x.Id == id);
            if (pass != null)
                Assert.Fail($"Validation {id} passed when it should have failed: {pass.Message}");

            Assert.Fail($"Validation {id} did not run...");
        }

        public static void Should(this IValidationResult source, ValidationType type, string id) {
            switch (type) {
                case ValidationType.Pass:
                    ShouldPass(source, id);
                    break;
                case ValidationType.Fail:
                    ShouldFail(source, id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}