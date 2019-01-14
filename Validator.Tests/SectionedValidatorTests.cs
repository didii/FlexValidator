using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validator.Tests.Models;
using Validator.Tests.Validators;

namespace Validator.Tests {
    [TestClass]
    public class SectionedValidatorTests {
        private TestSectionedValidator<SomeModel> _sut;
        private string section = "section";
        private string section2 = "otherSection";
        private Guid guid = new Guid("de2319c3-5568-467b-b370-acb486a553f6");
        private Guid guid2 = new Guid("4ac89b89-52ea-4b50-a283-ea79f29c7122");

        [TestMethod]
        public void Validate_ShouldTestSection() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfo(guid));
                    _sut.Pass();
                });
            });

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [TestMethod]
        public void Validate_ShouldTestAllSections() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfo(guid));
                    _sut.Pass();
                });
                validator.Section(section2, x => {
                    _sut.Start(new ValidationInfo(guid2));
                    _sut.Fail();
                });
            });

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == guid2);
        }

        [TestMethod]
        public void ValidateSection_ShouldTestOnlySection() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfo(guid));
                    _sut.Pass();
                });
                validator.Section(section2, x => {
                    _sut.Start(new ValidationInfo(guid2));
                    _sut.Fail();
                });
            });

            //Act
            var result = _sut.ValidateSection(section, model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [TestMethod]
        public void ValidateSection2_ShouldTestOnlySection2() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfo(guid));
                    _sut.Pass();
                });
                validator.Section(section2, x => {
                    _sut.Start(new ValidationInfo(guid2));
                    _sut.Fail();
                });
            });

            //Act
            var result = _sut.ValidateSection(section2, model);

            //Assert
            Assert.IsFalse(result.Passes.Any());
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == guid2);
        }

        [TestMethod]
        public void Validate_WithSomeValidator_ShouldRun() {
            //Arrange
            var model = new SomeModel();
            var other = new TestSimpleValidator<SomeModel>();
            other.ValidateFunc = x => {
                other.Start(new ValidationInfo(guid));
                other.Pass();
            };

            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => _sut.Section(section, m => _sut.RunValidator(other, m)));

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [TestMethod]
        public void Validate_WithSubValidator_ShouldRun() {
            //Arrange
            var model = new SomeModel();
            var sub1 = new TestSimpleValidator<SubModel>();
            sub1.ValidateFunc = x => {
                sub1.Start(new ValidationInfo(guid));
                sub1.Pass();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => _sut.Section(section, m => _sut.RunValidator(sub1, m.Sub)));

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [TestMethod]
        public void Validate_WithMultileSubValidators_ShouldRunAllSections() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<DoubleModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfo(guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<DoubleModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfo(guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(section, m => _sut.RunValidator(doubleLeft, m.DoubleLeft));
                _sut.Section(section2, m => _sut.RunValidator(doubleRight, m.DoubleRight));
            });

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == guid2);
        }

        [TestMethod]
        public void ValidateSection_WithMultileSubValidators_ShouldRunSection() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<DoubleModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfo(guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<DoubleModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfo(guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(section, m => _sut.RunValidator(doubleLeft, m.DoubleLeft));
                _sut.Section(section2, m => _sut.RunValidator(doubleRight, m.DoubleRight));
            });

            //Act
            var result = _sut.ValidateSection(section, model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [TestMethod]
        public void ValidateSection2_WithMultileSubValidators_ShouldRunSection2() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<DoubleModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfo(guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<DoubleModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfo(guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(section, m => _sut.RunValidator(doubleLeft, m.DoubleLeft));
                _sut.Section(section2, m => _sut.RunValidator(doubleRight, m.DoubleRight));
            });

            //Act
            var result = _sut.ValidateSection(section2, model);

            //Assert
            Assert.IsFalse(result.Passes.Any());
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == guid2);
        }
    }
}