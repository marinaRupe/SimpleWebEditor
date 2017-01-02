using System.Data.Entity;
using SimpleWebEditorApplication.Core.Models;

namespace SimpleWebEditorApplication.Core
{
    public class CoreDbContext : DbContext
    {
        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<Page> Pages { get; set; }
        public IDbSet<UserRequest> UserRequests { get; set; }

        public CoreDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasOptional(acc => acc.WorkPage).WithRequired(page => page.Owner);
            modelBuilder.Entity<Account>().HasOptional(acc => acc.PublishedPage).WithRequired(page => page.Owner);
            modelBuilder.Entity<UserRequest>().HasRequired(req => req.Sender).WithOptional();
        }
    }
}
