using System.ComponentModel.DataAnnotations;

namespace ScorecardAPI.Models
{
    public class Tournament
    {
        [Key]
        public int TournamentId { get; set; }
        public List<Match> Matches { get; set; } = new List<Match>();
        public List<User> Fighters { get; set; } = new List<User>();
    }
}
