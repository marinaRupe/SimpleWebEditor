using System.Collections.Generic;
using SimpleWebEditorApplication.Core.Interfaces;
using System.Data.Entity;

namespace SimpleWebEditorApplication.Core.Repositories
{
    public abstract class SqlRepositoryBase<K, T> : IRepository<K, T> where T : class 
    {
        protected readonly CoreDbContext _context;

        protected SqlRepositoryBase(CoreDbContext context)
        {
            _context = context;
        }

        public virtual bool Update(T item)
        {
            if (item == null)
            {
                return false;
            }
            _context.Entry(item).State = EntityState.Modified;
            return true;
        }

        public virtual bool Remove(K itemId)
        {
            var item = Get(itemId);
            return Remove(item);
        }

        public abstract bool Add(T item);
        public abstract T Get(K itemId);
        public abstract IEnumerable<T> GetAll();
        public abstract bool Remove(T item);
    }
}
