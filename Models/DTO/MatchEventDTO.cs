namespace ScorecardAPI.Models.DTO
{
    public class MatchEventDTO
    {
        public int FighterOneReduction { get; set; }
        public int FighterTwoReduction { get; set; }
        public bool ApplyHealthReduction { get; set; }
        public MatchEvent ToMatchEvent (int matchId)
        {
            return new MatchEvent
            {
                MatchId = matchId,
                EventType = "CoreEvent",
                EventTime = DateTime.Now.ToUniversalTime(),
                FighterOneReduction = FighterOneReduction,
                FighterTwoReduction = FighterTwoReduction,
                ApplyHealthReduction = ApplyHealthReduction
            };
        }
    }

    public class StrikeEventDTO : MatchEventDTO
    {
        new public StrikeEvent ToMatchEvent(int matchId)
        {
            return new StrikeEvent
            {
                MatchId = matchId,
                EventType = "Strike",
                EventTime = DateTime.Now.ToUniversalTime(),
                FighterOneReduction = FighterOneReduction,
                FighterTwoReduction = FighterTwoReduction,
                ApplyHealthReduction = ApplyHealthReduction
            };
        }
    }

    public class PenaltyEventDTO : MatchEventDTO
    {
        public string? Reason { get; set; }

        new public PenaltyEvent ToMatchEvent(int matchId)
        {
            return new PenaltyEvent
            {
                MatchId = matchId,
                EventType = "Penalty",
                EventTime = DateTime.Now.ToUniversalTime(),
                FighterOneReduction = FighterOneReduction,
                FighterTwoReduction = FighterTwoReduction,
                ApplyHealthReduction = ApplyHealthReduction,
                Reason = Reason
            };
        }
    }

    public class DisqualificationEventDTO : MatchEventDTO
    {
        public int UserId { get; set; }
        public string? Reason { get; set; }

        new public DisqualificationEvent ToMatchEvent(int matchId)
        {
            return new DisqualificationEvent
            {
                MatchId = matchId,
                EventType = "Disqualification",
                EventTime = DateTime.Now.ToUniversalTime(),
                FighterOneReduction = FighterOneReduction,
                FighterTwoReduction = FighterTwoReduction,
                ApplyHealthReduction = ApplyHealthReduction,
                Reason = Reason,
                UserId = UserId
            };
        }
    }

    public class MatchEventOutputDTO
    {
        public int MatchId { get; set; }
        public int EventId { get; set; }
        public string? EventType { get; set; }
        public string? EventTime { get; set; }
        public int FighterOneReduction { get; set; }
        public int FighterTwoReduction { get; set; }

        public static MatchEventOutputDTO FromEvent(MatchEvent matchEvent)
        {
            switch (matchEvent)
            {
                case StrikeEvent strikeEvent:
                    return new StrikeEventOutputDTO()
                    {
                        MatchId = matchEvent.MatchId,
                        EventId = matchEvent.MatchEventId,
                        EventType = matchEvent.EventType,
                        EventTime = matchEvent.EventTime.ToLongTimeString(),
                        FighterOneReduction = matchEvent.FighterOneReduction,
                        FighterTwoReduction = matchEvent.FighterTwoReduction,
                    };
                case PenaltyEvent penaltyEvent:
                    return new PenaltyEventOutputDTO()
                    {
                        MatchId = matchEvent.MatchId,
                        EventId = matchEvent.MatchEventId,
                        EventType = matchEvent.EventType,
                        EventTime = matchEvent.EventTime.ToLongTimeString(),
                        FighterOneReduction = matchEvent.FighterOneReduction,
                        FighterTwoReduction = matchEvent.FighterTwoReduction,
                        Reason = penaltyEvent.Reason
                    };
                case DisqualificationEvent disqualificationEvent:
                    return new DisqualificationEventOutputDTO()
                    {
                        MatchId = matchEvent.MatchId,
                        EventId = matchEvent.MatchEventId,
                        EventType = matchEvent.EventType,
                        EventTime = matchEvent.EventTime.ToLongTimeString(),
                        FighterOneReduction = matchEvent.FighterOneReduction,
                        FighterTwoReduction = matchEvent.FighterTwoReduction,
                        UserId = disqualificationEvent.UserId,
                        Reason = disqualificationEvent.Reason
                    };
                default:
                    return new MatchEventOutputDTO()
                    {
                        MatchId = matchEvent.MatchId,
                        EventId = matchEvent.MatchEventId,
                        EventType = matchEvent.EventType,
                        EventTime = matchEvent.EventTime.ToLongTimeString(),
                        FighterOneReduction = matchEvent.FighterOneReduction,
                        FighterTwoReduction = matchEvent.FighterTwoReduction,
                    };
            }
        }

        public static List<MatchEventOutputDTO> FromEvents(List<MatchEvent> matchEvents)
        {
            var l = new List<MatchEventOutputDTO>();
            foreach (var evt in matchEvents)
            {
                l.Add(FromEvent(evt));
            }
            return l;
        }
    }

    public class StrikeEventOutputDTO : MatchEventOutputDTO
    {
        
    }

    public class PenaltyEventOutputDTO : MatchEventOutputDTO
    {
        public string? Reason { get; set; }
    }

    public class DisqualificationEventOutputDTO : MatchEventOutputDTO
    {
        public int UserId { get; set; }
        public string? Reason { get; set; }
    }

}
