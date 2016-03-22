using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        Task<T> Delete(int id);
    }
}
