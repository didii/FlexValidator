using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlexValidator.Tests.Models;
using FlexValidator.Tests.Validators;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class AsyncSimpleTests {
        private TestSimpleValidator<SomeModel> _sut;
        private SomeModel _model = new SomeModel();

        [Test]
        public async Task ValidateAsync_ShouldNotThrow() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>();

            //Act
            await _sut.ValidateAsync(_model);
        }

        [Test]
        public async Task ValdidateAsync_ShouldReturnSomething() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>();

            //Act
            var result = await _sut.ValidateAsync(_model);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task ValidateAsync_ShouldInvokeDoValidateAsync() {
            //Arrange
            var isRun = false;
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateAsyncFunc = async m => isRun = true
            };

            //Act
            await _sut.ValidateAsync(_model);

            //Assert
            Assert.IsTrue(isRun);
        }

        [Test]
        public async Task ValidateAsync_ShouldInvokeDoValidate() {
            //Arrange
            var isRun = false;
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => isRun = true
            };

            //Act
            await _sut.ValidateAsync(_model);

            //Assert
            Assert.IsTrue(isRun);
        }

        [Test]
        public void Validate_ShouldInvokeDoValidate() {
            //Arrange
            var isRun = false;
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => isRun = true
            };

            //Act
            _sut.Validate(_model);

            //Assert
            Assert.IsTrue(isRun);
        }

        [Test]
        public void Validate_ShouldInvokeDoValidateAsync() {
            //Arrange
            var isRun = false;
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateAsyncFunc = async m => isRun = true
            };

            //Act
            _sut.Validate(_model);

            //Assert
            Assert.IsTrue(isRun);
        }
    }
}
