# Welcome to the API specification of FlexValidator

See the left window for all navigation to all classes.

This page gives a simple overview on what to find. More details can be found in the API documentation itself.

Here is a picture of the general hierarchy of the classes.

![Hierarchy](../images/hierarchy.png)

## Validator

Contains the base methods such as `Start`, `Pass`, `Fail` and `Complete`. Has no knowledge about types and only serves as to have a centralized place to place these methods. Those are the core to writing your own validations.

## SimpleValidator

Provides a type-less base structure for all typed `SimpleValidator<...>`s such that their logic is centralized.
This implements logic for a method `ValidationResult Validate(object[])` such that state is correctly set before and the right result is returned.
It also defines `virtual` methods for a user to implement where they can write their validation logic.

## SimpleValidator&lt;...&gt;

Exposes typed methods that a user can implement and use.
For more information about how to use them, see the [homepage](/#validators).

## SectionedValidator

Provides a type-less base structure for all typed `SectionedValidator<...>`s such that their logic is centralized.
This class internally keeps a dictionary of the sections that are defined and simply loops over all items when `Validate` is called.
The method `Section` simply stores the given key-value pair.

## SectionedValidator&lt;...&gt;

Just as with the `SimpleValidator<...>`, it exposes methods that a user can implement and use.
For more information about how to use them, see the [homepage](/#validators).
