using FlexValidator.Example.App.Models;
using FlexValidator.Example.App.Tests.Helpers;
using FlexValidator.Example.App.Validators;
using NUnit.Framework;

namespace FlexValidator.Example.App.Tests {
    public class BaseModelValidatorTests {
        private BaseModelValidator _sut = new BaseModelValidator();

        [TestCase(ValidationType.Fail, -5L)]
        [TestCase(ValidationType.Pass, 0L)]
        [TestCase(ValidationType.Pass, 5L)]
        public void Validate_IdCannotBeNegative(ValidationType type, long id) {
            //Arrange
            var model = new SomeModel() { Id = id };

            //Act
            var result = _sut.Validate(model);

            //Assert
            result.Should(type, "20dedb00-4bbd-4ed0-8dc6-1b975e5cafb5");
        }

        [TestCase(ValidationType.Fail, 0)]
        [TestCase(ValidationType.Pass, -5L)]
        [TestCase(ValidationType.Pass, 5L)]
        public void Validate_IdCannotBeZero(ValidationType type, long id) {
            //Arrange
            var model = new SomeModel() { Id = id };

            //Act
            var result = _sut.Validate(model);

            //Assert
            result.Should(type, "94558b8e-5b5b-433c-bf9f-2c398727baf4");
        }
    }
}