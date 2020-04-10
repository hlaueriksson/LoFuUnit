[![Build status](https://ci.appveyor.com/api/projects/status/ahjxbhw42vggh0su?svg=true)](https://ci.appveyor.com/project/hlaueriksson/lofuunit) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/lofuunit/badge)](https://www.codefactor.io/repository/github/hlaueriksson/lofuunit)

Use MSTest for **BDD**.

Use _local functions_ to structure tests with patterns like:

* `Arrange` / `Act` / `Assert`
* `Given` / `When` / `Then`
* `Context` / `Specification`

LoFuUnit.MSTest and related packages makes it convenient for developers to write tests with _collaboration_ & _communication_ in mind.

## Tests ✔️

An example of a test with `LoFuUnit.MSTest` and [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/):

```csharp
using FluentAssertions;
using LoFuUnit.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFuUnitDocs
{
    [TestClass]
    public class AuthenticationTests : LoFuTest
    {
        SecurityService Subject;
        UserToken Token;

        [TestMethod]
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

The `LoFuUnit.MSTest` package contains the `LoFuTest` base class for test fixtures to inherit from. This class defines a method marked with the `[TestCleanup]` attribute, and it is called after running each test in the test fixture. The `TestCleanup` method invokes the test functions in the containing test method that was just executed. The invocations will occur in the order that the test functions are declared. If a test function fails, the test method fails directly. Any subsequent test functions in the test method will not be invoked.

More documentation is available at [https://github.com/hlaueriksson/LoFuUnit](https://github.com/hlaueriksson/LoFuUnit)
