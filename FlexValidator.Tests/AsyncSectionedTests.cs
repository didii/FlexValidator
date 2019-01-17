using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlexValidator.Tests.Models;
using FlexValidator.Tests.Validators;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class AsyncSectionedTests {
        private TestSectionedValidator<SomeModel> _sut;
        private SomeModel _model = new SomeModel();
        private const string Section = "section";

        [Test]
        public async Task ValidateAsync_ShouldNotThrow() {
            //Arrange
            _sut = new TestSectionedValidator<SomeModel>();

            //Act
            await _sut.ValidateAsync(_model);
        }

        [Test]
        public async Task ValdidateAsync_ShouldReturnSomething() {
            //Arrange
            _sut = new TestSectionedValidator<SomeModel>();

            //Act
            var result = await _sut.ValidateAsync(_model);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public void Validate_ShouldInvokeDoValidate() {
            //Arrange
            var isRun = false;
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.Section(Section, m => isRun = true));

            //Act
            _sut.Validate(_model);

            //Assert
            Assert.IsTrue(isRun);
        }

        [Test]
        public void Validate_ShouldInvokeDoValidateAsync() {
            //Arrange
            var isRun = false;
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.AsyncSection(Section, async m => isRun = true));

            //Act
            _sut.Validate(_model);

            //Assert
            Assert.IsTrue(isRun);
        }

        [Test]
        public async Task ValidateAsync_ShouldInvokeDoValidateAsync() {
            //Arrange
            var isRun = false;
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.AsyncSection(Section, async m => isRun = true));

            //Act
            await _sut.ValidateAsync(_model);

            //Assert
            Assert.IsTrue(isRun);
        }

        [Test]
        public async Task ValidateAsync_ShouldInvokeDoValidate() {
            //Arrange
            var isRun = false;
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.Section(Section, m => isRun = true));

            //Act
            await _sut.ValidateAsync(_model);

            //Assert
            Assert.IsTrue(isRun);
        }

        [Test]
        public void ValidateSection_WithSyncSection_ShouldReturnSomething() {
            //Arrange
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.Section(Section, m => {}));

            //Act
            var result = _sut.ValidateSection(Section, _model);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public void ValidateSection_WithAsyncSection_ShouldReturnSomething() {
            //Arrange
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.AsyncSection(Section, async m => {}));

            //Act
            var result = _sut.ValidateSection(Section, _model);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task ValidateSectionAsync_WithSyncSection_ShouldReturnSomething() {
            //Arrange
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.Section(Section, m => {}));

            //Act
            var result = await _sut.ValidateSectionAsync(Section, _model);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task ValidateSectionAsync_WithAsyncSection_ShouldReturnSomething() {
            //Arrange
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(() => _sut.AsyncSection(Section, async m => {}));

            //Act
            var result = await _sut.ValidateSectionAsync(Section, _model);

            //Assert
            Assert.NotNull(result);
        }
    }
}
