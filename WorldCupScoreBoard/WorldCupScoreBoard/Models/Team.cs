using System.ComponentModel.DataAnnotations;

namespace WorldCupScoreboard.Models
{
    public class Team
    {
        [Required]
        public string Country { get; }
        [Required]
        public TeamType TeamType { get; }

        public Team(string country, TeamType teamType)
        {
            Country = country;
            TeamType = teamType;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Team other = (Team)obj;
            return Country.Equals(other.Country) && TeamType.Equals(other.TeamType);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Country, TeamType);
        }
    }
}