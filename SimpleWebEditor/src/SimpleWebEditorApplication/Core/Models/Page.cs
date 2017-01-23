using System;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebEditorApplication.Core.Models
{
    public class Page
    {
        public const string PUBLISHED_PAGE_PATH = "/PublishedPages/";
        public const string WORK_PAGE_PATH = "/WorkPages/";

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
            ResetPagePath();
            CreateFile();
        }

        public string RequestPagePath()
        {
            return Startup.FILE_SERVER + PagePath;
        }

        public bool CreateFile()
        {
            var path = CalculatePath();
            if (File.Exists(path)) return false;
            File.Create(path);
            return true;
        }

        public bool DeleteFile()
        {
            var path = CalculatePath();
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

        // MUST UPDATE DATABASE
        public void OverwriteFile(string htmlCode)
        {
            DeleteFile();
            ResetPagePath();
            var path = CalculatePath();
            File.WriteAllText(path, htmlCode);
        }

        // MUST UPDATE DATABASE
        private void ResetPagePath()
        {
            PagePath = (IsPublished ? PUBLISHED_PAGE_PATH : WORK_PAGE_PATH) + Owner.UserName + Guid.NewGuid() + ".html";
        }

        private string CalculatePath()
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), @"UserPagesServer");
            return dir + PagePath;
        }
    }
}
