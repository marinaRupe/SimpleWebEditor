using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebEditorApplication.Core.Models
{

    public class Account
    {
        /// <summary>
        /// Use to connect to SimpleWebEditor.Models.ApplicationUser
        /// </summary>
        [Key]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        public Page WorkPage { get; set; }

        public Page PublishedPage { get; set; }
    }
}
