using FluentValidation;
using FluentValidation.Results;
using Moq;
using WorldCupScoreboard.Models;
using WorldCupScoreboard.Services;
using WorldCupScoreboardTest;
using Match = WorldCupScoreboard.Models.Match;

namespace WorldCupScoreboard.Tests
{
    public class FootballScoreboardTests
    {
        private FootballScoreboardService _scoreboard;
        private Mock<IValidator<(Team, Team)>> _mockTeamValidator;

        [SetUp]
        public void Setup()
        {
            _mockTeamValidator = FootballScoreboardTestHelper.CreateMockTeamValidator();
            _scoreboard = new FootballScoreboardService(_mockTeamValidator.Object);
        }


        [Test]
        public void StartNewMatch_OnlyOneMatchInProgress()
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

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _scoreboard.UpdateScore("Italy", "Uruguay", 2, 1));
        }

        [Test]
        public void UpdateScore_ScoreUpdatedCorrectly()
        {
            // Arrange
            var homeTeam = new Team("Spain", TeamType.Home);
            var awayTeam = new Team("Brazil", TeamType.Away);
            _scoreboard.StartMatch(homeTeam, awayTeam);

            // Act
            _scoreboard.UpdateScore("Spain", "Brazil", 2, 1);

            // Assert
            var match = _scoreboard.GetMatchesInProgress().FirstOrDefault();
            Assert.NotNull(match);
            Assert.That(match.HomeScore, Is.EqualTo(2));
            Assert.That(match.AwayScore, Is.EqualTo(1));
        }

        [Test]
        public void GetMatchesInProgress_SummaryIsOrderedCorrectly()
        {
            // Arrange
            _scoreboard.StartMatch(new Team("Mexico", TeamType.Home), new Team("Canada", TeamType.Away));
            _scoreboard.StartMatch(new Team("Spain", TeamType.Home), new Team("Brazil", TeamType.Away));
            _scoreboard.StartMatch(new Team("Germany", TeamType.Home), new Team("France", TeamType.Away));
            _scoreboard.StartMatch(new Team("Uruguay", TeamType.Home), new Team("Italy", TeamType.Away));
            _scoreboard.StartMatch(new Team("Argentina", TeamType.Home), new Team("Australia", TeamType.Away));

            // Act
            _scoreboard.UpdateScore("Mexico", "Canada", 0, 5);
            _scoreboard.UpdateScore("Spain", "Brazil", 10, 2);
            _scoreboard.UpdateScore("Germany", "France", 2, 2);
            _scoreboard.UpdateScore("Uruguay", "Italy", 6, 6);
            _scoreboard.UpdateScore("Argentina", "Australia", 3, 1);

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
        public void FinishMatch_RemovesMatchFromScoreboard()
        {
            // Arrange
            var homeTeam = new Team("Spain", TeamType.Home);
            var awayTeam = new Team("Brazil", TeamType.Away);
            _scoreboard.StartMatch(homeTeam, awayTeam);

            // Act
            _scoreboard.FinishMatch("Spain", "Brazil");

            // Assert
            Assert.That(_scoreboard.GetMatchesInProgress().Count(), Is.EqualTo(0));
        }
    }


}