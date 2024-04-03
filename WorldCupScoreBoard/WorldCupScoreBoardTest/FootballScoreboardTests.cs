using System.Text.RegularExpressions;

namespace WorldCupScoreboard.Tests
{
    public class FootballScoreboardTests
    {
        private FootballScoreboard _scoreboard;

        [SetUp]
        public void Setup()
        {
            _scoreboard = new FootballScoreboard();
        }

        [Test]
        public void StartNewMatch()
        {
            // Arrange
            Team homeTeam = new Team("Mexico", TeamType.Home);
            Team awayTeam = new Team("Canada", TeamType.Away);

            // Act
            _scoreboard.StartMatch(homeTeam, awayTeam);

            // Assert
            Assert.That(_scoreboard.GetMatchesInProgress().Count(), Is.EqualTo(1));
        }
    }
}