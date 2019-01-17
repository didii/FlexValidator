using System;
using System.Collections.Generic;
using System.Text;
using FlexValidator.Tests.Models;
using FlexValidator.Tests.Validators;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class RunValidatorTests {
        private TestSimpleValidator<SomeModel> _sut;
        private TestSimpleValidator<SomeModel> _validator1 = new TestSimpleValidator<SomeModel>();
        private TestSimpleValidator<SomeModel, SomeModel> _validator2 = new TestSimpleValidator<SomeModel, SomeModel>();
        private TestSimpleValidator<SomeModel, SomeModel, SomeModel> _validator3 = new TestSimpleValidator<SomeModel, SomeModel, SomeModel>();
        private TestSimpleValidator<SomeModel, SomeModel, SomeModel, SomeModel> _validator4 = new TestSimpleValidator<SomeModel, SomeModel, SomeModel, SomeModel>();
        private Guid guid = new Guid("12b696db-1612-4390-805f-e8972d12a12a");

        private SomeModel _model = new SomeModel() {
            Some = new SomeModel() {
                Some = new SomeModel() {
                    Some = new SomeModel() {
                        Some = new SomeModel()
                    }
                }
            }
        };

        [Test]
        public void RunValidator1_ShouldNotThrow() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => { _sut.RunValidator(_validator1, m.Some); }
            };

            //Act
            _sut.Validate(_model);
        }

        [Test]
        public void RunValidator2_ShouldNotThrow() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => _sut.RunValidator(_validator2, m.Some, m.Some.Some)
            };

            //Act
            _sut.Validate(_model);
        }

        [Test]
        public void RunValidator3_ShouldNotThrow() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => _sut.RunValidator(_validator3, m.Some, m.Some.Some, m.Some.Some.Some)
            };

            //Act
            _sut.Validate(_model);
        }

        [Test]
        public void RunValidator4_ShouldNotThrow() {
            //Arrange
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => _sut.RunValidator(_validator4, m.Some, m.Some.Some, m.Some.Some.Some, m.Some.Some.Some.Some)
            };

            //Act
            _sut.Validate(_model);
        }

        [Test]
        public void RunValidator_ShouldAddPasses() {
            //Arrange
            _validator1 = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _validator1.Start(new ValidationInfoBase(guid));
                    _validator1.Pass();
                }
            };
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => _sut.RunValidator(_validator1, m.Some)
            };

            //Act
            var result = _sut.Validate(_model);

            //Assert
            Assert.IsTrue(result.Check(guid));
        }

        [Test]
        public void RunValidator_ShouldAddFails() {
            //Arrange
            _validator1 = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _validator1.Start(new ValidationInfoBase(guid));
                    _validator1.Fail();
                }
            };
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => _sut.RunValidator(_validator1, m.Some)
            };

            //Act
            var result = _sut.Validate(_model);

            //Assert
            Assert.IsFalse(result.Check(guid));
        }
    }
}
