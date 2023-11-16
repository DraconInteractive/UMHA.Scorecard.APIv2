using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ScorecardAPI.Models.DTO
{
    public class CreateMatchDTO
    {
        public int TournamentId { get; set; }
        public int FighterOneId { get; set; }
        public int FighterTwoId { get; set; }
        public int Duration { get; set; }
        public int Pool { get; set; }
    }

    public class ModifyMatchDTO
    {
        public int TournamentId { get; set; }
        public int FighterOneId { get; set; }
        public int FighterTwoId { get; set; }
        public int FighterOneHealth { get; set; }
        public int FighterTwoHealth { get; set; }
        public int Duration { get; set; }

    }

    public class MatchOutputDTO
    {
        public int MatchId { get; set; }
        public int TournamentId { get; set; }
        public int FighterOneId { get; set; }
        public int FighterTwoId { get; set; }
        public int FighterOneHealth { get; set; }
        public int FighterTwoHealth { get; set; }
        public int Pool { get; set; }
        public int Duration { get; set; }
        public string? Started {  get; set; }
        public string? Ended { get; set; }
        public MatchStatus Status { get; set; }

        public static List<MatchOutputDTO> FromMatches(List<Match> matches)
        {
            var newList = new List<MatchOutputDTO>();
            foreach (Match match in matches)
            {
                newList.Add(FromMatch(match));
            }
            return newList;
        }

        public static MatchOutputDTO FromMatch (Match match)
        {
            return new MatchOutputDTO()
            {
                MatchId = match.MatchId,
                TournamentId = match.TournamentId,
                FighterOneId = match.FighterOneId,
                FighterTwoId = match.FighterTwoId,
                FighterOneHealth = match.FighterOneHealth,
                FighterTwoHealth = match.FighterTwoHealth,
                Pool = match.Pool,
                Status = match.Status,
                Duration = match.Duration,
                Started = match.StartTime.ToString(new CultureInfo("en-au")),
                Ended = match.EndTime.ToString(new CultureInfo("en-au"))
            };
        }
    }
}
