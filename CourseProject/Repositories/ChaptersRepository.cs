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
    class ChaptersRepository : IRepository<Chapter>
    {
        private readonly AuthContext db;

        public ChaptersRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<Chapter> GetAll()
        {
            return db.Chapters.ToList();
        }

        public async Task<Chapter> Get(int id)
        {
            return await db.Chapters.FindAsync(id);
        }

        public IEnumerable<Chapter> Find(Func<Chapter, bool> predicate)
        {
            return db.Chapters.Where(predicate).ToList();
        }

        public void Add(Chapter item)
        {
            db.Chapters.Add(item);
        }

        public void Update(Chapter item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Chapter> Remove(int id)
        {
            var item = await db.Chapters.FindAsync(id);

            if (item == null) return null;

            db.Chapters.Remove(item);

            return item;
        }

        public void AddRange(IEnumerable<Chapter> range)
        {
            db.Chapters.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Chapter> range)
        {
            db.Chapters.RemoveRange(range);
        }
    }
}