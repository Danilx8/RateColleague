using Quartz.Impl;
using Quartz;
using RateColleague.Jobs;
using RateColleague.Models;

namespace RateColleague.Services.RatingCollectionService
{
    public class RatingCollectionService : IRatingCollectionService
    {
        public async Task ScheduleJobAsync(int roomId, DateTime closingDate)
        {
            IJobDetail job = JobBuilder.Create<CollectRatingJob>()
                .WithIdentity(roomId.ToString(), "RoomsLifetime")
                .UsingJobData("RoomId", roomId)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(roomId.ToString(), "RoomsLifetime")
                .StartAt(closingDate)
                .ForJob(job.Key)
                .Build();

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task TerminateJob(int roomId)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.TriggerJob(new JobKey(roomId.ToString(), "RoomsLifeTime"));
        }
    }
}
