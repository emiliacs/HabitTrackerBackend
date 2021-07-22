using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.DataTransferObjects
{
    public class EditHabitDto
    {
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }
        public bool Favorite { get; set; }
        public bool PublicHabit { get; set; }
    }
}
