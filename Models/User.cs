using System.ComponentModel.DataAnnotations;

namespace ScorecardAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
