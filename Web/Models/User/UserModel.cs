using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class UserModel
    {
        public Guid Id  { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
        public IList<string> Roles { get; set; }
        //public IList<RoleModel> Roles { get; set; }
    }
}
