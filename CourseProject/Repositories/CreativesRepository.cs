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
    class CreativesRepository : IRepository<Creative>
    {
        private readonly AuthContext db;

        public CreativesRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<Creative> GetAll()
        {
            return db.Creatives.ToList();
        }

        public async Task<Creative> Get(int id)
        {
            return await db.Creatives.FindAsync(id);
        }

        public IEnumerable<Creative> Find(Func<Creative, bool> predicate)
        {
            return db.Creatives.Where(predicate).ToList();
        }

        public void Add(Creative item)
        {
            db.Creatives.Add(item);
        }

        public void Update(Creative item)
        {
           db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Creative> Remove(int id)
        {
            var item = await db.Creatives.FindAsync(id);

            if (item == null) return null;

            db.Creatives.Remove(item);

            return item;
        }

        public void AddRange(IEnumerable<Creative> range)
        {
            db.Creatives.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Creative> range)
        {
            db.Creatives.RemoveRange(range);
        }
    }
}