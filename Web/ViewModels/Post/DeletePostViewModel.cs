using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.ViewModels.Comment;

namespace Web.ViewModels.Post
{
    public class DeletePostViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Comments")]
        public ICollection<CommentViewModel> Comments { get; set; }

        [Display(Name = "Created")]
        public DateTime RowCreatedDateTime { get; set; }

        [Display(Name = "Modified")]
        public DateTime RowModifiedDateTime { get; set; }
    }
}