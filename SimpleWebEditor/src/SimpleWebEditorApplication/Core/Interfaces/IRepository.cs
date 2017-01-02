using System.Collections.Generic;

namespace SimpleWebEditorApplication.Core.Interfaces
{
    public interface IRepository<T>
    {
        bool Add(T item);

        bool Update(T item);

        T Get(object itemId);

        IEnumerable<T> GetAll();

        bool Remove(T item);

        bool Remove(object itemId);

        void RemoveAll();
    }
}
