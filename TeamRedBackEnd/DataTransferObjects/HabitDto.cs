using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamRedBackEnd.Database.Models;
using TeamRedBackEnd.DataTransferObjects;

namespace TeamRedBackEnd.DataTransferObject
{
    public class HabitDto
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }
        public bool Favorite { get; set; }
        public bool PublicHabit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesTodo { get; set; }
        public List<HabitHistoryDto> history { get; set; }
    }
}
