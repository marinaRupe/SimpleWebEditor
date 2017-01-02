using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Core.Models;

namespace SimpleWebEditorApplication.Core.Repositories
{
    public class PageRepository : IPageRepository
    {
        public bool Add(Page item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Page item)
        {
            throw new NotImplementedException();
        }

        public Page Get(object itemId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Page> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Page item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(object itemId)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Page> GetPublished()
        {
            throw new NotImplementedException();
        }

        public Page GetByOwner(Account owner, bool published)
        {
            throw new NotImplementedException();
        }
    }
}
