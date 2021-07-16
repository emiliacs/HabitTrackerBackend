using System.ComponentModel.DataAnnotations;

namespace TeamRedBackEnd.DataTransferObject
{
    public class EditUserDto
    {
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters. "), MinLength(3, ErrorMessage = "Username can't be shorter than 3 characters")]
        [RegularExpression(@"^[0-9a-zA-Z-_]{1,50}$", ErrorMessage = "Username can only contain letters, numbers or -_")]
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool PublicProfile { get; set; }

    }
}
