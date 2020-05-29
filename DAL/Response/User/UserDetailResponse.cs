using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Response.User
{
    public class UserDetailResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }

        public IList<string> Roles = new List<string>();

    }
}
