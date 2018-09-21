using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Comment
{
    public class AddCommentViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Post Id")]
        public int PostId { get; set; }

        [Display(Name = "Author")]
        [MaxLength(50, ErrorMessage = "Max length of {0} characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Content")]
        [MaxLength(300, ErrorMessage = "Max length of {0} characters.")]
        public string Content { get; set; }
    }
}