using WorldCupScoreboard.Models;

namespace WorldCupScoreboard.Services
{
    public interface IFootballScoreboardService
    {
        public void StartMatch(Team homeTeam, Team awayTeam);

        public IEnumerable<Match> GetMatchesInProgress();

        public void UpdateScore(MatchUpdateInfo matchUpdateInfo);

        public void FinishMatch(MatchUpdateInfo matchUpdateInfo);
    }
}
