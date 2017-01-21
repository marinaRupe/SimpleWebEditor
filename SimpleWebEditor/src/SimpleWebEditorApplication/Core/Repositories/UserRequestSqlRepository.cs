using System;
using System.Collections.Generic;
using System.Linq;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Core.Models;

namespace SimpleWebEditorApplication.Core.Repositories
{
    public class UserRequestSqlRepository : SqlRepositoryBase<Guid, UserRequest>, IUserRequestRepository
    {
        public UserRequestSqlRepository(CoreDbContext context) : base (context)
        {
        }

        public override bool Add(UserRequest item)
        {
            if (item == null)
            {
                return false;
            }
            if (_context.UserRequests.Select(ur => ur.Id).Contains(item.Id))
            {
                return false;
            }
            _context.UserRequests.Add(item);
            _context.SaveChanges();
            return true;
        }

        public override UserRequest Get(Guid itemId)
        {
            return _context.UserRequests.FirstOrDefault(ur => ur.Id.Equals(itemId));
        }

        public override IEnumerable<UserRequest> GetAll()
        {
            return new List<UserRequest>(_context.UserRequests);
        }

        public override bool Remove(UserRequest item)
        {
            if (item == null)
            {
                return false;
            }
            _context.UserRequests.Remove(item);
            _context.SaveChanges();
            return true;
        }
    }
}
