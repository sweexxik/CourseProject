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
    public class RatingsRepository : IRepository<Rating>
    {
        private readonly AuthContext db;

        public RatingsRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<Rating> GetAll()
        {
           return db.Ratings.ToList();
        }

        public async Task<Rating> Get(int id)
        {
            return await db.Ratings.FindAsync(id);
        }

        public IEnumerable<Rating> Find(Func<Rating, bool> predicate)
        {
            return db.Ratings.Where(predicate).ToList();
        }

        public void Add(Rating item)
        {
            db.Ratings.Add(item);
        }

        public void Update(Rating item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Rating> Remove(int id)
        {
            var item = await db.Ratings.FindAsync(id);

            if (item == null) return null;

            db.Ratings.Remove(item);

            return item;
        }

        public Task<IEnumerable<Creative>> Search(string pattern)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Rating> range)
        {
            db.Ratings.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Rating> range)
        {
            db.Ratings.RemoveRange(range);
        }
    }
}