using System;
using System.Linq;
using NUnit.Framework;

namespace FlexValidator.Example.App.Tests.Helpers {
    public static class ValidationResultExtensions {
        public static void ShouldPass(this ValidationResult source, string guid) {
            ShouldPass(source, new Guid(guid));
        }

        public static void ShouldPass(this ValidationResult source, Guid guid) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var pass = source.Passes.FirstOrDefault(x => x.Guid == guid);
            if (pass != null)
                return;

            var fail = source.Fails.FirstOrDefault(x => x.Guid == guid);
            if (fail != null)
                Assert.Fail($"Validation {guid} failed when it should have passed: {fail.Message}");

            Assert.Fail($"Validation {guid} did not run...");
        }

        public static void ShouldFail(this ValidationResult source, string guid) {
            ShouldFail(source, new Guid(guid));
        }

        public static void ShouldFail(this ValidationResult source, Guid guid) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var fail = source.Fails.FirstOrDefault(x => x.Guid == guid);
            if (fail != null)
                return;

            var pass = source.Passes.FirstOrDefault(x => x.Guid == guid);
            if (pass != null)
                Assert.Fail($"Validation {guid} passed when it should have failed: {pass.Message}");

            Assert.Fail($"Validation {guid} did not run...");
        }

        public static void Should(this ValidationResult source, ValidationType type, string guid) {
            Should(source, type, new Guid(guid));
        }

        public static void Should(this ValidationResult source, ValidationType type, Guid guid) {
            switch (type) {
                case ValidationType.Pass:
                    ShouldPass(source, guid);
                    break;
                case ValidationType.Fail:
                    ShouldFail(source, guid);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}