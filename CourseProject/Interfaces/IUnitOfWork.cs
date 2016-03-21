using System;
using System.Threading.Tasks;
using CourseProject.Domain;
using CourseProject.Domain.Entities;
using CourseProject.Models;
using CourseProject.UserEntities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Creative> Creatives { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Chapter> Chapters { get; }
        IRepository<Like> Likes { get; }
        IRepository<CreativeCategory> Categories { get; }
        Task<ApplicationUser> FindUser(string userName);
        Task<ApplicationUser> FindUser(string userName, string password);
        Task<IdentityResult> RegisterUser(UserModel userModel);
        Task<bool> CheckUserRole(string userId);
        void Save();
    }
}
