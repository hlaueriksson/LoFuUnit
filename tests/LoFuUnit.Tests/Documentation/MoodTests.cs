using FluentAssertions;
using LoFuUnit.AutoNSubstitute;
using LoFuUnit.NUnit;
using NSubstitute;
using NUnit.Framework;

namespace LoFuUnit.Tests.Documentation
{
    /// <summary>
    /// Compare with https://github.com/machine/machine.fakes#withsubjecttsubject
    /// </summary>
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

    public interface ISystemClock
    {
        DateTime CurrentTime { get; set; }
    }

    public class MoodIdentifier
    {
        private readonly ISystemClock _clock;

        public MoodIdentifier(ISystemClock clock)
        {
            _clock = clock;
        }

        public string IdentifyMood()
        {
            return _clock.CurrentTime.DayOfWeek switch
            {
                DayOfWeek.Monday => "Pretty bad",
                DayOfWeek.Friday => "Pretty good",
                _ => "Just fine",
            };
        }
    }
}
