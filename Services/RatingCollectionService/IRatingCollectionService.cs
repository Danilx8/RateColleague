namespace RateColleague.Services.RatingCollectionService
{
    public interface IRatingCollectionService
    {
        public Task ScheduleJobAsync(int roomId, DateTime closingDate);

        public Task TerminateJob(int roomId);
    }
}
