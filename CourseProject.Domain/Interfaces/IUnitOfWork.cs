using System;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Creative> Creatives { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Chapter> Chapters { get; }
        IRepository<Like> Likes { get; }
        Task<IdentityUser> FindUser(string userName);
        void Save();
    }
}
