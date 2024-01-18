using System.ComponentModel.DataAnnotations;

namespace RateColleague.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; } = 0;

        public required Question Question { get; set; }
    }
}
