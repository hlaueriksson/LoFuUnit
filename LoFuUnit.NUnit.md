# LoFuUnit.NUnit

[![build](https://github.com/hlaueriksson/LoFuUnit/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/LoFuUnit/actions/workflows/build.yml) [![CodeFactor](https://codefactor.io/repository/github/hlaueriksson/lofuunit/badge)](https://codefactor.io/repository/github/hlaueriksson/lofuunit)

Use NUnit for **BDD**.

Use _local functions_ to structure tests with patterns like:

* `Arrange` / `Act` / `Assert`
* `Given` / `When` / `Then`
* `Context` / `Specification`

LoFuUnit.NUnit and related packages makes it convenient for developers to write tests with _collaboration_ & _communication_ in mind.

## Tests ✔️

An example of a test with `LoFuUnit.NUnit` and [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/):

```csharp
using FluentAssertions;
using LoFuUnit.NUnit;
using NUnit.Framework;

namespace LoFuUnitDocs
{
    public class AuthenticationTests
    {
        SecurityService Subject;
        UserToken Token;

        [LoFu, Test]
        public void Authenticate_admin_users()
        {
            Subject = new SecurityService();

            void when_authenticating_an_admin_user() =>
                Token = Subject.Authenticate("username", "password");

            void should_indicate_the_user_s_role() =>
                Token.Role.Should().Be(Roles.Admin);

            void should_have_a_unique_session_id() =>
                Token.SessionId.Should().NotBeNull();
        }
    }
}
```

Output:

```txt
Authenticate admin users
    when authenticating an admin user
    should indicate the user's role
    should have a unique session id
```

Test methods can contain local functions that are invoked implicitly. These test functions can perform the _arrange_, _act_ or _assert_ steps of the test.

The `LoFuUnit.NUnit` package provides the `[LoFu]` attributes to mark test methods with.
The attribute invokes the test functions in the containing test method.
The invocations will occur in the order that the test functions are declared.
If a test function fails, the test method fails directly.
Any subsequent test functions in the test method will not be invoked.

More documentation is available at [https://github.com/hlaueriksson/LoFuUnit](https://github.com/hlaueriksson/LoFuUnit)
