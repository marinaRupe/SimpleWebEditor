using System.Collections.Generic;

namespace SimpleWebEditorApplication.Core.Interfaces
{
    public interface IRepository<K, T>
    {
        bool Add(T item);

        bool Update(T item);

        T Get(K itemId);

        IEnumerable<T> GetAll();

        bool Remove(T item);

        bool Remove(K itemId);
        
    }
}
