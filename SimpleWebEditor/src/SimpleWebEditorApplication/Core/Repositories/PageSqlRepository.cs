﻿using System;
using System.Collections.Generic;
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
            if (_context.Pages.Contains(item))
            {
                return false;
            }
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
            return new List<Page>(_context.Pages);
        }

        public override bool Remove(Page item)
        {
            if (item == null)
            {
                return false;
            }
            _context.Pages.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Page> GetPublished()
        {
            return new List<Page>(_context.Pages.Where(p => p.IsPublished));
        }

        public Page GetByOwner(Account owner, bool published)
        {
            return _context.Pages.FirstOrDefault(p => p.Owner.Equals(owner) && p.IsPublished == published);
        }
    }
}