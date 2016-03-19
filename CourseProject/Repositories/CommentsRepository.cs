using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.UserEntities;

namespace CourseProject.Repositories
{
    class CommentsRepository : Interfaces.IRepository<Comment>
    {
        private readonly AuthContext db;

        public CommentsRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments.ToList();
        }

        public async Task<Comment> Get(int id)
        {
            return await db.Comments.FindAsync(id);
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

        public async Task<Comment> Delete(int id)
        {
            var item = await db.Comments.FindAsync(id);

            if (item == null) return null;

            db.Comments.Remove(item);

            return item;
        }
    }
}
