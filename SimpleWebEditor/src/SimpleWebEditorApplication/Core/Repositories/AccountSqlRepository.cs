using System.Collections.Generic;
using System.Linq;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Core.Models;

namespace SimpleWebEditorApplication.Core.Repositories
{
    public class AccountSqlRepository : SqlRepositoryBase<string, Account>, IAccountRepository
    {
        public AccountSqlRepository(CoreDbContext context) : base (context)
        {
        }

        public override bool Add(Account item)
        {
            if (item == null)
            {
                return false;
            }/*
            if (_context.Accounts.Contains(item))
            {
                return false;
            }*/
            _context.Accounts.Add(item);
            _context.Pages.Add(item.WorkPage);
            _context.Pages.Add(item.PublishedPage);
            _context.SaveChanges();
            return true;
        }

        public override Account Get(string itemId)
        {
            return _context.Accounts.FirstOrDefault(acc => acc.UserName.Equals(itemId));
        }

        public override IEnumerable<Account> GetAll()
        {
            return new List<Account>(_context.Accounts);
        }

        public override bool Remove(Account item)
        {
            if (item == null)
            {
                return false;
            }
            item.WorkPage.DeleteFile();
            item.PublishedPage.DeleteFile();
            _context.Accounts.Remove(item);
            _context.SaveChanges();
            return true;
        }
        
    }
}
