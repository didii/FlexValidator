# FlexValidator
A flexible validator made for C#.

See the source code on GitHub: https://github.com/didii/FlexValidator.

## Table of contents
- [FlexValidator](#flexvalidator)
  - [Table of contents](#table-of-contents)
  - [Why?](#why)
  - [Installation](#installation)
  - [How to use it?](#how-to-use-it)
    - [Validation Syntax](#validation-syntax)
    - [Testing](#testing)
    - [Validators](#validators)
      - [SimpleValidator](#simplevalidator)
      - [SectionedValidator](#sectionedvalidator)
    - [Utility methods](#utility-methods)
  - [Todo](#todo)

## Why?

I've recently been in a project that had a lot of very complex business rules.
It was partly law-based and thus had rules with exceptions and exceptions and exceptions.
We first started using [FluentValidator](https://github.com/JeremySkinner/FluentValidation),
which - don't get me wrong - is a good framework,
but is not suited for this kind of complex logic with numerous of properties.

What we needed was:

* Seperation of concerns
  * E.g. Validating the connection between property A and property B has nothing to do with that of property A and property C. Test should isolate those validations.
* Testability
  * We need to know whether a validation failed, but also when it passed
  * Seperation of concern is here related
* Complex logic has still to be readable

So I came up with another way to write validations where you can simply make use of statements you know: `if`, `else`, `&&`, .... And where every validation can be nicely seperated from the rest.

## Installation

Either in Visual Studio, using the *Manage NuGet Packages...* menu-item on your project or solution.

Or using the Package Manager console:

```
PM> Install-Package didii.FlexValidator
```

Or using the .NET CLI:

```
> dotnet add package didii.FlexValidator
```

## How to use it?

When you already have a validator, it's as easy as it gets:

```csharp
// Get your model and validator from somewhere
SomeModel model = ...;
IValidator<SomeModel> validator = ...;

// Validate the model
var result = validator.Validate(model);
// or
var result = await validator.ValidateAsync(model);

// Check the result
if (result.IsValid)
    // yay the model is valid
else
    // now it is not valid
    // you can inspect result.Fails to see which ones failed
```

The `result` object contains a list of `Passes` and `Fails` so you can inspect what happened.

### Validation Syntax

The default syntax is pretty straightforward.

```csharp
private void ValidateName(SomeModel model) {
    //Start a single validation by calling Start and providing an instance of ValidationInfo
    //You are required to give it a GUID as its identifier and a message
    Start(new ValidationInfo("d4c99639-dd0f-49ba-921f-0c53653b2326", "Name cannot be null"));

    //Write your validation logic
    if (model.Name == null)
        Fail(); //This fails the last started validation
    else
        Pass(); //This passes the last started validation
    
    //Complete the validation
    Complete();
    //Using Complete without arguments is recommended, but not required
}
```

The structure is like this:

1. Start the validation by calling `Start` and provide it with information about the validation.
   1. If you want more properties, simply extend `ValidationInfo` and add the properties you need
2. After `Start` you can write your custom business logic that leads to calls to `Pass` or `Fail`.
   1. A single validation rule can only fail or pass once. When `Fail` or `Pass` is encoutered multiple times, it will throw a `InvalidValidatorStateException`.
3. You can complete the rule by calling `Complete`
   1. It's recommended to always do this. Calling it without arguments does a check to see if the current rule had a call to `Fail` or `Pass`. If not, it will throw a `InvalidValidatorStateException`.

By using the argument in `Complete` you also simplify the test a bit.

```csharp
private void ValidateName(SomeModel model) {
    Start(new ValidationInfo("d4c99639-dd0f-49ba-921f-0c53653b2326", "Name cannot be null"));
    if (model.Name == null)
        Fail();
    Complete(Assume.Pass);
}
```

Now when `Complete` is encountered and no call to `Pass` or `Fail` were made beforehand, it will call `Pass` for you.
You can also use `Assume.Fail` to assume the test to be failed if no `Pass` or `Fail` was encountered.

Validations can follow each other up as much as you want

```csharp
private void ValidateName(SomeModel model) {
    Start(new ValidationInfo("d4c99639-dd0f-49ba-921f-0c53653b2326", "Name cannot be null"));
    if (model.Name == null) {
        Fail();
        //Short-circuit here: other validations don't matter if Name is null
        return;
    }
    Complete(Assume.Pass);

    Start(new ValidationInfo("e1410df7-1438-4b5a-a755-1c8657f827a2", "Name cannot be empty"));
    if (model.Name.Length == 0) {
        Fail();
        //Short-circuit again
        return;
    }
    Complete(Assume.Pass);

    Start(new ValidationInfo("9aee2017-5c09-485d-8d91-c2d8a102c569", "Name must start with an alphabetical letter"))
    if (new Regex(@"[a-zA-Z]").IsMatch(model.name))
        Pass();
    Complete(Assume.Fail);

    //...
}
```

Note that since we write our validations using simple statements, we can short-circuit out a validation.
If `Name` is `null`, it does not make sense to still try and run the other validations.
The second validation will fail anyway since this will throw a `NullReferenceException`.

### Testing

You're probably also here to know whether or not this makes testing validations easier or not.
Well, according to my findings, it does.
This is why we need the GUID so that a single validation rule can always be identified.
See more below why I chose to use GUIDs.

The test is now pretty easy to write.
In this example I use `NUnit`, but it can be done with any testing framework.

```csharp
enum Must {
    Pass,
    Fail,
}

[TestCase(Must.Fail, null)]
[TestCase(Must.Pass, "")]
[TestCase(Must.Pass, "some text")]
public void Validate_NameCannotBeNull(Must type, string name) {
    //Arrange
    var validator = new MyValidator();
    var model = new SomeModel() {
        Name = name
    }

    //Act
    var result = validator.Validate(model);

    //Assert
    result.Should(type, "d4c99639-dd0f-49ba-921f-0c53653b2326");
}
```

We have defined an extension method `Should` on `ValidationResult` to make our lives easier.
See [ValidationResultExtensions.cs](https://github.com/didii/FlexValidator/blob/master/FlexValidator.Example.App.Tests/Helpers/ValidationResultExtensions.cs) for the implemenatation.
In words, it simply checks if `result` has the given validation (identified by the GUID) in its collection of `Fails` or `Passes` and calls `Assert.Fail` when it's in the wrong collection or it does not exist.

Take note that the GUID is the same as the first validation we defined above.

### Validators

So, where do you write that code you ask?
I've created 2 validators that you can extend to write your logic for you objects.
The `SimpleValidator` and the `SectionedValidator`.

#### SimpleValidator

To be used for simple validations where you can test the whole model through.
You don't need seperate _sections_ to seperate logic for testing.
Ideal for small classes such as a base class that exposes an `Id`.

To use, simply make a validator that inherits from `SimpleValidator<>` and specify (all) the model(s) you want to validate. Then override `DoValidate` and place your logic in there.

```csharp
class SomeValidator : SimpleValidator<SomeModel> {
    // Override the DoValidate method 
    protected override void DoValidate(SomeModel model) {
        // Use the custom validation logic here
        Start(new ValidationInfo("someGuid", "Name cannot be null"));
        if (model.Name == null) {
            Fail();
            return;
        }
        Complete(Assume.Pass);
    }
}
```

#### SectionedValidator

To be used for complex models.
You want to seperate logic and isolate certain validation rules so they can be tested properly.
You don't want the validation of your object to run in one go for testing purposes.

Again, simply make a validator that inherits from `SectionedValidator<>` and specify (all) the model(s) you want to validate.

```csharp
class OtherValidator : SectionedValidator<OtherModel> {
    // Declare publicly visible section names to be used in the tests
    public const string IdSection = "Id";
    public const string NameSection = "Name";
    
    // Constructor where you define all your sections
    public OtherValidator() {
        //Create a validation section to validate only the Id
        Section(IdSection, ValidateId);
        //Create another section to only validate the Name
        Section(NameSection, ValidateSome);
    }
    
    private void ValidateId(OtherModel model) {
        Start(new ValidationInfo("someGuid", "Id must be greater than 0"));
        if (model.Id > 0)
            Pass();
        Complete(Assume.Fail); 
    }
    
    private void ValidateName(OtherModel model) {
        Start(new ValidationInfo("someOtherGuid", "Name cannot be null or empty"));
        if (string.IsNullOrEmpty(model.Name))
            Fail();
        Complete(Assume.Pass);
    }
}
```

In the constructor we create 2 sections that validate each property of `OtherModel` seperately.
The end result will be exactly the same: running `validator.Validate(model)` will still execute each section and all validations will be run.
But for tests it is different.

Here we can call the method `ValidateSection` instead to make sure only that part is called.
If we have left a `throw Exception()` in another section, this test won't be influenced at all.

```csharp
[TestCase(Must.Fail, -5)]
[TestCase(Must.Fail, 0)]
[TestCase(Must.Pass, 9)]
public void Validate_IdSection_IdMustBeGreaterThanZero(Must type, long id) {
    //Arrange
    var validator = new MyValidator();
    var model = new SomeModel() {
        Id = id
    };
    
    //Act
    var result = validator.ValidateSection(SomeValidator.IdSection, model);
    
    //Assert
    result.Should(type, "someGuid");
}
```

Note that internally the sections are stored in a dictionary. This means order is not guaranteed to be reserved when executing `Validate`. This is intentional and makes it so that your sections need to be entirely independent of each other.

### Utility methods

There are also a couple of utility methods available.

| Method       | Description                                                                        |
|--------------|------------------------------------------------------------------------------------|
| `RunValidator` | Runs another validator on the model you've given it. To be used for nested models. |
| `Passed`       | Give a GUID of a prior test and it will return true if that test passed.           |
| `Failed`       | Give a GUID of a prior test and it will return true if that test failed.           |

Check the API for more info.


## Todo

These are things that still need to be added.

**Important**

* ~~Async support~~
  * Still needs to be tested, but the code is there
* Pass data to children validators such as their name

**Less important**

* Custom `ValidationInfo` object in `Start` and `ValidationResult`
  * For now you can create a class that inherits from `ValidationInfo` and pass that to `Start`. You will however have to cast every validation result to your own type afterwards.
* ~~Make lookups faster (used in `Passed` and `Failed`)~~
  * ~~The `List` should become a `Dictionary`~~
* ~~Make extensibility easier~~
