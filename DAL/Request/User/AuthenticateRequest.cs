using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Request.User
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
