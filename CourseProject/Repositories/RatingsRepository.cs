﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.UserEntities;

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

        public void Create(Rating item)
        {
            db.Ratings.Add(item);
        }

        public void Update(Rating item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Rating> Delete(int id)
        {
            var item = await db.Ratings.FindAsync(id);

            if (item == null) return null;

            db.Ratings.Remove(item);

            return item;
        }
    }
}