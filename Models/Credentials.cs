using System.ComponentModel.DataAnnotations;

namespace RateColleague.Models
{
    public class Credentials
    {
        [MaxLength(30)]
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [MinLength(6)]
        public required string Password { get; set; }
    }
}
