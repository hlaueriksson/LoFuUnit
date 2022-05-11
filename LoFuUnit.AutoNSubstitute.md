# LoFuUnit.AutoNSubstitute

[![build](https://github.com/hlaueriksson/LoFuUnit/actions/workflows/build.yml/badge.svg)](https://github.com/hlaueriksson/LoFuUnit/actions/workflows/build.yml) [![CodeFactor](https://codefactor.io/repository/github/hlaueriksson/lofuunit/badge)](https://codefactor.io/repository/github/hlaueriksson/lofuunit)

Use LoFuUnit and NSubstitute to automatically `Mock` / `Fake` / `Stub` dependencies.

LoFuUnit.AutoNSubstitute and related packages makes it convenient for developers to write tests with _collaboration_ & _communication_ in mind.

## Mocks 🦆

An example of a test with `LoFuUnit.AutoNSubstitute`, [LoFuUnit.NUnit](https://www.nuget.org/packages/LoFuUnit.NUnit/) and [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/):

```csharp
using System;
using FluentAssertions;
using LoFuUnit.AutoNSubstitute;
using LoFuUnit.NUnit;
using NSubstitute;
using NUnit.Framework;

namespace LoFuUnitDocs
{
    public class MoodTests : LoFuTest<MoodIdentifier>
    {
        string _mood;

        [LoFu, Test]
        public void Identify_mood_on_mondays()
        {
            void given_the_current_day_is_monday()
            {
                var monday = new DateTime(2011, 2, 14);

                Use<ISystemClock>()
                    .CurrentTime
                    .Returns(monday);
            }

            void when_identifying_my_mood() =>
                _mood = Subject.IdentifyMood();

            void should_be_pretty_bad() =>
                _mood.Should().Be("Pretty bad");
        }
    }
}
```

Output:

```txt
Identify mood on mondays
    given the current day is monday
    when identifying my mood
    should be pretty bad
```

The `LoFuTest<TSubject>` base class provides auto-mocking capabilities. The generic type parameter defines what kind of _subject_ under test to create.

The `Use<TDependency>` method creates a mock that the _subject_ is dependent upon, and it can later be accessed via the `The<TDependency>` method.
Use these methods to configure the behavior and verify the interaction with the mocks.

The `Subject` property returns an auto-mocked instance of the _subject_ under test.

More documentation is available at [https://github.com/hlaueriksson/LoFuUnit](https://github.com/hlaueriksson/LoFuUnit)
