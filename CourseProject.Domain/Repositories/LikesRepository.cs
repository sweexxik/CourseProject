using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CourseProject.Domain.Context;
using CourseProject.Domain.Interfaces;

namespace CourseProject.Domain.Repositories
{
    class LikesRepository : IRepository<Like>
    {
        private readonly CreativeContext db;

        public LikesRepository(CreativeContext context)
        {
            db = context;
        }

        public IEnumerable<Like> GetAll()
        {
            return db.Likes.ToList();
        }

        public Like Get(int id)
        {
            return db.Likes.Find(id);
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