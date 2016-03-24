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
    class ChaptersRepository : IRepository<Chapter>
    {
        private readonly AuthContext db;

        public ChaptersRepository(AuthContext context)
        {
            db = context;
        }

        public IEnumerable<Chapter> GetAll()
        {
            return db.Chapters.ToList();
        }

        public async Task<Chapter> Get(int id)
        {
            return await db.Chapters.FindAsync(id);
        }

        public IEnumerable<Chapter> Find(Func<Chapter, bool> predicate)
        {
            return db.Chapters.Where(predicate).ToList();
        }

        public void Add(Chapter item)
        {
            db.Chapters.Add(item);
        }

        public void Update(Chapter item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Chapter> Remove(int id)
        {
            var item = await db.Chapters.FindAsync(id);

            if (item == null) return null;

            db.Chapters.Remove(item);

            return item;
        }

        public async Task<IEnumerable<Creative>> Search(string pattern)
        {
            var ftsResults1 = db.Chapters.Where(c => c.Name.Contains(pattern)).Select(c => c.Id);

            var ftsResults2 = db.Chapters.Where(c => c.Body.Contains(pattern)).Select(c => c.Id);

            var ftsResults = ftsResults1.Union(ftsResults2).Distinct();

            var foundedChapters = db.Chapters.Where(chapter => ftsResults.Contains(chapter.Id)).ToList();

            var foundedCreatives = new List<Creative>();

            foreach (var chapter in foundedChapters)
            {
                var creative = await db.Creatives.FindAsync(chapter.CreativeId);

                if (!foundedCreatives.Contains(creative))
                {
                    foundedCreatives.Add(creative);
                }
            }

            return foundedCreatives;
        }

        public void AddRange(IEnumerable<Chapter> range)
        {
            db.Chapters.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Chapter> range)
        {
            db.Chapters.RemoveRange(range);
        }
    }
}