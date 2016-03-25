﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;

namespace CourseProject.Repositories
{
    class LikesRepository : IRepository<Like>
    {
        private readonly DatabaseContext db;

        public LikesRepository(DatabaseContext context)
        {
            db = context;
        }

        public IEnumerable<Like> GetAll()
        {
            return db.Likes.ToList();
        }

        public async Task<Like> Get(int id)
        {
            return await db.Likes.FindAsync(id);
        }

        public IEnumerable<Like> Find(Func<Like, bool> predicate)
        {
            return db.Likes.Where(predicate).ToList();
        }

        public void Add(Like item)
        {
            db.Likes.Add(item);
        }

        public void Update(Like item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<Like> Remove(int id)
        {
            var item = await db.Likes.FindAsync(id);

            if (item == null) return null;

            db.Likes.Remove(item);

            return item;
        }

        public Task<IEnumerable<Creative>> Search(string pattern)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Like> range)
        {
            db.Likes.AddRange(range);
        }

        public void RemoveRange(IEnumerable<Like> range)
        {
            db.Likes.RemoveRange(range);
        }
    }
}