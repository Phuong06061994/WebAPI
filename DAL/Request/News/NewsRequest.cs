using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Request
{
    public class NewsRequest
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Theme { get; set; }
        public string CreatedBy { get; set; }
    }
}
