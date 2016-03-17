using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CourseProject.Domain.Context;
using CourseProject.Domain.Interfaces;

namespace CourseProject.Domain.Repositories
{
    class CommentsRepository : IRepository<Comment>
    {
        private readonly CreativeContext db;

        public CommentsRepository(CreativeContext context)
        {
            db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments.ToList();
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return db.Comments.Where(predicate).ToList();
        }

        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = db.Comments.Find(id);

            if (item != null)
            {
                db.Comments.Remove(item);
            }
        }
    }
}
