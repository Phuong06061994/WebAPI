using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.News
{
    public class NewsCreateModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Theme { get; set; }
        public string CreatedBy { get; set; }
    }
}
