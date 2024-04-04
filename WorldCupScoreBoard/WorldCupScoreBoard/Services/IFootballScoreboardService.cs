using WorldCupScoreboard.Models;

namespace WorldCupScoreboard.Services
{
    public interface IFootballScoreboardService
    {
        public void StartMatch(Team homeTeam, Team awayTeam);

        public IEnumerable<Match> GetMatchesInProgress();

        public void UpdateScore(string homeTeamName, string awayTeamName, int homeScore, int awayScore);

        public void FinishMatch(string homeTeamName, string awayTeamName);
    }
}
