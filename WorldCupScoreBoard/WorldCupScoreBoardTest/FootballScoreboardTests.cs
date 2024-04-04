using FluentValidation;
using Moq;
using WorldCupScoreboard.Models;
using WorldCupScoreboard.Services;
using WorldCupScoreboard.Test;
using Match = WorldCupScoreboard.Models.Match;

namespace WorldCupScoreboard.Tests
{
    public class FootballScoreboardTests
    {
        private FootballScoreboardService _scoreboard;
        private Mock<IValidator<(Team, Team)>> _mockTeamValidator;
        private Mock<IValidator<MatchUpdateInfo>> _mockMatchUpdateValidator;

        [SetUp]
        public void Setup()
        {
            _mockTeamValidator = FootballScoreboardTestHelper.CreateMockTeamValidator();
            _mockMatchUpdateValidator = FootballScoreboardTestHelper.CreateMockMatchUpdateInfoValidator();
            _scoreboard = new FootballScoreboardService(_mockTeamValidator.Object, _mockMatchUpdateValidator.Object);
        }


        [Test]
        public void StartNewMatch_OnlyOneMatchInProgress_ShouldPass()
        {
            // Arrange
            var homeTeam = new Team("Mexico", TeamType.Home);
            var awayTeam = new Team("Canada", TeamType.Away);

            // Act
            _scoreboard.StartMatch(homeTeam, awayTeam);

            // Assert
            Assert.That(_scoreboard.GetMatchesInProgress().Count(), Is.EqualTo(1));
        }

        [Test]
        public void StartMatch_SameHomeAndAwayTeam_ThrowsArgumentException()
        {
            // Arrange
            var sameTeam = new Team("Mexico", TeamType.Home);
            var awayTeam = new Team("Mexico", TeamType.Away);

            // Act and Assert
            Assert.Throws<ValidationException>(() => _scoreboard.StartMatch(sameTeam, sameTeam));
        }

        [Test]
        public void StartMatch_SameTypeTeam_ThrowsArgumentException()
        {
            // Arrange
            var sameTeam = new Team("Mexico", TeamType.Home);
            var awayTeam = new Team("Canada", TeamType.Home);

            // Act and Assert
            Assert.Throws<ValidationException>(() => _scoreboard.StartMatch(sameTeam, sameTeam));
        }

        [Test]
        public void UpdateScore_MatchNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            _scoreboard.StartMatch(new Team("Spain", TeamType.Home), new Team("Brazil", TeamType.Away));
            _scoreboard.StartMatch(new Team("Germany", TeamType.Home), new Team("France", TeamType.Away));

            var matchUpdateInfo = new MatchUpdateInfo() { HomeTeam = "Italy", AwayTeam = "Uruguay", HomeScore = 2, AwayScore = 2 };

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _scoreboard.UpdateScore(matchUpdateInfo));
        }

        [Test]
        public void UpdateScore_ScoreUpdatedCorrectly_ShouldPass()
        {
            // Arrange
            var homeTeam = new Team("Spain", TeamType.Home);
            var awayTeam = new Team("Brazil", TeamType.Away);
            _scoreboard.StartMatch(homeTeam, awayTeam);

            // Act

            var matchUpdateInfo = new MatchUpdateInfo() { HomeTeam = "Spain", AwayTeam = "Brazil", HomeScore = 2, AwayScore = 1 };
            _scoreboard.UpdateScore(matchUpdateInfo);

            // Assert
            var match = _scoreboard.GetMatchesInProgress().FirstOrDefault();
            Assert.NotNull(match);
            Assert.That(match.HomeScore, Is.EqualTo(2));
            Assert.That(match.AwayScore, Is.EqualTo(1));
        }

        [Test]
        public void GetMatchesInProgress_SummaryIsOrderedCorrectly_ShoulPass()
        {
            // Arrange
            _scoreboard.StartMatch(new Team("Mexico", TeamType.Home), new Team("Canada", TeamType.Away));
            _scoreboard.StartMatch(new Team("Spain", TeamType.Home), new Team("Brazil", TeamType.Away));
            _scoreboard.StartMatch(new Team("Germany", TeamType.Home), new Team("France", TeamType.Away));
            _scoreboard.StartMatch(new Team("Uruguay", TeamType.Home), new Team("Italy", TeamType.Away));
            _scoreboard.StartMatch(new Team("Argentina", TeamType.Home), new Team("Australia", TeamType.Away));

            // Act
            _scoreboard.UpdateScore(new MatchUpdateInfo() { HomeTeam = "Mexico", AwayTeam = "Canada", HomeScore = 0, AwayScore = 5 });
            _scoreboard.UpdateScore(new MatchUpdateInfo() { HomeTeam = "Spain", AwayTeam = "Brazil", HomeScore = 10, AwayScore = 2 });
            _scoreboard.UpdateScore(new MatchUpdateInfo() { HomeTeam = "Germany", AwayTeam = "France", HomeScore = 2, AwayScore = 2 });
            _scoreboard.UpdateScore(new MatchUpdateInfo() { HomeTeam = "Uruguay", AwayTeam = "Italy", HomeScore = 6, AwayScore = 6 });
            _scoreboard.UpdateScore(new MatchUpdateInfo() { HomeTeam = "Argentina", AwayTeam = "Australia", HomeScore = 3, AwayScore = 1 });

            // Assert
            var expectedSummary = new List<Match>
            {
                new Match(new Team("Uruguay", TeamType.Home), new Team("Italy", TeamType.Away), 6, 6),
                new Match(new Team("Spain", TeamType.Home), new Team("Brazil", TeamType.Away), 10, 2),
                new Match(new Team("Mexico", TeamType.Home), new Team("Canada", TeamType.Away), 0, 5),
                new Match(new Team("Argentina", TeamType.Home), new Team("Australia", TeamType.Away), 3, 1),
                new Match(new Team("Germany", TeamType.Home), new Team("France", TeamType.Away), 2, 2)
            };

            var result = _scoreboard.GetMatchesInProgress().ToList();

            CollectionAssert.AreEqual(expectedSummary, result);
        }

        [Test]
        public void FinishMatch_RemovesMatchFromScoreboard_ShouldPass()
        {
            // Arrange
            var homeTeam = new Team("Spain", TeamType.Home);
            var awayTeam = new Team("Brazil", TeamType.Away);
            _scoreboard.StartMatch(homeTeam, awayTeam);

            // Act
            _scoreboard.FinishMatch(new MatchUpdateInfo() { HomeTeam = "Spain", AwayTeam = "Brazil" });

            // Assert
            Assert.That(_scoreboard.GetMatchesInProgress().Count(), Is.EqualTo(0));
        }


        [Test]
        public void FinishMatch_NoTeam_ShouldFail()
        {
            // Arrange
            var homeTeam = new Team("Spain", TeamType.Home);
            var awayTeam = new Team("Brazil", TeamType.Away);
            _scoreboard.StartMatch(homeTeam, awayTeam);

            //Act
            var matchUpdateInfo = new MatchUpdateInfo() { HomeTeam = "Poland", AwayTeam = "Brazil" };

            // Assert
            Assert.Throws<ArgumentException>(() => _scoreboard.FinishMatch(matchUpdateInfo));
        }
    }


}