using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScorecardAPI.Models;
using ScorecardAPI.Models.DTO;

namespace ScorecardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ScorecardContext _context;

        public UsersController(ILogger<UsersController> logger, ScorecardContext context) 
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<User>> Get (int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                return NotFound("No user with this ID");
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<User>> Post([FromBody] CreateUserDTO createUserDTO)
        {
            var newUser = new User
            {
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                // Map other properties from DTO to User model
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new User { UserId = newUser.UserId }, newUser);
        }

        [HttpPost("batch", Name = "CreateUsers")]
        public async Task<ActionResult> Post([FromBody] List<CreateUserDTO> createUsersDTO)
        {
            List<User> created = new List<User>();
            foreach (var user in createUsersDTO)
            {
                var c = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                created.Add(c);
            }
            _context.Users.AddRange(created);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{userId}", Name = "ModifyUser")]
        public async Task<ActionResult<User>> Put(int userId, [FromBody] ModifyUserDTO modifyUserDTO)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Update user's properties
            user.FirstName = modifyUserDTO.FirstName;
            user.LastName = modifyUserDTO.LastName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user); // Or you can return the updated user object
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                return NotFound("No user with this ID");
            }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
