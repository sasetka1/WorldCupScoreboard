
using WorldCupScoreboard.Models;

namespace WorldCupScoreboard
{
    public class FootballScoreboard
    {
        private List<Match> _matchesInProgress = new List<Match>();
        public void StartMatch(Team homeTeam, Team awayTeam)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetMatchesInProgress()
        {
            return _matchesInProgress.OrderByDescending(m => m.StartTime);
        }
    }
}
