using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamRedBackEnd.DataTransferObject
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
        public string Picture { get; set; }
        public bool PublicProfile { get; set; }

    }
}
