using FluentValidation;
using WorldCupScoreboard.Models;

namespace WorldCupScoreboard.Validators
{
    public class TeamValidator : AbstractValidator<(Team, Team)>
    {
        public TeamValidator()
        {
            RuleFor(x => x.Item1)
                .NotNull()
                .WithMessage("Home team cannot be null.");

            RuleFor(x => x.Item2)
                .NotNull()
                .WithMessage("Away team cannot be null.");

            RuleFor(x => x.Item1.Country)
                .NotEmpty()
                .WithMessage("Country name cannot be empty.")
                .When(x => x.Item1 != null && x.Item2 != null); ;

            RuleFor(x => x.Item2.Country)
                .NotEmpty()
                .WithMessage("Country name cannot be empty.")
                .When(x => x.Item1 != null && x.Item2 != null); 

            RuleFor(x => x.Item1.Country)
                .NotEqual(x => x.Item2.Country)
                .WithMessage("Home team cannot be the same as away team.")
                .When(x => x.Item1 != null && x.Item2 != null); ;

            RuleFor(x => x.Item1.TeamType)
                .NotEqual(x => x.Item2.TeamType)
                .WithMessage("Team type cannot be the same type.")
                .When(x => x.Item1 != null && x.Item2 != null); 

        }
    }
}
