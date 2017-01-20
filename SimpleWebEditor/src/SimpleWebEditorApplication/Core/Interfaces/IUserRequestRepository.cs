using SimpleWebEditorApplication.Core.Models;
using System;

namespace SimpleWebEditorApplication.Core.Interfaces
{
    public interface IUserRequestRepository : IRepository<Guid, UserRequest>
    {
    }
}
