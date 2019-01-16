using System;
using System.Linq;
using FlexValidator.Tests.Models;
using FlexValidator.Tests.Validators;
using NUnit.Framework;

namespace FlexValidator.Tests {
    public class SectionedValidatorTests {
        private TestSectionedValidator<SomeModel> _sut;
        private const string Section = "section";
        private const string Section2 = "otherSection";
        private readonly Guid _guid = new Guid("de2319c3-5568-467b-b370-acb486a553f6");
        private readonly Guid _guid2 = new Guid("4ac89b89-52ea-4b50-a283-ea79f29c7122");

        [Test]
        public void Validate_ShouldTestSection() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(Section, x => {
                    _sut.Start(new ValidationInfoBase(_guid));
                    _sut.Pass();
                });
            });

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == _guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [Test]
        public void Validate_ShouldTestAllSections() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(Section, x => {
                    _sut.Start(new ValidationInfoBase(_guid));
                    _sut.Pass();
                });
                validator.Section(Section2, x => {
                    _sut.Start(new ValidationInfoBase(_guid2));
                    _sut.Fail();
                });
            });

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == _guid);
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == _guid2);
        }

        [Test]
        public void ValidateSection_ShouldTestOnlySection() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(Section, x => {
                    _sut.Start(new ValidationInfoBase(_guid));
                    _sut.Pass();
                });
                validator.Section(Section2, x => {
                    _sut.Start(new ValidationInfoBase(_guid2));
                    _sut.Fail();
                });
            });

            //Act
            var result = _sut.ValidateSection(Section, model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == _guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [Test]
        public void ValidateSection2_ShouldTestOnlySection2() {
            //Arrange
            var model = new SomeModel();
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                validator.Section(Section, x => {
                    _sut.Start(new ValidationInfoBase(_guid));
                    _sut.Pass();
                });
                validator.Section(Section2, x => {
                    _sut.Start(new ValidationInfoBase(_guid2));
                    _sut.Fail();
                });
            });

            //Act
            var result = _sut.ValidateSection(Section2, model);

            //Assert
            Assert.IsFalse(result.Passes.Any());
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == _guid2);
        }

        [Test]
        public void Validate_WithSomeValidator_ShouldRun() {
            //Arrange
            var model = new SomeModel();
            var other = new TestSimpleValidator<SomeModel>();
            other.ValidateFunc = x => {
                other.Start(new ValidationInfoBase(_guid));
                other.Pass();
            };

            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => _sut.Section(Section, m => _sut.RunValidator(other, m)));

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == _guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [Test]
        public void Validate_WithSubValidator_ShouldRun() {
            //Arrange
            var model = new SomeModel();
            var sub1 = new TestSimpleValidator<SomeModel>();
            sub1.ValidateFunc = x => {
                sub1.Start(new ValidationInfoBase(_guid));
                sub1.Pass();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => _sut.Section(Section, m => _sut.RunValidator(sub1, m.Some)));

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == _guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [Test]
        public void Validate_WithMultileSubValidators_ShouldRunAllSections() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<SomeModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfoBase(_guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<SomeModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfoBase(_guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(Section, m => _sut.RunValidator(doubleLeft, m.Some));
                _sut.Section(Section2, m => _sut.RunValidator(doubleRight, m.Some));
            });

            //Act
            var result = _sut.Validate(model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == _guid);
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == _guid2);
        }

        [Test]
        public void ValidateSection_WithMultileSubValidators_ShouldRunSection() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<SomeModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfoBase(_guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<SomeModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfoBase(_guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(Section, m => _sut.RunValidator(doubleLeft, m.Some));
                _sut.Section(Section2, m => _sut.RunValidator(doubleRight, m.Some));
            });

            //Act
            var result = _sut.ValidateSection(Section, model);

            //Assert
            Assert.IsTrue(result.Passes.Count() == 1);
            Assert.IsTrue(result.Passes.First().Guid == _guid);
            Assert.IsFalse(result.Fails.Any());
        }

        [Test]
        public void ValidateSection2_WithMultileSubValidators_ShouldRunSection2() {
            //Arrange
            var model = new SomeModel();
            var doubleLeft = new TestSimpleValidator<SomeModel>();
            doubleLeft.ValidateFunc = x => {
                doubleLeft.Start(new ValidationInfoBase(_guid));
                doubleLeft.Pass();
            };
            var doubleRight = new TestSimpleValidator<SomeModel>();
            doubleRight.ValidateFunc = x => {
                doubleRight.Start(new ValidationInfoBase(_guid2));
                doubleRight.Fail();
            };
            _sut = new TestSectionedValidator<SomeModel>();
            _sut.Init(validator => {
                _sut.Section(Section, m => _sut.RunValidator(doubleLeft, m.Some));
                _sut.Section(Section2, m => _sut.RunValidator(doubleRight, m.Some));
            });

            //Act
            var result = _sut.ValidateSection(Section2, model);

            //Assert
            Assert.IsFalse(result.Passes.Any());
            Assert.IsTrue(result.Fails.Count() == 1);
            Assert.IsTrue(result.Fails.First().Guid == _guid2);
        }
    }
}