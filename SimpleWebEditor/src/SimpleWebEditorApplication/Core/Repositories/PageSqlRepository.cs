using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Core.Models;

namespace SimpleWebEditorApplication.Core.Repositories
{
    public class PageSqlRepository : SqlRepositoryBase<Guid, Page>, IPageRepository
    {
        public PageSqlRepository(CoreDbContext context) : base (context)
        {
        }

        public override bool Add(Page item)
        {
            if (item == null)
            {
                return false;
            }
            if (_context.Pages.Select(p => p.Id).Contains(item.Id))
            {
                return false;
            }
            item.CreateFile();
            _context.Pages.Add(item);
            _context.SaveChanges();
            return true;
        }

        public override Page Get(Guid itemId)
        {
            return _context.Pages.FirstOrDefault(p => p.Id.Equals(itemId));
        }

        public override IEnumerable<Page> GetAll()
        {
            return new List<Page>(_context.Pages.Include(p => p.Owner));
        }

        public override bool Remove(Page item)
        {
            if (item == null)
            {
                return false;
            }
            item.DeleteFile();
            _context.Pages.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Page> GetPublished()
        {
            return new List<Page>(_context.Pages.Where(p => p.IsPublished).Include(p => p.Owner));
        }

        public Page GetByOwner(Account owner, bool published)
        {
            return _context.Pages.FirstOrDefault(p => p.Owner.UserName.Equals(owner.UserName) && (p.IsPublished == published));
        }

        public bool UpdatePageCode(Guid pageId, string htmlCode)
        {
            var page = Get(pageId);
            if (page == null) return false;
            page.OverwriteFile(htmlCode);
            return Update(page);
        }
    }
}
