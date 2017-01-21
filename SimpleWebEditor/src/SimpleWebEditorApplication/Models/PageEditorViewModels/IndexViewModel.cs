using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebEditorApplication.Models.PageEditorViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Work page path")]
        public string WorkPagePath { get; set; }

        [Display(Name = "Published page path")]
        public string PublishedPagePath { get; set; }
    }
}
