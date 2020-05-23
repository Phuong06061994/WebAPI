using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter PassWord")]

        public string Password { get; set; }

        public IList<string> Roles { get; set; }
    }
}
