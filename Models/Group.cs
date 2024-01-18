using System.ComponentModel.DataAnnotations;

namespace RateColleague.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "Sample group";
    }
}
