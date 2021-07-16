using System.ComponentModel.DataAnnotations;

namespace TeamRedBackEnd.DataTransferObject
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        [MinLength(10, ErrorMessage = "Password needs to be at least 10 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name can't be empty")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters. "), MinLength(3, ErrorMessage = "Username can't be shorter than 3 characters")]
        [RegularExpression(@"^[0-9a-zA-Z-_]{1,50}$", ErrorMessage = "Username can only contain letters, numbers or -_")]
        public string Name { get; set; }
    }
}
