using System.ComponentModel.DataAnnotations;

namespace WorldCupScoreboard.Models
{
    public class Match
    {
        [Required]
        public Team HomeTeam { get; }
        [Required]
        public Team AwayTeam { get; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; } = 0;

        public readonly DateTime StartTime;

        public Match(Team homeTeam, Team awayTeam, int homeScore = 0, int awayScore = 0)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            StartTime = DateTime.Now;
            HomeScore = homeScore;
            AwayScore = awayScore;

        }

        public void UpdateScore(int homeScore, int awayScore)
        {
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public int TotalScore()
        {
            return HomeScore + AwayScore;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Match other = (Match)obj;
            return HomeTeam.Equals(other.HomeTeam) && AwayTeam.Equals(other.AwayTeam) &&
                   HomeScore == other.HomeScore && AwayScore == other.AwayScore;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HomeTeam, AwayTeam, HomeScore, AwayScore);
        }
    }
}
