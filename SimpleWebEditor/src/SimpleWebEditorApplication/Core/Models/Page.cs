using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebEditorApplication.Core.Models
{
    public class Page
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Account Owner { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
