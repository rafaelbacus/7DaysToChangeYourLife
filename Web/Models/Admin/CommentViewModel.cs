using System;

namespace Web.Models.Admin
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; } = "Anonymous";
        public string Content { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
    }
}