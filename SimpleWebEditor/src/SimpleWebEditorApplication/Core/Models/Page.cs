using System;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebEditorApplication.Core.Models
{
    public class Page
    {
        public const string PUBLISHED_PAGE_PATH = "/publishedPages/";
        public const string WORK_PAGE_PATH = "/workPages/";

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Account Owner { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        [Required]
        public string PagePath { get; set; }

        public Page()
        {
            // entity framework needs this one
        }

        public Page(Account owner, bool published)
        {
            Id = Guid.NewGuid();
            Owner = owner;
            IsPublished = published;
            PagePath = (published ? PUBLISHED_PAGE_PATH : WORK_PAGE_PATH) + owner.UserName + ".html";
            CreateFile();
        }

        public bool CreateFile()
        {
            var root = Directory.GetCurrentDirectory();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), @"UserPagesServer");
            var path = dir + PagePath;
            if (File.Exists(path)) return false;
            File.Create(path);
            return true;
        }

        public bool DeleteFile()
        {
            var root = Directory.GetCurrentDirectory();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), @"UserPagesServer");
            var dir2 = Path.Combine(Directory.GetCurrentDirectory(), "/UserPages");
            var path = Path.Combine(dir, PagePath);
            if (!File.Exists(path)) return false;
            try
            {
                File.Delete(path);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
