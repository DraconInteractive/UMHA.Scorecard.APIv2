using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScorecardAPI.Models.DTO;
using ScorecardAPI.Models;
using System.Text.RegularExpressions;
using Match = ScorecardAPI.Models.Match;

namespace ScorecardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly ILogger<MatchesController> _logger;
        private readonly ScorecardContext _context;

        public MatchesController(ILogger<MatchesController> logger, ScorecardContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region Matches
        [HttpGet(Name = "GetMatches")]
        public ActionResult<IEnumerable<MatchOutputDTO>> Get()
        {
            var matches = _context.Matches
                                .Include(m => m.Tournament)
                                .Include(m => m.Events)
                                .ToList();
            var outputMatches = MatchOutputDTO.FromMatches(matches);
            outputMatches.Sort(delegate (MatchOutputDTO x, MatchOutputDTO y)
            {
                return x.Pool.CompareTo(y.Pool);
            });
            return Ok(outputMatches);
        }

        [HttpGet("{matchId}", Name = "GetMatch")]
        public async Task<ActionResult<MatchOutputDTO>> Get(int matchId)
        {
            var match = await _context.Matches.Include(m => m.Tournament).FirstOrDefaultAsync(x => x.MatchId == matchId);
            if (match == null)
            {
                return NotFound("No match with this ID");
            }
            else
            {
                return Ok(MatchOutputDTO.FromMatch(match));
            }
        }

        [HttpPost(Name = "CreateMatch")]
        public async Task<ActionResult<Match>> Post([FromBody] CreateMatchDTO createMatchDTO)
        {
            var newMatch = new Match
            {
                TournamentId = createMatchDTO.TournamentId,
                FighterOneId = createMatchDTO.FighterOneId,
                FighterTwoId = createMatchDTO.FighterTwoId,
                FighterOneHealth = 10,
                FighterTwoHealth = 10,
                Pool = createMatchDTO.Pool,
                Duration = 150,
                Status = MatchStatus.NotRunning
            };

            _context.Matches.Add(newMatch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new Match { TournamentId = newMatch.TournamentId }, newMatch);
        }

        [HttpPut("{matchId}/status", Name = "SetStatus")]
        public async Task<ActionResult> PutStatus(int matchId, [FromBody] MatchStatus newStatus)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == matchId);
            if (match == null)
            {
                return NotFound("No match with this Id");
            }
            else
            {
                if (match.Status == MatchStatus.NotRunning && newStatus != MatchStatus.NotRunning)
                {
                    match.StartTime = DateTime.Now.ToUniversalTime();
                }

                if (match.Status == MatchStatus.Finished || match.Status == MatchStatus.Cancelled)
                {
                    match.EndTime = DateTime.Now.ToUniversalTime();
                }
                match.Status = newStatus;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{matchId}", Name = "ModifyMatch")]
        public async Task<ActionResult<Match>> Put(int matchId, [FromBody] ModifyMatchDTO modifyMatchDTO)
        {
            var match = await _context.Matches.FindAsync(matchId);
            if (match == null)
            {
                return NotFound();
            }

            // Update user's properties
            match.TournamentId = modifyMatchDTO.TournamentId;
            match.FighterOneId = modifyMatchDTO.FighterOneId;
            match.FighterTwoId = modifyMatchDTO.FighterTwoId;
            match.Duration = modifyMatchDTO.Duration;
            match.FighterOneHealth = modifyMatchDTO.FighterOneHealth;
            match.FighterTwoHealth = modifyMatchDTO.FighterTwoHealth;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(matchId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(match); // Or you can return the updated user object
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }

        [HttpDelete("{matchId}", Name = "DeleteMatch")]
        public async Task<ActionResult> DeleteMatch(int matchId)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(x => x.MatchId == matchId);
            if (match == null)
            {
                return NotFound("No match with this ID");
            }
            else
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
        #endregion

        #region Events

        [HttpGet("{matchId}/events")]
        public async Task<ActionResult<MatchEventOutputDTO>> GetMatchEvents(int matchId)
        {
            var match = await _context.Matches.Include(m => m.Events).FirstOrDefaultAsync(m => m.MatchId == matchId);
            if (match == null)
            {
                return NotFound("Match not found");
            }

            return Ok(MatchEventOutputDTO.FromEvents(match.Events));
        }

        [HttpPost("{matchId}/events/strike")]
        public async Task<ActionResult> AddStrikeEvent(int matchId, [FromBody] StrikeEventDTO eventDTO)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == matchId);
            if (match == null)
            {
                return NotFound("Match not found");
            }

            var matchEvent = eventDTO.ToMatchEvent(matchId);
            if (matchEvent == null)
            {
                return BadRequest("Invalid event data");
            }

            if (matchEvent.ApplyHealthReduction)
            {
                match.FighterOneHealth -= matchEvent.FighterOneReduction;
                match.FighterTwoHealth -= matchEvent.FighterTwoReduction;
            }

            _context.MatchEvents.Add(matchEvent);
            //_context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok(MatchOutputDTO.FromMatch(match));
        }

        [HttpPost("{matchId}/events/penalty")]
        public async Task<ActionResult> AddPenaltyEvent(int matchId, [FromBody] PenaltyEventDTO eventDTO)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == matchId);
            if (match == null)
            {
                return NotFound("Match not found");
            }

            var matchEvent = eventDTO.ToMatchEvent(matchId);
            if (matchEvent == null)
            {
                return BadRequest("Invalid event data");
            }

            if (matchEvent.ApplyHealthReduction)
            {
                match.FighterOneHealth -= matchEvent.FighterOneReduction;
                match.FighterTwoHealth -= matchEvent.FighterTwoReduction;
            }

            _context.MatchEvents.Add(matchEvent);
            //_context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok(MatchOutputDTO.FromMatch(match));
        }

        [HttpPost("{matchId}/events/disqualify")]
        public async Task<ActionResult> AddDisqualificationEvent(int matchId, [FromBody] DisqualificationEventDTO eventDTO)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.MatchId == matchId);
            if (match == null)
            {
                return NotFound("Match not found");
            }

            var matchEvent = eventDTO.ToMatchEvent(matchId);
            if (matchEvent == null)
            {
                return BadRequest("Invalid event data");
            }

            if (matchEvent.ApplyHealthReduction)
            {
                match.FighterOneHealth -= matchEvent.FighterOneReduction;
                match.FighterTwoHealth -= matchEvent.FighterTwoReduction;
            }

            _context.MatchEvents.Add(matchEvent);
            //_context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok(MatchOutputDTO.FromMatch(match));
        }


        [HttpDelete("{matchId}/events/{eventId}", Name = "DeleteEvent")]
        public async Task<ActionResult> DeleteEvent(int matchId, int eventId)
        {
            var matchEvent = await _context.MatchEvents
                                   .FirstOrDefaultAsync(e => e.MatchId == matchId && e.MatchEventId == eventId);
            if (matchEvent == null)
            {
                return NotFound("No event with this ID for the specified match");
            }

            _context.MatchEvents.Remove(matchEvent);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}
