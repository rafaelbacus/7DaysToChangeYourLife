using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Admin
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; } = "Anonymous";

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Replies")]
        public ICollection<CommentViewModel> Replies { get; set; }

        [Display(Name = "Created")]
        public DateTime RowCreatedDateTime { get; set; }
    }
}