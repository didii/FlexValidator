using System;
using System.Linq;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class ValidationResultTests {
        private ValidationResult _sut;
        private const string Id1 = "277e2b45-39d9-4ca6-aa14-1f99659bf6c5";
        private const string Id2 = "48fa2523-a25f-460a-9a90-0ad3d624981f";

        [Test]
        public void Ctor_ShouldAddFailsToFails() {
            //Arrange + Act
            _sut = new ValidationResult(new[] { new ValidationInfoBase(Id1) });

            //Assert
            Assert.NotNull(_sut.Fails.FirstOrDefault(x => x.Id == Id1));
        }

        [Test]
        public void Ctor_ShouldAddPassesToPasses() {
            //Arrange + Act
            _sut = new ValidationResult(null, new[] { new ValidationInfoBase(Id1) });

            //Assert
            Assert.NotNull(_sut.Passes.FirstOrDefault(x => x.Id == Id1));
        }

        [TestCase(true, null, null)]
        [TestCase(false, new[] { Id1 }, null)]
        [TestCase(false, new[] { Id1, Id2 }, null)]
        [TestCase(true, null, new[] { Id1 })]
        [TestCase(true, null, new[] { Id1, Id2 })]
        [TestCase(false, new[] { Id1 }, new[] { Id2 })]
        public void IsValid_WhenNoFails_ShouldReturnTrue(bool expected, string[] fails, string[] passes) {
            //Arrange
            _sut = new ValidationResult(fails?.Select(x => new ValidationInfoBase(x)), passes?.Select(x => new ValidationInfoBase(x)));

            //Assert
            Assert.AreEqual(expected, _sut.IsValid);
        }

        [Test]
        public void AddPass_ShouldAddToPasses() {
            //Arrange
            _sut = new ValidationResult();
            var info = new ValidationInfoBase(Id1);

            //Act
            _sut.AddPass(info);

            //Assert
            Assert.IsTrue(_sut.Passes.Contains(info));
            Assert.IsFalse(_sut.Fails.Contains(info));
        }

        [Test]
        public void AddFail_ShouldAddToFails() {
            //Arrange
            _sut = new ValidationResult();
            var info = new ValidationInfoBase(Id1);

            //Act
            _sut.AddFail(info);

            //Assert
            Assert.IsTrue(_sut.Fails.Contains(info));
            Assert.IsFalse(_sut.Passes.Contains(info));
        }
    }
}