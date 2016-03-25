using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;

namespace CourseProject.Repositories
{
    public class MedalsRepository : IRepository<Medal>
    {
        private readonly DatabaseContext db;

        public MedalsRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<Medal> GetAll()
        {
            return db.Medals.ToList();
        }

        public async Task<Medal> Get(int id)
        {
            return await db.Medals.FindAsync(id);
        }

        public IEnumerable<Medal> Find(Func<Medal, bool> predicate)
        {
            return db.Medals.Where(predicate).ToList();
        }

        public void Add(Medal item)
        {
            db.Medals.Add(item);
        }

        public void Update(Medal item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Medal> Remove(int id)
        {
            var item = await db.Medals.FindAsync(id);

            if (item == null) return null;

            db.Medals.Remove(item);

            return item;
        }

        public Task<IEnumerable<Creative>> Search(string pattern)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Medal> range)
        {
            db.Medals.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Medal> range)
        {
            db.Medals.RemoveRange(range);
        }
    }
}