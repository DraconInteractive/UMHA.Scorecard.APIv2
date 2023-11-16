namespace ScorecardAPI.Models.DTO
{
    public class CreateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ModifyUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class UserOutputDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int UserId { get; set; }
        public List<int>? Tournaments { get; set; }

        public static UserOutputDTO FromUser (User user)
        {
            var listT = new List<int>();
            foreach (var tournament in user.Tournaments)
            {
                listT.Add(tournament.TournamentId);
            }

            return new UserOutputDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.UserId,
                Tournaments = listT
            };
        }

        public static List<UserOutputDTO> FromUsers (List<User> users)
        {
            var list = new List<UserOutputDTO>();
            foreach (var user in users)
            {
                list.Add(FromUser(user));
            }
            return list;
        }
    }
}