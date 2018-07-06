using System;

namespace Web.Models.Admin
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
    }
}