using System.ComponentModel.DataAnnotations;

namespace RateColleague.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "Title";
        public string Text { get; set; } = "Your question";

        public required Room Room { get; set; }
        public Group? Group { get; set; }
        public List<Grade> Grades { get; set; } = [];
    }
}
