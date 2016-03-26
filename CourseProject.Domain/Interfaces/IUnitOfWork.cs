using System;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Creative> Creatives { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Chapter> Chapters { get; }
        IRepository<Like> Likes { get; }
        IRepository<CreativeCategory> Categories { get; }
        IRepository<Rating> Ratings { get; }
        IRepository<Medal> Medals { get; }
        IRepository<Tag> Tags { get; }

        Task<ApplicationUser> FindUser(string userName);
        Task<ApplicationUser> FindUser(string userName, string password);
        Task<IdentityResult> UpdateUser(ApplicationUser user);
        Task<IdentityResult> RegisterUser(UserModel userModel);
        Task<IdentityResult> ChangePassword(string userId, string pass, string newPass);
        Task<bool> CheckUserRole(string userId);
        void Save();
    }
}
