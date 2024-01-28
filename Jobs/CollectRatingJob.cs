using Microsoft.EntityFrameworkCore;
using Quartz;
using RateColleague.Data;
using RateColleague.Models;
using RateColleague.Services.EmailSenderService;

namespace RateColleague.Jobs
{
    public class CollectRatingJob(ApplicationDbContext _db,
        EmailSenderService _emailSender) : IJob
    {
        private readonly ApplicationDbContext db = _db;
        private readonly EmailSenderService emailSender = _emailSender;

        public int RoomId { private get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Room room = await db
                    .Rooms
                    .Where(r => r.Id == RoomId)
                    .Include(r => r.Questions)
                        .ThenInclude(q => q.Grades)
                    .FirstAsync();
                await db.Groups.LoadAsync();

                emailSender.SendEmailAsync(room.Initiator.Email ?? throw new UnauthorizedAccessException());
            } catch(Exception)
            {
                await Console.Error.WriteLineAsync($"Room with id {RoomId} couldn't collect rating");
            }
        }
    }
}
