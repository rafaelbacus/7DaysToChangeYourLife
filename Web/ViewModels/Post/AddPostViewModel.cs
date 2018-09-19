using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Post
{
    public class AddPostViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(150, ErrorMessage = "Max length of {0} characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(4000, ErrorMessage = "Max length of {0} characters.")]
        public string Content { get; set; }
    }
}