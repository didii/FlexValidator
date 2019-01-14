# Validator
A flexible validator made for C#.

## Why?

I've recently been in a project that had a lot of very complex business rules.
It was partly law-based and thus had rules with exceptions and exceptions and exceptions.
We first started using [FluentValidator](https://github.com/JeremySkinner/FluentValidation),
which - don't get me wrong - is a good framework,
but is not suited for this kind of complex logic with numerous of properties.

The problems we had was:

* Seperation of concerns (properties that depend on each other need to be in the top-level validator)
* Testability (we also need to know if a test was passed)
* Complex logic isn't readable anymore in the Fluent-style
* Validations based on multiple properties are weird to write

So I came up with another way to write validations where you can simply make use of statements you know (`if`, `else`, `&&`).

## How to use?

There are 2 main types of validators.

### SimpleValidator

To be used for simple validations where you can test the whole model through.
You don't need seperate _sections_ to seperate logic for testing.

Simply make a validator that inherits from `SimpleValidator` and specify (all) the model(s) you want to validate.

```csharp
class SomeValidator : SimpleValidator<SomeModel> {
    // Override the DoValidate method 
    protected override void DoValidate(SomeModel model) {
        Start(new ValidationInfo("someGuid", "Name cannot be null"));
        if (model.Name == null)
            Fail();
            //Short-circuit here, we don't do any other validation when Name is null
            return;
        else
            Pass();
        Complete();
        
        Start(new ValidationInfo("someOtherGuid", "Name cannot be empty"));
        if (model.Name.Length == 0)
            Fail();
        // We skip a call to Pass here, instead we use the argument in Complete
        // When set to Assume.Pass, we assume that the test was passed if no Pass or Fail was called
        // Assume.Fail assumes the test to be failed
        // Note: when no argument is given, Complete will throw if no Pass or Fail was encoutered
        Complete(Assume.Pass);
    }
}
```

Now you have a validator that validates the `Name` of `SomeModel` to be not null and not empty with helpful messages.
The given info is registered when calling `Fail()` or `Pass()` and more validations can follow.

Take note that in this structure you can easily short-circuit the validation.
When `Name` is `null`, we don't bother checking the rest.
If we didn't do this, we'd always have to re-check on `null` or get a `NullReferenceException`.
Short-circuiting makes the code a lot more cleaner and readable.

The state can later be inspected when the validator was run.

```csharp
SomeModel model = ...;
SomeValidator validator = ...;
var result = validator.Validate(model);

if (result.IsValid) {
    //Yay, the model is valid
} else {
    //Aww, the model isn't valid
    //Inspect result.Fails and result.Passes to see which rule failed or passed
}
```

### SectionedValidator

To be used for complex models.
You want to seperate logic and isolate certain validation rules so they can be tested properly.

Again, simply make a validator that inherits from `SectionedValidator` and specify (all) the model(s) you want to validate.

```csharp
class OtherValidator : SectionedValidator<OtherModel> {
    public const string IdSection = "Id";
    public const string SomeSection = "Some";
    
    private readonly SomeValidator _someValidator = someValidator;
    
    // Inject other validators using your favorite DI container
    public OtherValidator(SomeValidator someValidator) {
        _someValidator = someValidator;
        
        //Create a validation section with a publicly visible name
        Section(IdSection, ValidateId);
        //Create another validation section
        Section(SomeSection, ValidateSome);
    }
    
    private void ValidateId(OtherModel model) {
        // Has the same structure as the main body of the simple validator
        Start(new ValidationInfo("someGuid", "someMessage"));
        if (model.Id > 0)
            Pass();
        Complete(false); //The false here means: if no Pass or Fail was encoutered, assume the validation failed
    }
    
    private void ValidateSome(OtherModel model) {
        Start(new ValidationInfo("someOtherGuid", "Some cannot be null"));
        if (model.Some == null)
            Fail();
            // Short-circuit here, we don't have to check anything else
            return;
        else {
            Pass();
            // Only run the sub-validator if model.Some is not null
            RunValidator(_someValidator, model.Some);
        }
        Complete();
    }
}
```

So this is a bit more complex.
We declare two publicly visible constant strings that are the names of our sections.
We inject another validator we'll use later.

Then we define 2 sections referring to the private methods we defined.
The body of these methods are to be used in the exact same way as the `DoValidate` method of the `SimpleValidator`.
The difference is that we can call this specific body seperately when needed.
For example in tests.

```csharp
private SomeValidator _sut = new SomeValidator();

[Test]
public void Test() {
    //Arrange
    SomeModel model = ...;
    
    //Act
    var result = _sut.ValidateSection(SomeValidator.IdSection, model);
    
    //Assert
    result.ShouldPass("someGuid");
}
```

This test will only bother to run the method `ValidateId` and will not run `ValidateSome`.
This makes it a lot easier to write tests since we do not yet have to mock `SomeValidator` when it's not needed.
All our tests will also not fail when we accidentily introduce a Exception in a section.

The other section simply shows how to call a nested validator.
Simply use the method `RunValidator` and pass the validator instance and model.
It will capture all passes and fails from that validator and adds it to the one you are working with.

## How did you make it?

It's ... complicated.

## Todo

* Async support
* Custom `ValidationInfo` object in `Start`
* Make lookups faster (used in `Passed` and `Failed`)
  * The `List` should become a `Dictionary`
