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

        public UserRequest()
        {
            // entity framework needs this one
        }

        public UserRequest(Account sender, string description)
        {
            Id = Guid.NewGuid();
            Sender = sender;
            Description = description;
        }
    }
}
