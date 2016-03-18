using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseProject.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //todo make async?

        IEnumerable<T> GetAll();
        Task<T> Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
