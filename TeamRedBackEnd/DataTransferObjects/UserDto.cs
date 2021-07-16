
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
