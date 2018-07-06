using System;
using System.Collections.Generic;

namespace Web.Models.Admin
{
    public class IndexViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}