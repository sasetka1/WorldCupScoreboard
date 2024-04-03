namespace WorldCupScoreboard.Models
{
    public class Team
    {

        public string Country { get; }
        public TeamType TeamType { get; }

        public Team(string country, TeamType teamType)
        {
            Country = country;
            TeamType = teamType;
        }

    }
}