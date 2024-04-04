using FluentValidation;
using WorldCupScoreboard.Models;
using WorldCupScoreboard.Validators;

namespace WorldCupScoreboardTest
{
    [TestFixture]
    public class TeamValidatorTests
    {
        private IValidator<(Team, Team)> _teamValidator;

        [SetUp]
        public void Setup()
        {
            _teamValidator = new TeamValidator(); 
        }

        [Test]
        public void TeamValidator_WhenValidData_ReturnsValidResult()
        {
            // Arrange
            var team1 = new Team("Team1", TeamType.Home);
            var team2 = new Team("Team2", TeamType.Away);
            var input = (team1, team2);

            // Act
            var result = _teamValidator.Validate(input);

            // Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void TeamValidator_WhenInvalidDataTheSameTeamType_ReturnsInvalidResult()
        {
            // Arrange
            var team1 = new Team("Team1", TeamType.Home);
            var team2 = new Team("Team1", TeamType.Home); // Same team type, should be invalid
            var input = (team1, team2);

            // Act
            var result = _teamValidator.Validate(input);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }

        [Test]
        public void TeamValidator_WhenInvalidDataTheSameCountry_ReturnsInvalidResult()
        {
            // Arrange
            var team1 = new Team("Poland", TeamType.Home);
            var team2 = new Team("Poland", TeamType.Away); 
            var input = (team1, team2);

            // Act
            var result = _teamValidator.Validate(input);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }

        [Test]
        public void TeamValidator_WhenInvalidDataCountryIsEmpty_ReturnsInvalidResult()
        {
            // Arrange
            var team1 = new Team("Poland", TeamType.Home);
            var team2 = new Team("", TeamType.Away);
            var input = (team1, team2);

            // Act
            var result = _teamValidator.Validate(input);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }

        [Test]
        public void TeamValidator_WhenInvalidDataTeamIsNull_ReturnsInvalidResult()
        {
            // Arrange
            var team1 = new Team("Poland", TeamType.Home);
            (Team, Team) input = (team1, null);

            // Act
            var result = _teamValidator.Validate(input);

            // Assert
            Assert.That(result.IsValid, Is.False);
        }
    }
}
