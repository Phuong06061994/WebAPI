using System;

namespace DAL.Dto
{
    public class News
    {
       
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Theme { get; set; }
        public Guid UserId { get; set; }

    }
}
