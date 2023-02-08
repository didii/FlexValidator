using System;
using System.Linq;
using FlexValidator.Exceptions;
using FlexValidator.Tests.Models;
using FlexValidator.Tests.Validators;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class ValidatorTests {
        private TestSimpleValidator<SomeModel> _sut;
        private SomeModel _model = new SomeModel();
        private string _id1 = "de2319c3-5568-467b-b370-acb486a553f6";

        private void TestResult(bool shouldPass, Action<SomeModel> validateFunc) {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = validateFunc
            };

            //Act
            var result = _sut.Validate(_model);

            //Assert
            if (shouldPass) {
                Assert.IsTrue(result.Passes.Count() == 1);
                Assert.IsTrue(result.Passes.First().Id == _id1);
                Assert.IsFalse(result.Fails.Any());
            } else {
                Assert.IsTrue(result.Fails.Count() == 1);
                Assert.IsTrue(result.Fails.First().Id == _id1);
                Assert.IsFalse(result.Passes.Any());
            }
        }

        private void TestThrow<TException>(Action<SomeModel> validateFunc) where TException : Exception {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = validateFunc
            };

            //Act
            TestDelegate test = () => _sut.Validate(_model);

            //Assert
            Assert.Throws<TException>(test);
        }

        [Test]
        public void Validate_ShouldPassModel() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = x => Assert.AreSame(_model, x)
            };

            //Act
            _sut.Validate(_model);

            //Assert
            //act should not throw
        }

        [Test]
        public void Pass_ShouldAddTheTestToPasses() {
            TestResult(true, x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Pass();
            });
        }

        [Test]
        public void Fail_ShouldAddTheTestToFails() {
            TestResult(false, x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Fail();
            });
        }

        [Test]
        public void AssumePass_ShouldAddTestToPasses() {
            TestResult(true, x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Complete(Assume.Pass);
            });
        }

        [Test]
        public void AssumeFail_ShouldAddTestToFails() {
            TestResult(false, x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Complete(Assume.Fail);
            });
        }

        [Test]
        public void Pass_BeforeAssumeFail_ShouldAddTestToPasses() {
            TestResult(true, x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Pass();
                _sut.Complete(Assume.Fail);
            });
        }

        [Test]
        public void Fail_BeforeAssumePass_ShouldAddTestToPasses() {
            TestResult(false, x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Fail();
                _sut.Complete(Assume.Pass);
            });
        }

        [Test]
        public void CompleteValidation_ShouldThrow_WhithoutPassFailOrAssume() {
            //Arrange
            TestThrow<InvalidValidatorStateException>(x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Complete();
            });
        }

        [Test]
        public void StartValidation_AfterStartValidation_ShouldThrow() {
            TestThrow<InvalidValidatorStateException>(x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Start(new ValidationInfoBase(_id1));
            });
        }

        [Test]
        public void PassWithoutStart_ShouldThrow() {
            TestThrow<InvalidValidatorStateException>(x => _sut.Pass());
        }

        [Test]
        public void FailWithoutStart_ShouldThrow() {
            TestThrow<InvalidValidatorStateException>(x => _sut.Fail());
        }

        [Test]
        public void PassAfterCompletion_ShouldThrow() {
            TestThrow<InvalidValidatorStateException>(x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Pass();
                _sut.Pass();
            });
        }

        [Test]
        public void FailAfterCompletion_ShouldThrow() {
            TestThrow<InvalidValidatorStateException>(x => {
                _sut.Start(new ValidationInfoBase(_id1));
                _sut.Pass();
                _sut.Fail();
            });
        }

        [Test]
        public void Passed_WithoutGuidInResult_ShouldThrow() {
            TestThrow<ValidationNotFoundException>(x => _sut.Passed(_id1.ToString()));
        }

        [Test]
        public void Failed_WithoutGuidInResult_ShouldThrow() {
            TestThrow<ValidationNotFoundException>(x => _sut.Failed(_id1.ToString()));
        }

        [Test]
        public void Passed_WithGuidInPassed_ShouldReturnTrue() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _sut.Result.AddPass(new ValidationInfoBase(_id1.ToString()));
                    Assert.IsTrue(_sut.Passed(_id1));
                }
            };

            //Act + Assert
            _sut.Validate(_model);
        }

        [Test]
        public void Passed_WithGuidInFailed_ShouldReturnFalse() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _sut.Result.AddFail(new ValidationInfoBase(_id1.ToString()));
                    Assert.IsFalse(_sut.Passed(_id1));
                }
            };

            //Act + Assert
            _sut.Validate(_model);
        }

        [Test]
        public void Failed_WithGuidInPassed_ShouldReturnFalse() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _sut.Result.AddPass(new ValidationInfoBase(_id1.ToString()));
                    Assert.IsFalse(_sut.Failed(_id1));
                }
            };

            //Act + Assert
            _sut.Validate(_model);
        }

        [Test]
        public void Failed_WithGuidInFailed_ShouldReturnTrue() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _sut.Result.AddFail(new ValidationInfoBase(_id1.ToString()));
                    Assert.IsTrue(_sut.Failed(_id1));
                }
            };

            //Act + Assert
            _sut.Validate(_model);
        }
    }
}