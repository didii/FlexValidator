# Validator

## How did you make it?

It's ... complicated.

The class hierarchy:

```
                                        Validator
                                            |
                   +------------------------+------------------------+---------------- - -  -  -
                   |                                                 |
                   V                                                 V
              Validator<T>                                  Validator<T1, T2>
                   |                                                 |
        +----------+-----------+                       +-------------+-------------+
        |                      |                       |                           |
        V                      V                       V                           V
SimpleValidator<T>    SectionedValidator<T>    SimpleValidator<T1, T2>    SectionedValidator<T1, T2>
```

### Validator

This class allows the user to write easy to use and understand code.
It partly keeps state about what the user is currently validating without taking away too much control.
It exposes the following methods:

* `Start(ValidationInfo info)`
* `Pass()`
* `Fail()`
* `Complete(Assume? assume)`
* `RunValidator<T1, ...>(IValidator<T1, ...> validator, T1 obj, ...)`

The method `Start` temporarily saves the given info and passes it to its inner `ValidationResult` when `Pass` or `Fail` is called.
Then the `info` is reset and a new validation can occur.
The method `Complete` is provided to clearly delimit a single validation or it can auto-pass or fail a method using the argument.

Lastly we have `RunValidator` that simply calls the inner validator, captures its result and adds it to its own inner `ValidationResult`.
I do have an issue with the method since at this point, `Validator` should not have any knowledge about types.
I could move them to an extension method, which would make sense, but this forces users to write `this.RunValidator` which is arguably even dumber.

We cannot use types here since we have an arbitrary amount of parameters.
We'll have to introduce types again later down the line.


### Validator&lt;T&gt;

This class is meant to re-introduce typings so any user of this code doesn't have to deal with casts.
It simply exposes the Validate method and redirects `ValidatorFunc` to use the exposed `Validate` method.
When `Validate` is overridden, `ValidatorFunc` will still execute the correct method.

This however introduces a problem: we want to be able to have any amount of variables typed.
This is not possible in C# as far as I know, so we'll have to define them manually.
Hence the `Validator<T1, T2>`, `Validator<T1, T2, T3>` etc.

### SimpleValidator&lt;T&gt;

This class is meant to be overriden by a user.
These validators simply expose a method `DoValidate` that is run when the public method `Validate` is called.
It simply makes sure the state is reset correctly before every `Validate` call.

The same problem occurs here as with `Validator<T>`.
We need to define another `SimpleValidator<T1, T2>` to make a validator that needs multiple arguments.

### SectionedValidator&lt;T&gt;

This class is meant to be overriden by a user.
This internally holds a collection of sections that can be run individually using `ValidateSection` and can be created using `Section`.
In all other aspects, it's exactly the same as the `SimpleValidator`.

## Extensibility

This also poses a problem.
If you look at the code, you'll notice that a lot of methods are `internal`.
This is done on purpose to hide all implementation details if you want to use the existing validators.
However, when you want to extend the existing validators (e.g. add a fourth tier: `Validator<T1, T2, T3, T4>`) or a different kind of validator that better suits your needs, you can't do it because you don't have access to the internal workings of the validators.

I have added an assembly attribute: `InternalsVisibleTo("Validator.Extensions")` so you can test this extensibility in your own project.
However, the dll name has to be exactly the same as `Validator.Extensions` which is not really handy.

Proposals are welcome.