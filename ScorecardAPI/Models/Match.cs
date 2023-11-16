using System.ComponentModel.DataAnnotations;

namespace ScorecardAPI.Models
{
    public enum MatchStatus
    {
        NotRunning = 0,
        Running = 1,
        Cancelled = 2, 
        Finished = 3
    }

    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        public int TournamentId { get; set; }
        public int FighterOneId { get; set; }
        public int FighterTwoId { get;set; }
        public int FighterOneHealth { get; set; }
        public int FighterTwoHealth { get; set; }
        public int Pool { get; set; }
        public int Duration { get; set; }
        public MatchStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<MatchEvent> Events { get; set; } = new List<MatchEvent>();
        public Tournament? Tournament { get; set; }
    }
}
