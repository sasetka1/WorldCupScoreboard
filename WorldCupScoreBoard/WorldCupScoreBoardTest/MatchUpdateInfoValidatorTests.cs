using WorldCupScoreboard.Models;
using WorldCupScoreboard.Validators;

namespace WorldCupScoreboard.Test
{
    [TestFixture]
    public class MatchUpdateInfoValidatorTests
    {
        private MatchUpdateInfoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MatchUpdateInfoValidator();
        }

        [Test]
        public void Validate_WithValidInput_ShouldPass()
        {
            // Arrange
            var matchUpdateInfo = new MatchUpdateInfo
            {
                HomeTeam = "Home",
                AwayTeam = "Away",
                HomeScore = 2,
                AwayScore = 1
            };

            // Act
            var result = _validator.Validate(matchUpdateInfo);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Validate_WithNegativeHomeScore_ShouldFail()
        {
            // Arrange
            var matchUpdateInfo = new MatchUpdateInfo
            {
                HomeTeam = "Home",
                AwayTeam = "Away",
                HomeScore = -1, // Negative home score
                AwayScore = 1
            };

            // Act
            var result = _validator.Validate(matchUpdateInfo);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void Validate_WithNegativeAwayScore_ShouldFail()
        {
            // Arrange
            var matchUpdateInfo = new MatchUpdateInfo
            {
                HomeTeam = "Home",
                AwayTeam = "Away",
                HomeScore = 2,
                AwayScore = -1 // Negative away score
            };

            // Act
            var result = _validator.Validate(matchUpdateInfo);

            // Assert
            Assert.IsFalse(result.IsValid);
        }


    }
}
