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
    public class TagsRepository : IRepository<Tag>
    {
        private readonly AuthContext db;

        public TagsRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return db.Tags.ToList();
        }

        public async Task<Tag> Get(int id)
        {
            return await db.Tags.FindAsync(id);
        }

        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return db.Tags.Where(predicate).ToList();
        }

        public void Add(Tag item)
        {
            db.Tags.Add(item);
        }

        public void Update(Tag item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Tag> Remove(int id)
        {
            var item = await db.Tags.FindAsync(id);

            if (item == null) return null;

            db.Tags.Remove(item);

            return item;
        }

        public void AddRange(IEnumerable<Tag> range)
        {
            db.Tags.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Tag> range)
        {
            db.Tags.RemoveRange(range);
        }
    }
}