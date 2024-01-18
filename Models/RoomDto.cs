namespace RateColleague.Models
{
    public class RoomDto
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required DateTime ClosePollTime { get; set; }
        public required string RatedColleagueName { get; set; }
        public required string RatedColleaguePosition { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
