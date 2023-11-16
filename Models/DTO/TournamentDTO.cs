using System.ComponentModel.DataAnnotations;

namespace ScorecardAPI.Models.DTO
{
    public class CreateTournamentDTO
    {
        public int GenerationMethod { get; set; }
        public int Pools { get; set; }
        public int[]? Fighters { get; set; }
    }

    public class CreateTournamentCompleteDTO
    {
        public int TournamentId { get; set; }
        public List<MatchOutputDTO>? Matches { get; set; }
    }

    public class TournamentOutputDTO
    {
        public class UserResult
        {
            public string UserName { get; set; }
            public int wins { get; set; }
            public int losses { get; set; }
            public int draws { get; set; }
            public int sumRemainingHealth { get; set; }
        }

        public int TournamentId { get; set; }
        public List<MatchOutputDTO> Matches { get; set; } = new List<MatchOutputDTO>(); // Be careful with this. Its not quite cyclical, but its close.
        public List<UserOutputDTO> Fighters { get; set; } = new List<UserOutputDTO>();
        public bool Finished { get; set; }
        public Dictionary<int, UserResult> results { get; set; } = new Dictionary<int, UserResult>();
        public List<int>? DisqualifiedFighters { get; set; } = new List<int>();

        public static TournamentOutputDTO FromTournament (Tournament tournament)
        {
            var nt = new TournamentOutputDTO();
            var matchList = new List<MatchOutputDTO>();
            var userList = new List<UserOutputDTO>();
            nt.TournamentId = tournament.TournamentId;
            foreach (var match in tournament.Matches)
            {
                matchList.Add(MatchOutputDTO.FromMatch(match));
            }
            foreach (var user in tournament.Fighters)
            {
                userList.Add(UserOutputDTO.FromUser(user));
            }
            nt.Matches = matchList;
            nt.Fighters = userList;
            nt.results = GetResults(tournament);
            nt.Finished = matchList.All(m => m.Status == MatchStatus.Finished || m.Status == MatchStatus.Cancelled);

            var dis = new List<int>();
            foreach (var match in tournament.Matches)
            {
                foreach (var evt in match.Events)
                {
                    if (evt is DisqualificationEvent dEvt)
                    {
                        if (!dis.Contains(dEvt.UserId))
                        {
                            dis.Add(dEvt.UserId);
                        }
                    }
                }
            }
            nt.DisqualifiedFighters = dis;
            return nt;
        }

        public static List<TournamentOutputDTO> FromTournaments (List<Tournament> tournaments)
        {
            var l = new List<TournamentOutputDTO>();
            foreach (var tournament in tournaments)
            {
                l.Add(FromTournament(tournament));
            }
            return l;
        }

        public static Dictionary<int, UserResult> GetResults(Tournament tournament)
        {
            var newResults = new Dictionary<int, UserResult>();
            foreach (var fighter in tournament.Fighters)
            {
                newResults.Add(fighter.UserId, new UserResult()
                {
                    UserName = fighter.FirstName + " " + fighter.LastName
                });
            }

            foreach (var match in tournament.Matches)
            {
                newResults[match.FighterOneId].sumRemainingHealth += match.FighterOneHealth;
                newResults[match.FighterTwoId].sumRemainingHealth += match.FighterTwoHealth;

                if (match.FighterOneHealth > match.FighterTwoHealth)
                {
                    newResults[match.FighterOneId].wins++;
                    newResults[match.FighterTwoId].losses++;
                }
                else if (match.FighterOneHealth < match.FighterTwoHealth)
                {
                    newResults[match.FighterOneId].losses++;
                    newResults[match.FighterTwoId].wins++;
                }
                else
                {
                    newResults[match.FighterOneId].draws++;
                    newResults[match.FighterTwoId].draws++;
                }
            }

            return newResults;
        }
    }
}
