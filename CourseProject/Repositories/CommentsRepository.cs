using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;

namespace CourseProject.Repositories
{
    class CommentsRepository : Interfaces.IRepository<Comment>
    {
        private readonly DatabaseContext db;

        public CommentsRepository(DatabaseContext context)
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

        public void Add(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Comment> Remove(int id)
        {
            var item = await db.Comments.FindAsync(id);

            if (item == null) return null;

            db.Comments.Remove(item);

            return item;
        }

        public async Task<IEnumerable<Creative>> Search(string pattern)
        {
            var ftsResults1 = db.Comments.Where(c => c.Text.Contains(pattern)).Select(c => c.Id);

            var ftsResults2 = db.Comments.Where(c => c.User.UserName.Contains(pattern)).Select(c => c.Id);

            var ftsResults = ftsResults1.Union(ftsResults2).Distinct();

            var foundedComments = db.Comments.Where(comment => ftsResults.Contains(comment.Id)).ToList();

            var foundedCreatives = new List<Creative>();

            foreach (var comment in foundedComments)
            {
                var creative = await db.Creatives.FindAsync(comment.CreativeId);

                if (!foundedCreatives.Contains(creative))
                {
                    foundedCreatives.Add(creative);
                }
            }

            return foundedCreatives;
        }

        public void AddRange(IEnumerable<Comment> range)
        {
            db.Comments.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Comment> range)
        {
            db.Comments.RemoveRange(range);
        }
    }
}
