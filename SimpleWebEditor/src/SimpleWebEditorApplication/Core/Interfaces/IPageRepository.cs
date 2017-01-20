using System.Collections.Generic;
using SimpleWebEditorApplication.Core.Models;
using System;

namespace SimpleWebEditorApplication.Core.Interfaces
{
    public interface IPageRepository : IRepository<Guid, Page>
    {
        IEnumerable<Page> GetPublished();

        Page GetByOwner(Account owner, bool published);
    }
}
