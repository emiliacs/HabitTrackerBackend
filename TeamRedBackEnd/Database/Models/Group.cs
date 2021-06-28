using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.Database.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int HabitID { get; set; }
       
        public int[] UserIds { get; set; } // change to int array with user ids

        [ForeignKey("HabitID")]
        public Habit Habit { get; set; }
    }
}
