using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.DataTransferObjects
{
    public class HabitHistoryDto
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public int OwnerId { get; set; }
        public DateTime HabitHistoryDate { get; set; }
        public bool HabitHistoryResult { get; set; } 
        public int HabitHistoryNum { get; set; } 
    }
}
