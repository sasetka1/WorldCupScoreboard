using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WorldCupScoreboard.Models;
using WorldCupScoreboard.Validators;

namespace WorldCupScoreboard.Services
{
    public class FootballScoreboardService: IFootballScoreboardService
    {
        private List<Match> _matchesInProgress = new List<Match>();
        private readonly IValidator<(Team, Team)> _validator;

        public FootballScoreboardService(IValidator<(Team, Team)> validator)
        {
            _validator = validator;
        }

        public void StartMatch(Team homeTeam, Team awayTeam)
        {
            var validationResult =  _validator.Validate((homeTeam, awayTeam));

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            _matchesInProgress.Add(new Match(homeTeam, awayTeam));
        }


        // _validator.ValidateAndThrow(homeTeam);
        // _validator.ValidateAndThrow(awayTeam);
        /*            if (homeTeam.Country == awayTeam.Country)
                    {
                        throw new ArgumentException("Home team cannot be the same as away team.");
                    }

                    if (homeTeam.TeamType == awayTeam.TeamType)
                    {
                        throw new ArgumentException("Team type cannot be the same type.");
                    }*/
        public IEnumerable<Match> GetMatchesInProgress()
        {
            return _matchesInProgress.OrderByDescending(m => m.TotalScore()).ThenByDescending(m => m.StartTime);
        }

        public void UpdateScore(string homeTeamName, string awayTeamName, int homeScore, int awayScore)
        {
            var match = _matchesInProgress.Find(m => m.HomeTeam.Country == homeTeamName && m.AwayTeam.Country == awayTeamName);
            if (match != null)
            {
                match.UpdateScore(homeScore, awayScore);
            }
            else
            {
                throw new InvalidOperationException("Match not found.");
            }
        }

        public void FinishMatch(string homeTeamName, string awayTeamName)
        {
            var matchToRemove = _matchesInProgress.FirstOrDefault(match =>
                  match.HomeTeam.Country == homeTeamName && match.AwayTeam.Country == awayTeamName);

            if (matchToRemove == null)
            {
                throw new ArgumentException("Match not found.");
            }

            _matchesInProgress.Remove(matchToRemove);

        }
    }
}
