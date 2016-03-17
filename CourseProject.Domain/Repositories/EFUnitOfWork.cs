using System;
using CourseProject.Domain.Context;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;

namespace CourseProject.Domain.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly CreativeContext db;

        private CreativesRepository creativesRepository;
        private CommentsRepository commentsRepository;
        private LikesRepository likesRepository;
        private ChaptersRepository chaptersRepository;

        private bool isDisposed;

        public EfUnitOfWork(string connectionString)
        {
            db = new CreativeContext(connectionString);
        }
        
        public IRepository<Creative> Creatives
        {
            get
            {
                return creativesRepository ?? (creativesRepository = new CreativesRepository(db));
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return commentsRepository ?? (commentsRepository = new CommentsRepository(db));
            }
        }

        public IRepository<Chapter> Chapters
        {
            get
            {
                return chaptersRepository ?? (chaptersRepository = new ChaptersRepository(db));
            }
        }

        public IRepository<Like> Likes
        {
            get
            {
                return likesRepository ?? (likesRepository = new LikesRepository(db));
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                db.Dispose();
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}