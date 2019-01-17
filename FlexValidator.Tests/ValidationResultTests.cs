using System;
using System.Linq;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class ValidationResultTests {
        private ValidationResult _sut;
        private const string sGuid = "277e2b45-39d9-4ca6-aa14-1f99659bf6c5";
        private const string sGuid2 = "48fa2523-a25f-460a-9a90-0ad3d624981f";
        private Guid guid = new Guid(sGuid);
        private Guid guid2 = new Guid(sGuid2);

        [Test]
        public void Ctor_ShouldAddFailsToFails() {
            //Arrange + Act
            _sut = new ValidationResult(new[] { new ValidationInfoBase(guid) });

            //Assert
            Assert.NotNull(_sut.Fails.FirstOrDefault(x => x.Guid == guid));
        }

        [Test]
        public void Ctor_ShouldAddPassesToPasses() {
            //Arrange + Act
            _sut = new ValidationResult(null, new[] { new ValidationInfoBase(guid) });

            //Assert
            Assert.NotNull(_sut.Passes.FirstOrDefault(x => x.Guid == guid));
        }

        [TestCase(true, null, null)]
        [TestCase(false, new[] { sGuid }, null)]
        [TestCase(false, new[] { sGuid, sGuid2 }, null)]
        [TestCase(true, null, new[] { sGuid })]
        [TestCase(true, null, new[] { sGuid, sGuid2 })]
        [TestCase(false, new[] { sGuid }, new[] { sGuid2 })]
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
            var info = new ValidationInfoBase(guid);

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
            var info = new ValidationInfoBase(guid);

            //Act
            _sut.AddFail(info);

            //Assert
            Assert.IsTrue(_sut.Fails.Contains(info));
            Assert.IsFalse(_sut.Passes.Contains(info));
        }
    }
}