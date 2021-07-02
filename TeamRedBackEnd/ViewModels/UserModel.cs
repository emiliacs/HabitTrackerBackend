using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.ViewModels
{
    public class Usermodel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool PublicProfile { get; set; }
        public int[] FriendIds { get; set; }
        public int[] GroupIds { get; set; }
        public byte[] Salt { get; set; }
        public byte[] BytePassword { get; set; }



        public ICollection<Group> Groups { get; set; }
        public ICollection<Habit> Habits { get; set; }

    }
}
