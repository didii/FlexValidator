# Welcome to the API specification of FlexValidator

See the left window for all navigation to all classes.

Here is a picture of the general hierarchy of the classes.

![Hierarchy](../images/hierarchy.png)

## Validator

Contains the base methods such as `Start`, `Pass`, `Fail` and `Complete`. Has no knowledge about types and only serves as to have a centralized place to place these methods. Those are the core to writing your own validations.

## Validator&lt;T&gt;

All the `Validator` types with generic arguments introduce typing into the system.
Such that a user doesn't have to deal with `object`.
Each generic `Validator` extends its own interface `IValidator` with the same generic arguments which makes it expose a single method:

```csharp
ValidationResult Validate(T1 model1, T2 model2, ...).
```

where the amount or arguments it has corresponds to the amount of generic arguments it has.
FlexValidator currently supports up to 4 arguments.
Creating more is possible, but just needs a lot of boilerplate to make it work.

## SimpleValidator&lt;T&gt; and SectionedValidator&lt;T&gt;

See the [homepage](/#validators) to know how they work.
They provide a structure such that you can write your own validation class.
