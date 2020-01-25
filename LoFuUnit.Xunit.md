[![Build status](https://ci.appveyor.com/api/projects/status/ahjxbhw42vggh0su?svg=true)](https://ci.appveyor.com/project/hlaueriksson/lofuunit) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/lofuunit/badge)](https://www.codefactor.io/repository/github/hlaueriksson/lofuunit)

Use xUnit.net for **BDD**.

Use _local functions_ to structure tests with patterns like:

* `Arrange` / `Act` / `Assert`
* `Given` / `When` / `Then`
* `Context` / `Specification`

LoFuUnit.Xunit and related packages makes it convenient for developers to write tests with _collaboration_ & _communication_ in mind.

## Tests ✔️

An example of a test with `LoFuUnit.Xunit` and [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/):

```csharp
using System;
using FluentAssertions;
using LoFuUnit.Xunit;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LoFuUnitDocs
{
    public class AuthenticationTests : IDisposable
    {
        ITestOutputHelper Output { get; }

        public AuthenticationTests(ITestOutputHelper output) => Output = output;

        public void Dispose() => this.Assert(Output as TestOutputHelper);

        SecurityService Subject;
        UserToken Token;

        [Fact]
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

The `LoFuUnit.Xunit` package provides two important extension methods for test fixtures. The `Assert` and `AssertAsync` methods invokes the test functions in the containing test method. They can be invoked for all test methods in a `Dispose` method, by passing the `TestOutputHelper` as an argument. The invocations will occur in the order that the test functions are declared. If a test function fails, the test method fails directly. Any subsequent test functions in the test method will not be invoked.

More documentation is available at [https://github.com/hlaueriksson/LoFuUnit](https://github.com/hlaueriksson/LoFuUnit)
