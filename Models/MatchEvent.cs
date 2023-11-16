using System.ComponentModel.DataAnnotations;

namespace ScorecardAPI.Models
{
    public class MatchEvent
    {
        [Key]
        public int MatchEventId { get; set; }
        public int MatchId { get; set; }
        public Match? Match { get; set; }
        public bool ApplyHealthReduction { get; set; }
        public string? EventType { get; set; }
        public DateTime EventTime { get; set; }

        public int FighterOneReduction { get; set; }
        public int FighterTwoReduction { get; set; }
    }

    public class StrikeEvent : MatchEvent
    {

    }

    public class PenaltyEvent : MatchEvent
    {
        public string? Reason { get; set; }
    }

    public class DisqualificationEvent : MatchEvent
    {
        public int UserId { get; set; }
        public string? Reason { get; set; }
    }
}
