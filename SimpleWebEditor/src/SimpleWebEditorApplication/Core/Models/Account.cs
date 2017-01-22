using System;
using System.Collections.Generic;
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

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public List<Page> Pages { get; set; }

        public Account()
        {
            // entity framework needs this one
        }

        public Account(string userName)
        {
            UserName = userName;
        }
    }
}
