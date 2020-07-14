# Best Practices

- [Consider naming fixtures, methods and functions from a users' point-of-view](#consider-naming-fixtures-methods-and-functions-from-a-users-point-of-view)
- [Stick to `Arrange-Act-Assert` conventions](#stick-to-arrange-act-assert-conventions)
- [Test fixtures should be `Snake_cased`](#test-fixtures-should-be-snake_cased)
- [Test methods should be `snake_cased`](#test-methods-should-be-snake_cased)
- [Test functions should be `snake_cased`](#test-functions-should-be-snake_cased)
- [Divide test functions with new line](#divide-test-functions-with-new-line)
- [Test functions should be a single line statement](#test-functions-should-be-a-single-line-statement)
- [Do not use curly brackets for single line test functions](#do-not-use-curly-brackets-for-single-line-test-functions)
- [Fields should appear first in a test fixture](#fields-should-appear-first-in-a-test-fixture)
- [Do not use visibility modifiers on fields](#do-not-use-visibility-modifiers-on-fields)

## Consider naming fixtures, methods and functions from a users' point-of-view

> ✔️ Consider:

```c#
public class Given_a_car
{
    Car subject;
    bool is_stopped;

    [SetUp]
    public void SetUp() =>
        subject = new Car();

    [LoFu, Test]
    public void when_stopped()
    {
        is_stopped = subject.Stop();

        void should_turn_off_the_engine() =>
            is_stopped.Should().BeTrue();
    }
}
```

> ❌ Avoid:

```c#
public class CarTests
{
    Car car;
    bool value;

    [SetUp]
    public void SetUp() =>
        car = new Car();

    [LoFu, Test]
    public void Stop()
    {
        value = car.Stop();

        // What should be true? The test output will be ambiguous!
        void should_be_true() =>
            value.Should().BeTrue();
    }
}
```

## Stick to `Arrange-Act-Assert` conventions

Test fixture `SetUp` is for arrange, test method is for acting, and test functions should be used for asserting.

```c#
public class Given_a_car
{
    Car subject;
    bool is_stopped;

    [SetUp]
    public void SetUp() =>
        subject = new Car(); // arrange

    [LoFu, Test]
    public void when_stopped()
    {
        is_stopped = subject.Stop(); // act

        void should_turn_off_the_engine() =>
            is_stopped.Should().BeTrue(); // assert
    }
}
```

## Test fixtures should be `Snake_cased`

The test fixtures should begin with `Given` and be `Snake_cased`

> ✔️ Consider:

```c#
public class Given_a_car
{
}
```

> ❌ Avoid:

```c#
public class GivenACar
{
}
```

## Test methods should be `snake_cased`

The test methods should begin with `when` and be `snake_cased`

> ✔️ Consider:

```c#
[LoFu, Test]
public void when_stopped()
{
}
```

> ❌ Avoid:

```c#
[LoFu, Test]
public void WhenStopped()
{
}
```

## Test functions should be `snake_cased`

The test functions should begin with `should` and be `snake_cased`

> ✔️ Consider:

```c#
void should_turn_off_engine() =>
    is_stopped.Should().BeTrue();
```

> ❌ Avoid:

```c#
void ShouldTurnOffEngine() =>
    is_stopped.Should().BeTrue();
```

## Divide test functions with new line

> ✔️ Consider:

```c#
void should_turn_off_the_engine() =>
    is_stopped.Should().BeTrue();
```

> ❌ Avoid:

```c#
void should_turn_off_the_engine() => is_stopped.Should().BeTrue();
```

## Test functions should be a single line statement

> ✔️ Consider:

```c#
void should_turn_off_engine() =>
    is_stopped.Should().BeTrue();
```

> ❌ Avoid:

```c#
void should_turn_off_engine()
{
    var stopped = subject.Stop();

    stopped.Should().BeTrue();
}
```

## Do not use curly brackets for single line test functions

> ✔️ Consider:

```c#
void should_turn_off_the_engine() =>
    is_stopped.Should().BeTrue();
```

> ❌ Avoid:

```c#
void should_turn_off_the_engine() =>
{
    is_stopped.Should().BeTrue();
}
```

## Fields should appear first in a test fixture

> ✔️ Consider:

```c#
public class Given_a_car
{
    bool is_stopped;

    [LoFu, Test]
    public void when_stopped()
    {
        is_stopped = subject.Stop();

        void should_turn_off_the_engine() =>
            is_stopped.Should().BeTrue();
    }
}
```

> ❌ Avoid:

```c#
public class Given_a_car
{
    [LoFu, Test]
    public void when_stopped()
    {
        is_stopped = subject.Stop();

        void should_turn_off_the_engine() =>
            is_stopped.Should().BeTrue();
    }

    bool is_stopped;
}
```

## Do not use visibility modifiers on fields

> ✔️ Consider:

```c#
public class Given_a_car
{
    bool is_stopped;
}
```

> ❌ Avoid:

```c#
public class Given_a_car
{
    private bool is_stopped;
}
```
