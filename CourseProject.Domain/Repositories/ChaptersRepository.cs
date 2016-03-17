using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CourseProject.Domain.Context;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;

namespace CourseProject.Domain.Repositories
{
    class ChaptersRepository : IRepository<Chapter>
    {
        private readonly CreativeContext db;

        public ChaptersRepository(CreativeContext context)
        {
            db = context;
        }

        public IEnumerable<Chapter> GetAll()
        {
            return db.Chapters.ToList();
        }

        public Chapter Get(int id)
        {
            return db.Chapters.Find(id);
        }

        public IEnumerable<Chapter> Find(Func<Chapter, bool> predicate)
        {
            return db.Chapters.Where(predicate).ToList();
        }

        public void Create(Chapter item)
        {
            db.Chapters.Add(item);
        }

        public void Update(Chapter item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var item = db.Chapters.Find(id);

            if (item != null)
            {
                db.Chapters.Remove(item);
            }
        }
    }
}