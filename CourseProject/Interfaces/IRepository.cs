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
        void Add(T item);
       
        void Update(T item);
        Task<T> Remove(int id);

        void AddRange(IEnumerable<T> range);
        void RemoveRange(IEnumerable<T> range);
    }
}
