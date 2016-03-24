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
    public class CreativeCategoryRepository : IRepository<CreativeCategory>
    {
        private readonly AuthContext db;

        public CreativeCategoryRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<CreativeCategory> GetAll()
        {
           return db.Categories.ToList();
        }

        public async Task<CreativeCategory> Get(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public IEnumerable<CreativeCategory> Find(Func<CreativeCategory, bool> predicate)
        {
            return db.Categories.Where(predicate).ToList();
        }

        public void Add(CreativeCategory item)
        {
            db.Categories.Add(item);
        }

        public void Update(CreativeCategory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<CreativeCategory> Remove(int id)
        {
            var item = await db.Categories.FindAsync(id);

            if (item == null) return null;

            db.Categories.Remove(item);

            return item;
        }

        public Task<IEnumerable<Creative>> Search(string pattern)
        {
            //todo implement?
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<CreativeCategory> range)
        {
            db.Categories.AddRange(range);
        }

        public void RemoveRange(IEnumerable<CreativeCategory> range)
        {
            db.Categories.RemoveRange(range);
        }
    }
}