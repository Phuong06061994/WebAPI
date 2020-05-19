using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "Enter User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter PassWord")]
        
        public string Password { get; set; }
    }
}
