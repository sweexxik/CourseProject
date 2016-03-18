using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain;
using CourseProject.Interfaces;
using CourseProject.UserEntities;

namespace CourseProject.Repositories
{
    class LikesRepository : IRepository<Like>
    {
        private readonly AuthContext db;

        public LikesRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<Like> GetAll()
        {
            return db.Likes.ToList();
        }

        public async Task<Like> Get(int id)
        {
            return  await db.Likes.FindAsync(id);
        }

        public IEnumerable<Like> Find(Func<Like, bool> predicate)
        {
            return db.Likes.Where(predicate).ToList();
        }

        public void Create(Like item)
        {
            db.Likes.Add(item);
        }

        public void Update(Like item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = db.Likes.Find(id);

            if (item != null)
            {
                db.Likes.Remove(item);
            }
        }
    }
}