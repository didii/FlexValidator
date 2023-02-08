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
        private string _id = "12b696db-1612-4390-805f-e8972d12a12a";

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
        public void RunValidator_ShouldAddPasses() {
            //Arrange
            _validator1 = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _validator1.Start(new ValidationInfoBase(_id));
                    _validator1.Pass();
                }
            };
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => _sut.RunValidator(_validator1, m.Some)
            };

            //Act
            var result = _sut.Validate(_model);

            //Assert
            Assert.IsTrue(result.Check(_id));
        }

        [Test]
        public void RunValidator_ShouldAddFails() {
            //Arrange
            _validator1 = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => {
                    _validator1.Start(new ValidationInfoBase(_id));
                    _validator1.Fail();
                }
            };
            _sut = new TestSimpleValidator<SomeModel>() {
                ValidateFunc = m => _sut.RunValidator(_validator1, m.Some)
            };

            //Act
            var result = _sut.Validate(_model);

            //Assert
            Assert.IsFalse(result.Check(_id));
        }
    }
}
