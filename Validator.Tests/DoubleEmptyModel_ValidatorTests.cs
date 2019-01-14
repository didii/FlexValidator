using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validator.Tests.Models;
using Validator.Tests.Validators;

namespace Validator.Tests {
    [TestClass]
    public class DoubleEmptyModel_ValidatorTests {
        private TestSimpleValidator<EmptyModel, EmptyModel> _sut;
        private EmptyModel _model1 = new EmptyModel();
        private EmptyModel _model2 = new EmptyModel();
        private Guid guid = new Guid("de2319c3-5568-467b-b370-acb486a553f6");

        [TestMethod]
        public void Validate_ShouldPassModels() {
            //Assert
            _sut = new TestSimpleValidator<EmptyModel, EmptyModel>() {
                ValidateFunc = (x, y) => {
                    Assert.AreSame(_model1, x);
                    Assert.AreSame(_model2, y);
                }
            };

            //Act
            _sut.Validate(_model1, _model2);

            //Assert
            //act does not throw
        }

        [TestMethod]
        public void Pass_ShouldAddTestToPasses() {
            //Assert
            _sut = new TestSimpleValidator<EmptyModel, EmptyModel>() {
                ValidateFunc = (x, y) => {
                    _sut.Start(new ValidationInfoBase(guid));
                    _sut.Pass();
                }
            };

            //Act
            var result = _sut.Validate(_model1, _model2);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }
    }
}