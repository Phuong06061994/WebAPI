using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Dto
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
