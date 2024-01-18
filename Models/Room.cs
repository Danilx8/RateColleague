using System.ComponentModel.DataAnnotations;

namespace RateColleague.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; } = "Room";
        public required string PublicPassword { get; set; }
        public required RatedColleague RatedColleague { get; set; }

        public List<Question> Questions { get; set; } = [];
    }
}
