using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamRedBackEnd.Database.Models
{
    public class User : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UpperName { get; set; }
        public string Email { get; set; }
        public string UpperEmail { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public bool PublicProfile { get; set; }
        public int[] FriendIds { get; set; }
        public int[] GroupIds { get; set; }
        public byte[] Salt { get; set; }
        public byte[] BytePassword { get; set; }
        public string VerificationCode { get; set; }
        public bool Verified { get; set; }



        public ICollection<Group> Groups { get; set; }
        public ICollection<Habit> Habits { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
