﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateColleague.Data;
using RateColleague.Models;
using RateColleague.Services.RatingCollectionService;
using System.Security.Claims;

namespace RateColleague.Controllers
{
    [ApiController]
    public class RoomController(ApplicationDbContext _db,
        IRatingCollectionService _rating) : Controller
    {
        protected readonly ApplicationDbContext db = _db;
        private readonly IRatingCollectionService rating = _rating;

        [HttpGet]
        [Route("room/{signature}")]
        public IActionResult Retrieve(string signature, [FromQuery] Pagination filter)
        {
            Room room;

            try
            {
                room = db
                    .Rooms
                    .Where(r => r.UniqueSign == signature)
                    .Include(r => r.RatedColleague)
                    .Include(r => r.Initiator)
                    .FirstOrDefault()
                    ?? throw new BadHttpRequestException("Room by given id doesn't exist");
            }
            catch (BadHttpRequestException err)
            {
                return NotFound(err.Message);
            }

            List<Question> questions = [.. db
                .Questions
                .Where(q => q.Room == room)
                .Skip((filter.PageNum - 1) * filter.Limit)
                .Take(filter.Limit)];

            return Ok((questions, room));
        }

        [Route("")]
        [HttpPost]
        public IActionResult Rate(Grade[] grades)
        {
            db.Grades.AddRange(grades);
            return Ok();
        }

        [Route("new")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RoomDto room)
        {
            string code;
            while (true)
            {
                code = RandomString();
                if (db
                    .Rooms
                    .Where(r => r.UniqueSign == code)
                    .FirstOrDefault() == default) break;
            }

            if (room.ClosePollTime <= DateTime.UtcNow)
            {
                return BadRequest("Close time has to be in the future");
            }

            var initiatorId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            Employee initiator = db.Users.Find(initiatorId.Value)!;

            RatedColleague colleague = new()
            {
                Name = room.RatedColleagueName,
                Position = room.RatedColleaguePosition
            };

            db.RatedColleagues.Add(colleague);

            Room generatedRoom = new()
            {
                Name = room.Name,
                UniqueSign = code,
                PublicPassword = room.Password,
                ClosePollTime = room.ClosePollTime,
                RatedColleague = colleague,
                InitiatorId = initiator.Id,
                Initiator = initiator
            };

            db.Rooms.Add(generatedRoom);

            List<Question> generatedQuestions = new(room.Questions.Select
                (q => new Question()
                {
                    Title = q.Title,
                    Text = q.Text,
                    Room = generatedRoom
                }));

            db.Questions.AddRange(generatedQuestions);

            try
            {
                await rating.ScheduleJobAsync(generatedRoom.UniqueSign, room.ClosePollTime);
            }
            finally
            {
                db.SaveChanges();
            }

            return Ok(generatedRoom);
        }

        [Route("/kill/{id}")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TerminateAsync(string sign)
        {
            await rating.TerminateJob(sign);
            return Ok("Success");
        }

        private static string RandomString(int length = 4)
        {
            Random random = new();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
