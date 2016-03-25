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
        private readonly DatabaseContext db;

        public TagsRepository(DatabaseContext context)
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

        public async Task<IEnumerable<Creative>> Search(string pattern)
        {
            var ftsResults = db.Tags.Where(x => x.Name.Contains(pattern)).Select(t => t.Id);

            var foundedTags = db.Tags.Where(tag => ftsResults.Contains(tag.Id)).ToList();

            var foundedCreatives = new List<Creative>();

            foreach (var tag in foundedTags)
            {
                var creative = await db.Creatives.FindAsync(tag.CreativeId);

                if (!foundedCreatives.Contains(creative))
                {
                    foundedCreatives.Add(creative);
                }
            }

            return foundedCreatives;
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