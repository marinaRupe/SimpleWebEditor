using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebEditorApplication.Core.Models
{
    public class UserRequest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Account Sender { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
