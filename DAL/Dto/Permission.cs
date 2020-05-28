using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Dto
{
    public class Permission
    {
        public Guid RoleId { get; set; }

        public string FunctionId { get; set; }

        public string ActionId { get; set; }
    }
}
