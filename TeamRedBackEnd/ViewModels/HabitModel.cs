using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TeamRedBackEnd.ViewModels
{
    public class HabitModel 
    {
        [Required]
        public int HabitId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
