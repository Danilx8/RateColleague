namespace RateColleague.Models
{
    public class QuestionDto
    {
        public required string Title { get; set; }
        public required string Text { get; set; }
        public int GroupId { get; set; }
    }
}
