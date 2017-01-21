using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebEditorApplication.Models.ProfileViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "First name:")]
        public string FirstName { get; set; }

        [Display(Name = "Last name:")]
        public string LastName { get; set; }

        [Display(Name = "Birth date:")]
        public string BirthDate { get; set; }
    }
}
