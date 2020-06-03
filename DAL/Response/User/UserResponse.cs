using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }

    }
}
