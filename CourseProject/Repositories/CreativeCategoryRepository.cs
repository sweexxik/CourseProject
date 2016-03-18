using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.UserEntities;

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

        public void Create(CreativeCategory item)
        {
            db.Categories.Add(item);
        }

        public void Update(CreativeCategory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var category = db.Categories.Find(id);

            if (category != null)
            {
                db.Categories.Remove(category);
            }
        }
    }
}