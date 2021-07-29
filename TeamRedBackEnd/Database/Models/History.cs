using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamRedBackEnd.Database.Models
{
    public class History : IModelBase
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public int OwnerId { get; set; }
        public DateTime HabitHistoryDate { get; set; }
        public bool HabitHistoryResult { get; set; }
        public int HabitHistoryNum { get; set; }

        [ForeignKey("HabitId")]
        public Habit Habit { get; set; }

    }
}
