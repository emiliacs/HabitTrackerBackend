using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesTodo { get; set; }
        public int DayRepeat { get; set; }
        public int TimeSpan { get; set; }
        public enum HabitCategory { Workout, Health, Hobby, Sleep, Drink }
        public HabitCategory Category { get; set; }
        public bool Favorite { get; set; }
        public bool PublicHabit { get; set; }

        public enum Weekdays { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }
        public Weekdays ChosenWeekDays { get; set; }
        public List<History> History;

        public User User { get; set; }
        public Group Group { get; set; }

    }
}
