using System.ComponentModel.DataAnnotations;

namespace RateColleague.Models
{
    public class RegistrationDto
    {
        public required string Name { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmedPassword { get; set; }
    }
}
