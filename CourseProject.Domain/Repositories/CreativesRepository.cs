using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CourseProject.Domain.Context;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;

namespace CourseProject.Domain.Repositories
{
    class CreativesRepository : IRepository<Creative>
    {
        private readonly CreativeContext db;

        public CreativesRepository(CreativeContext context)
        {
            db = context;
        }

        public IEnumerable<Creative> GetAll()
        {
            return db.Creatives.ToList();
        }

        public Creative Get(int id)
        {
            return db.Creatives.Find(id);
        }

        public IEnumerable<Creative> Find(Func<Creative, bool> predicate)
        {
            return db.Creatives.Where(predicate).ToList();
        }

        public void Create(Creative item)
        {
            db.Creatives.Add(item);
        }

        public void Update(Creative item)
        {
           db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var creative = db.Creatives.Find(id);

            if (creative != null)
            {
                db.Creatives.Remove(creative);
            }
        }
    }
}