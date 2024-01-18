using Quartz.Impl;
using Quartz;
using RateColleague.Jobs;
using RateColleague.Models;

namespace RateColleague.Services.RatingCollectionService
{
    public class RatingCollectionService : IRatingCollectionService
    {
        public async Task ScheduleJobAsync(string roomUniqueSign, DateTime closingDate)
        {
            IJobDetail job = JobBuilder.Create<CollectRatingJob>()
                .WithIdentity(roomUniqueSign, "RoomsLifetime")
                .UsingJobData("RoomId", roomUniqueSign)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(roomUniqueSign, "RoomsLifetime")
                .StartAt(closingDate)
                .ForJob(job.Key)
                .Build();

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task TerminateJob(string roomUniqueSign)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.TriggerJob(new JobKey(roomUniqueSign, "RoomsLifeTime"));
        }
    }
}
