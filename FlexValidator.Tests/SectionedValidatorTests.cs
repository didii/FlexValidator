using System;
using System.Linq;
using FlexValidator.Tests.Models;
using FlexValidator.Tests.Validators;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class SectionedValidatorTests {
        private TestSectionedValidator<SomeModel> _sut;
        private string section = "section";
        private string section2 = "otherSection";
        private Guid guid = new Guid("de2319c3-5568-467b-b370-acb486a553f6");
        private Guid guid2 = new Guid("4ac89b89-52ea-4b50-a283-ea79f29c7122");

        [Test]
        public void Validate_ShouldTestSection() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfoBase(guid));
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

        [Test]
        public void Validate_ShouldTestAllSections() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfoBase(guid));
                    _sut.Pass();
                });
                validator.Section(section2, x => {
                    _sut.Start(new ValidationInfoBase(guid2));
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

        [Test]
        public void ValidateSection_ShouldTestOnlySection() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfoBase(guid));
                    _sut.Pass();
                });
                validator.Section(section2, x => {
                    _sut.Start(new ValidationInfoBase(guid2));
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

        [Test]
        public void ValidateSection2_ShouldTestOnlySection2() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(section, x => {
                    _sut.Start(new ValidationInfoBase(guid));
                    _sut.Pass();
                });
                validator.Section(section2, x => {
                    _sut.Start(new ValidationInfoBase(guid2));
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

        [Test]
        public void Validate_WithSomeValidator_ShouldRun() {
            //Arrange
            var model = new SomeModel();
            var other = new TestSimpleValidator<SomeModel>();
            other.ValidateFunc = x => {
                other.Start(new ValidationInfoBase(guid));
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

        [Test]
        public void Validate_WithSubValidator_ShouldRun() {
            //Arrange
            var model = new SomeModel();
            var sub1 = new TestSimpleValidator<SomeModel>();
            sub1.ValidateFunc = x => {
                sub1.Start(new ValidationInfoBase(guid));
                sub1.Pass();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => _sut.Section(section, m => _sut.RunValidator(sub1, m.Some)));

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [Test]
        public void Validate_WithMultileSubValidators_ShouldRunAllSections() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<SomeModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfoBase(guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<SomeModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfoBase(guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(section, m => _sut.RunValidator(doubleLeft, m.Some));
                _sut.Section(section2, m => _sut.RunValidator(doubleRight, m.Some));
            });

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == guid2);
        }

        [Test]
        public void ValidateSection_WithMultileSubValidators_ShouldRunSection() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<SomeModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfoBase(guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<SomeModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfoBase(guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(section, m => _sut.RunValidator(doubleLeft, m.Some));
                _sut.Section(section2, m => _sut.RunValidator(doubleRight, m.Some));
            });

            //Act
            var result = _sut.ValidateSection(section, model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [Test]
        public void ValidateSection2_WithMultileSubValidators_ShouldRunSection2() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<SomeModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfoBase(guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<SomeModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfoBase(guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(section, m => _sut.RunValidator(doubleLeft, m.Some));
                _sut.Section(section2, m => _sut.RunValidator(doubleRight, m.Some));
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