using System;
using System.Collections.Generic;
using Web.ViewModels.Post;

namespace Web.ViewModels.Admin
{
    public class AdminViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}