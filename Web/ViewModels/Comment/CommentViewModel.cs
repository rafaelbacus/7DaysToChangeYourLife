using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        [Display(Name = "Author")]
        [MaxLength(64, ErrorMessage = "Max length of {0} characters.")]
        public string Author { get; set; }

        [Display(Name = "Content")]
        [MaxLength(1024, ErrorMessage = "Max length of {0} characters.")]
        public string Content { get; set; }

        [Display(Name = "Replies")]
        public ICollection<CommentViewModel> Replies { get; set; }

        [Display(Name = "Created")]
        public DateTime RowCreatedDateTime { get; set; }
    }
}