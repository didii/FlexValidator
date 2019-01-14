using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validator.Tests.Models;
using Validator.Tests.Validators;

namespace Validator.Tests {
    [TestClass]
    public class EmptyModel_ValidatorTests {
        private TestSimpleValidator<EmptyModel> _sut;
        private EmptyModel _model = new EmptyModel();
        private Guid guid = new Guid("de2319c3-5568-467b-b370-acb486a553f6");
        private Guid guid2 = new Guid("4ac89b89-52ea-4b50-a283-ea79f29c7122");

        private void TestResult(bool shouldPass, Action<EmptyModel> validateFunc) {
            //Arrange
            _sut = new TestSimpleValidator<EmptyModel>() {
                ValidateFunc = validateFunc
            };

            //Act
            var result = _sut.Validate(_model);

            //Assert
            if (shouldPass) {
                Assert.IsTrue(result.Passes.Count() == 1);
                Assert.IsTrue(result.Passes.First().Guid == guid);
                Assert.IsFalse(result.Fails.Any());
            } else {
                Assert.IsTrue(result.Fails.Count() == 1);
                Assert.IsTrue(result.Fails.First().Guid == guid);
                Assert.IsFalse(result.Passes.Any());
            }
        }

        private void TestThrow(Action<EmptyModel> validateFunc) {
            //Arrange
            _sut = new TestSimpleValidator<EmptyModel>() {
                ValidateFunc = validateFunc
            };

            //Act
            Action test = () => _sut.Validate(_model);

            //Assert
            Assert.ThrowsException<InvalidValidatorStateException>(test);
        }

        [TestMethod]
        public void Validate_ShouldPassModel() {
            //Arrange
            _sut = new TestSimpleValidator<EmptyModel>() {
                ValidateFunc = x => Assert.AreSame(_model, x)
            };

            //Act
            _sut.Validate(_model);

            //Assert
            //act should not throw
        }

        [TestMethod]
        public void Pass_ShouldAddTheTestToPasses() {
            TestResult(true, x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Pass();
            });
        }

        [TestMethod]
        public void Fail_ShouldAddTheTestToFails() {
            TestResult(false, x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Fail();
            });
        }

        [TestMethod]
        public void AssumePass_ShouldAddTestToPasses() {
            TestResult(true, x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Complete(true);
            });
        }

        [TestMethod]
        public void AssumeFail_ShouldAddTestToFails() {
            TestResult(false, x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Complete(false);
            });
        }

        [TestMethod]
        public void Pass_BeforeAssumeFail_ShouldAddTestToPasses() {
            TestResult(true, x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Pass();
                _sut.Complete(false);
            });
        }

        [TestMethod]
        public void Fail_BeforeAssumePass_ShouldAddTestToPasses() {
            TestResult(false, x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Fail();
                _sut.Complete(true);
            });
        }

        [TestMethod]
        public void CompleteValidation_ShouldThrow_WhithoutPassFailOrAssume() {
            //Arrange
            TestThrow(x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Complete();
            });
        }

        [TestMethod]
        public void StartValidation_AfterStartValidation_ShouldThrow() {
            TestThrow(x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Start(new ValidationInfo(guid));

            });
        }

        [TestMethod]
        public void PassWithoutStart_ShouldThrow() {
            //Assert
            TestThrow(x => _sut.Pass());
        }

        [TestMethod]
        public void FailWithoutStart_ShouldThrow() {
            TestThrow(x => _sut.Fail());
        }

        [TestMethod]
        public void PassAfterCompletion_ShouldThrow() {
            TestThrow(x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Pass();
                _sut.Pass();
            });
        }

        [TestMethod]
        public void FailAfterCompletion_ShouldThrow() {
            TestThrow(x => {
                _sut.Start(new ValidationInfo(guid));
                _sut.Pass();
                _sut.Fail();
            });
        }
    }
}