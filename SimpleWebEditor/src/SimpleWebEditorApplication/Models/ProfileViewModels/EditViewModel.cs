using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebEditorApplication.Models.ProfileViewModels
{
    public class EditViewModel
    {
        [Required]
        [Display(Name = "First name:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name:")]
        public string LastName { get; set; }
    }
}
