using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Response
{
    public class NewsResponse
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Theme { get; set; }
        public Guid UserId { get; set; }
    }
}
