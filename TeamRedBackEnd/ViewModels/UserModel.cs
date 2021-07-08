using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MinLength(10, ErrorMessage = "Password needs to be at least 10 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name can't be empty")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters. "), MinLength(3, ErrorMessage = "Username can't be shorter than 3 characters")]
        [RegularExpression(@"^[0-9a-zA-Z-_]{1,50}$", ErrorMessage =  "Username can only contain letters, numbers or -_")]
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool PublicProfile { get; set; }
        public int[] FriendIds { get; set; }
        public int[] GroupIds { get; set; }
        public byte[] Salt { get; set; }
        public byte[] BytePassword { get; set; }
        public string VerificationCode { get; set; }
        public bool Verified { get; set; }

        public string Token { get; set; }
        public ICollection<Database.Models.Group> Groups { get; set; }
        public ICollection<Habit> Habits { get; set; }

    }
}
