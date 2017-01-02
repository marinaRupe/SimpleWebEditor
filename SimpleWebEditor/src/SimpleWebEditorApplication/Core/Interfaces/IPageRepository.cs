using System.Collections.Generic;
using SimpleWebEditorApplication.Core.Models;

namespace SimpleWebEditorApplication.Core.Interfaces
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<Page> GetPublished();

        Page GetByOwner(Account owner, bool published);
    }
}
