using System;
using System.Collections.Generic;
using Web.ViewModels.Comment;

namespace Web.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public DateTime RowCreatedDateTime { get; set; }
    }
}