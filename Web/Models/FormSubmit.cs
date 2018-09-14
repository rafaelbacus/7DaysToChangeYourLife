using System;
using Model;

namespace Web.Models
{
    public class FormSubmit 
    {
        public int Id { get; set; }
        public Result Result { get; set; }
        public string IndexUrl { get; set; }
        public string CancelUrl { get; set; }
        public string DeleteUrl { get; set; }
    }
}