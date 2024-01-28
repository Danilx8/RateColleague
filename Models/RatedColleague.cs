using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateColleague.Models
{
    public class RatedColleague
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "No name";
        public string Position { get; set; } = "No position";
    }
}
