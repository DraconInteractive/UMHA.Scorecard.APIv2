using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScorecardAPI.Models;
using ScorecardAPI.Models.DTO;

namespace ScorecardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        private readonly ScorecardContext _context;

        public TournamentController(ILogger<TournamentController> logger, ScorecardContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetTournaments")]
        public async Task<ActionResult<IEnumerable<TournamentOutputDTO>>> Get()
        {
            var tournamentContext = await _context.Tournaments
                .Include(t => t.Fighters)
                .Include(t => t.Matches)
                    .ThenInclude(m => m.Events)
                .ToListAsync();


            var outputs = TournamentOutputDTO.FromTournaments(tournamentContext);
            return Ok(outputs);
        }

        [HttpGet("{tournamentId}", Name = "GetTournament")]
        public async Task<ActionResult<TournamentOutputDTO>> Get(int tournamentId)
        {
            var tournamentContext = await _context.Tournaments.Include(t => t.Matches).Include(t => t.Fighters).FirstOrDefaultAsync(x => x.TournamentId == tournamentId);
            if (tournamentContext == null)
            {
                return NotFound("No tournament with this ID");
            }
            else
            {
                return Ok(TournamentOutputDTO.FromTournament(tournamentContext));
            }
        }

        [HttpPost(Name = "CreateTournament")]
        public async Task<ActionResult<CreateTournamentCompleteDTO>> Post([FromBody] CreateTournamentDTO createTournamentDTO)
        {
            if (createTournamentDTO.Fighters == null)
            {
                return BadRequest("No fighter objects supplied");
            }

            var newTournament = new Tournament();

            var fighterEntities = await _context.Users
                                            .Where(u => createTournamentDTO.Fighters.Contains(u.UserId))
                                            .ToListAsync();

            foreach (var fighter in fighterEntities)
            {
                newTournament.Fighters.Add(fighter);
            }

            _context.Tournaments.Add(newTournament);
            await _context.SaveChangesAsync();

            int gen = createTournamentDTO.GenerationMethod;
            if (gen == 0) // Dont generate matches
            {
                
            }
            else if (gen == 1) // Round robin
            {
                int numberOfFighters = createTournamentDTO.Fighters.Length;
                int numberOfPools = createTournamentDTO.Pools;
                if (numberOfPools == 0)
                {
                    numberOfPools = 1;
                }
                int baseFightersPerPool = numberOfFighters / numberOfPools;
                int extraFighters = numberOfFighters % numberOfPools; // Fighters that don't fit evenly into pools

                int fighterIndex = 0;

                for (int pool = 0; pool < numberOfPools; pool++)
                {
                    int fightersInThisPool = baseFightersPerPool + (pool < extraFighters ? 1 : 0);
                    for (int i = 0; i < fightersInThisPool; i++)
                    {
                        for (int j = i + 1; j < fightersInThisPool; j++)
                        {
                            int f1Index = fighterIndex + i;
                            int f2Index = fighterIndex + j;

                            var f1ID = createTournamentDTO.Fighters[f1Index];
                            var f2ID = createTournamentDTO.Fighters[f2Index];

                            if (f1ID == f2ID)
                            {
                                continue; // Skip matching the same fighter
                            }

                            var match = new Match
                            {
                                TournamentId = newTournament.TournamentId,
                                FighterOneId = f1ID,
                                FighterTwoId = f2ID,
                                FighterOneHealth = 10,
                                FighterTwoHealth = 10,
                                Duration = 150,
                                Pool = pool + 1 // Pool numbering starts at 1
                            };

                            newTournament.Matches.Add(match);
                        }
                    }
                }
                /*
                for (int i = 0; i < createTournamentDTO.Fighters.Length; i++)
                {
                    for (int j = i + 1; j < createTournamentDTO.Fighters.Length; j++)
                    {
                        var f1ID = createTournamentDTO.Fighters[i];
                        var f2ID = createTournamentDTO.Fighters[j];
                        if (f1ID == f2ID)
                        {
                            continue; // Don't match against the same fighter
                        }

                        var match = new Match
                        {
                            TournamentId = newTournament.TournamentId,
                            FighterOneId = f1ID,
                            FighterTwoId = f2ID,
                            FighterOneHealth = 10,
                            FighterTwoHealth = 10,
                            Duration = 150
                        };

                        newTournament.Matches.Add(match);
                    }
                }
                */
                await _context.SaveChangesAsync();
            }

            var completeDTO = new CreateTournamentCompleteDTO()
            {
                TournamentId = newTournament.TournamentId,
                Matches = MatchOutputDTO.FromMatches(newTournament.Matches)
            };

            return CreatedAtAction(nameof(Post), new { newTournament.TournamentId }, completeDTO);
        }

        [HttpDelete("{tournamentId}", Name = "DeleteTournament")]
        public async Task<ActionResult> Delete (int tournamentId)
        {
            var t = await _context.Tournaments.FirstOrDefaultAsync(x => x.TournamentId == tournamentId);
            if (t == null)
            {
                return NotFound("No tournament with this ID");
            }
            else
            {
                _context.Tournaments.Remove(t);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
