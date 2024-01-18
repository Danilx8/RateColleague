namespace RateColleague.Services.RatingCollectionService
{
    public interface IRatingCollectionService
    {
        public Task ScheduleJobAsync(string roomUniqueSign, DateTime closingDate);

        public Task TerminateJob(string roomUniqueSign);
    }
}
