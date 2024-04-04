using FluentValidation.Results;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupScoreboard.Models;

namespace WorldCupScoreboardTest
{
    public class FootballScoreboardTestHelper
    {
        public static Mock<IValidator<(Team, Team)>> CreateMockTeamValidator()
        {
            var mockTeamValidator = new Mock<IValidator<(Team, Team)>>();

            // Setup for successful validation
            mockTeamValidator.Setup(v => v.Validate(It.IsAny<(Team, Team)>()))
                             .Returns(new ValidationResult());

            // Setup for validation failure when home and away teams are the same
            mockTeamValidator.Setup(v => v.Validate(It.Is<(Team, Team)>(t => t.Item1.Country == t.Item2.Country)))
                             .Returns(new ValidationResult(new[] { new ValidationFailure("", "Home team cannot be the same as away team.") }));

            // Setup for validation failure when teams are of the same type
            mockTeamValidator.Setup(v => v.Validate(It.Is<(Team, Team)>(t => t.Item1.TeamType == t.Item2.TeamType)))
                             .Returns(new ValidationResult(new[] { new ValidationFailure("", "Team type cannot be the same type.") }));

            return mockTeamValidator;
        }
    }
}
