using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateColleague.Models
{
    [Index("UniqueSign", IsUnique = true)]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [BindNever]
        [ValidateNever]
        public required string UniqueSign { get; set; }
        public required string Name { get; set; } = "Room";
        public required string PublicPassword { get; set; }
        public required DateTime ClosePollTime { get; set; }
        public required RatedColleague RatedColleague { get; set; }
        [BindNever]
        public required string InitiatorId { get; set; }
        [BindNever]
        [ForeignKey("InitiatorId")]
        public required Employee Initiator { get; set; }
    }
}
