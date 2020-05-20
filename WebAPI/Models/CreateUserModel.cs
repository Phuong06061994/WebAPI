using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter PassWord")]

        public string Password { get; set; }
    }
}
