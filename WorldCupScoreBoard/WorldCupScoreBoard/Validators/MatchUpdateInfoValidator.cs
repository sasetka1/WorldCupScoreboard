using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupScoreboard.Models;

namespace WorldCupScoreboard.Validators
{
    public class MatchUpdateInfoValidator : AbstractValidator<MatchUpdateInfo>
    {
        public MatchUpdateInfoValidator()
        {
            RuleFor(match => match.HomeTeam)
                .NotNull().WithMessage("Home team is required.");

            RuleFor(match => match.AwayTeam)
                .NotNull().WithMessage("Away team is required.");

            RuleFor(match => match.HomeScore)
               .GreaterThanOrEqualTo(0).WithMessage("Home score cannot be negative.");

            RuleFor(match => match.AwayScore)
               .GreaterThanOrEqualTo(0).WithMessage("Home score cannot be negative.");
        }
    }

}
