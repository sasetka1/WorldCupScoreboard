namespace WorldCupScoreboard.Models
{
    public class Match
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }

        public readonly System.DateTime StartTime;
    }
}
