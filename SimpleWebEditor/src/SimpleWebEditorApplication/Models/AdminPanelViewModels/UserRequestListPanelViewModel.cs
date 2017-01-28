using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebEditorApplication.Models.AdminPanelViewModels
{
    public class UserRequestListPanelViewModel
    {
        [Required]
        [Display(Name = "Request ID")]
        public string RequestId { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Request")]
        public string Request { get; set; }
    }
}
