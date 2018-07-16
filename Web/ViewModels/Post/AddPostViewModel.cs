using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Post
{
    public class AddPostViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string Content { get; set; }
    }
}