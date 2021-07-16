using System.ComponentModel.DataAnnotations.Schema;

namespace TeamRedBackEnd.Database.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int HabitID { get; set; }

        public int[] UserIds { get; set; } 

        [ForeignKey("HabitID")]
        public Habit Habit { get; set; }
    }
}
