﻿using System;
using System.Linq;
using FlexValidator.Tests.Models;
using FlexValidator.Tests.Validators;
using NUnit.Framework;

namespace FlexValidator.Tests {

    public class EmptyModel2ValidatorTests {
        private TestSimpleValidator<(SomeModel, SomeModel)> _sut;
        private SomeModel _model1 = new();
        private SomeModel _model2 = new();
        private string _id = "de2319c3-5568-467b-b370-acb486a553f6";

        [Test]
        public void Validate_ShouldPassModels() {
            //Assert
            _sut = new TestSimpleValidator<(SomeModel, SomeModel)>() {
                ValidateFunc = (models) => {
                    Assert.AreSame(_model1, models.Item1);
                    Assert.AreSame(_model2, models.Item2);
                }
            };

            //Act
            _sut.Validate((_model1, _model2));

            //Assert
            //act does not throw
        }

        [Test]
        public void Pass_ShouldAddTestToPasses() {
            //Assert
            _sut = new TestSimpleValidator<(SomeModel, SomeModel)>() {
                ValidateFunc = (models) => {
                    _sut.Start(new ValidationInfoBase(_id));
                    _sut.Pass();
                }
            };

            //Act
            var result = _sut.Validate((_model1, _model2));

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Id == _id);
            Assert.IsFalse(result.Fails.Any());
        }
    }
}