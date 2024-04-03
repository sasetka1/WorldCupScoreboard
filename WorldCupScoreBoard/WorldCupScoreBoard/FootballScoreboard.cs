
using WorldCupScoreboard.Models;

namespace WorldCupScoreboard
{
    public class FootballScoreboard
    {
        private List<Match> _matchesInProgress = new List<Match>();
        public void StartMatch(Team homeTeam, Team awayTeam)
        {
            if (homeTeam.Country == awayTeam.Country)
            {
                throw new ArgumentException("Home team cannot be the same as away team.");
            }

            if (homeTeam.TeamType == awayTeam.TeamType)
            {
                throw new ArgumentException("Team type cannot be the same type.");
            }

            _matchesInProgress.Add(new Match(homeTeam, awayTeam));
        }

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
            throw new NotImplementedException();
        }
    }
}
