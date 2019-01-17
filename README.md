# FlexValidator
A flexible and testable validator made for C#.

See the docs for all info you need: [API docs](https://didii.github.io/FlexValidator/).
This readme is only part of it

## Syntax

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

## Testing

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

## Todo

These are things that still need to be added.

**Important**

* Pass data to children validators such as their name

**Less important**

* Custom `ValidationInfo` object in `Start` and `ValidationResult`
  * For now you can create a class that inherits from `ValidationInfo` and pass that to `Start`. You will however have to cast every validation result to your own type afterwards.
