using System.ComponentModel.DataAnnotations;

namespace TeamRedBackEnd.DataTransferObject
{
    public class EditPasswordDto
    {
        [Required(ErrorMessage = "Password can't be empty")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password can't be empty")]
        [MinLength(10, ErrorMessage = "Password needs to be at least 10 characters long")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please confirm your new password")]
        public string ConfrimPassword { get; set; }
    }
}
