using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Validator.Example.App.Models;
using Validator.Example.App.Tests.Helpers;
using Validator.Example.App.Validators;

namespace Validator.Example.App.Tests {
    public class DoubleLeftRightValidatorTests {
        private DoubleLeftRightValidator _sut = new DoubleLeftRightValidator();

        [TestCase(ValidationType.Pass, null, null)]
        [TestCase(ValidationType.Pass, "", "")]
        [TestCase(ValidationType.Pass, "name", "name")]
        [TestCase(ValidationType.Pass, "blahblah", "blahblah")]
        [TestCase(ValidationType.Fail, null, "")]
        [TestCase(ValidationType.Fail, "", null)]
        [TestCase(ValidationType.Fail, "nameLeft", "nameRight")]
        public void Validate_TypeBothIn_RequiresSameName(ValidationType type, string leftName, string rightName) {
            //Arrange
            var left = new DoubleModel() {
                Name = leftName,
                Type = DoubleModelType.In
            };
            var right = new DoubleModel() {
                Name = rightName,
                Type = DoubleModelType.In
            };

            //Act
            var result = _sut.ValidateSection(DoubleLeftRightValidator.InInSection, left, right);

            //Assert
            result.Should(type, "e229be44-ec96-436a-916c-dbbfabe7a7f3");
        }

        [TestCase(ValidationType.Pass, null)]
        [TestCase(ValidationType.Fail, "")]
        [TestCase(ValidationType.Fail, "hoho")]
        public void Validate_TypeLeftInRightOut_RequireLeftToBeNull(ValidationType type, string leftName) {
            //Arrange
            var left = new DoubleModel() {
                Name = leftName,
                Type = DoubleModelType.In
            };
            var right = new DoubleModel() {
                Name = null,
                Type = DoubleModelType.Out
            };

            //Act
            var result = _sut.ValidateSection(DoubleLeftRightValidator.InOutSection, left, right);

            //Assert
            result.Should(type, "66a73e51-1c21-4ef7-958c-ae45acfd9f45");
        }

        public void Validate_TypeLeftInRightOut_RequireRightToHaveAValue(ValidationType type, string rightName) {
            //Arrange
            var left = new DoubleModel() {
                Name = null,
                Type = DoubleModelType.In
            };
            var right = new DoubleModel() {
                Name = rightName,
                Type = DoubleModelType.Out
            };

            //Act
            var result = _sut.ValidateSection(DoubleLeftRightValidator.InOutSection, left, right);

            //Assert
            result.Should(type, "670ac060-d954-4518-8d03-155130391b92");
        }
    }
}
