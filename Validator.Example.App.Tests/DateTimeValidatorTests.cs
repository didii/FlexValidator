using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Validator.Example.App.Tests.Helpers;
using Validator.Example.App.Validators;

namespace Validator.Example.App.Tests {
    public class DateTimeValidatorTests {
        private DateTimeValidator _sut = new DateTimeValidator();

        [TestCase(ValidationType.Fail, 1800, 1, 1)]
        [TestCase(ValidationType.Fail, 1999, 12, 31)]
        [TestCase(ValidationType.Pass, 2000, 1, 1)]
        [TestCase(ValidationType.Pass, 2005, 1, 1)]
        [TestCase(ValidationType.Pass, 2020, 1, 1)]
        public void Validate_DateMustBe2000OrLater(ValidationType type, int year, int month, int day) {
            //Arrange
            var model = new DateTime(year, month, day);

            //Act
            var result = _sut.Validate(model);

            //Assert
            result.Should(type, "8d7af18b-1a13-4b7e-963d-68c6cd6636ca");
        }
    }
}
