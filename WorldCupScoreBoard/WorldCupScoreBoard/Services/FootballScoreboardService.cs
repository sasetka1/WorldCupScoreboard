using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WorldCupScoreboard.Models;
using WorldCupScoreboard.Validators;

namespace WorldCupScoreboard.Services
{
    public class FootballScoreboardService: IFootballScoreboardService
    {
        private List<Match> _matchesInProgress = new List<Match>();
        private readonly IValidator<(Team, Team)> _teamValidator;
        private readonly IValidator<MatchUpdateInfo> _matchValidator;

        public FootballScoreboardService(IValidator<(Team, Team)> validator, IValidator<MatchUpdateInfo> matchValidator)
        {
            _teamValidator = validator;
            _matchValidator = matchValidator;
        }

        public void StartMatch(Team homeTeam, Team awayTeam)
        {
            var validationResult =  _teamValidator.Validate((homeTeam, awayTeam));

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            _matchesInProgress.Add(new Match(homeTeam, awayTeam));
        }

        public IEnumerable<Match> GetMatchesInProgress()
        {
            return _matchesInProgress.OrderByDescending(m => m.TotalScore()).ThenByDescending(m => m.StartTime);
        }

        public void UpdateScore(MatchUpdateInfo matchUpdateInfo)
        {
            var validationResult = _matchValidator.Validate((matchUpdateInfo));

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var match = _matchesInProgress.Find(m => m.HomeTeam.Country == matchUpdateInfo.HomeTeam && m.AwayTeam.Country == matchUpdateInfo.AwayTeam);

            if (match != null)
            {
                match.UpdateScore(matchUpdateInfo.HomeScore, matchUpdateInfo.AwayScore);
            }
            else
            {
                throw new InvalidOperationException("Match not found.");
            }
        }

        public void FinishMatch(MatchUpdateInfo matchUpdateInfo)
        {
            var matchToRemove = _matchesInProgress.FirstOrDefault(match =>
                  match.HomeTeam.Country == matchUpdateInfo.HomeTeam && match.AwayTeam.Country == matchUpdateInfo.AwayTeam);

            if (matchToRemove == null)
            {
                throw new ArgumentException("Match not found.");
            }

            _matchesInProgress.Remove(matchToRemove);

        }
    }
}
