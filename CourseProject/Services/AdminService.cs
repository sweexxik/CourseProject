using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork db;
        private readonly IAccountService service;

        public AdminService(IUnitOfWork repo, IAccountService serv)
        {
            db = repo;
            service = serv;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            return db.Users.GetAllUsers().Select(x => service.InitUserViewModel(x)).ToList();
        }

        public IEnumerable<Medal> GetMedals()
        {
            return db.Medals.GetAll().ToList();
        }

        public async Task<IEnumerable<UserViewModel>> SaveUserData(UserViewModel model)
        {
            var user = await db.Users.FindUserById(model.Id);

            var result = await service.InitApplicatonUser(model, user);

            await db.Users.UpdateUser(result);
           
            return db.Users.GetAllUsers().Select(x => service.InitUserViewModel(x)).ToList();
        }

        public async Task<IdentityResult> DeleteUser(UserViewModel model)
        {
            var user = await db.Users.FindUserById(model.Id);

            db.Ratings.RemoveRange(db.Ratings.Find(x=>x.User.Id == user.Id));
            db.Comments.RemoveRange(db.Comments.Find(x=>x.User.Id == user.Id));
            db.Likes.RemoveRange(db.Likes.Find(x=>x.User.Id == user.Id));
            db.Creatives.RemoveRange(db.Creatives.Find(x=>x.User.Id == user.Id));

            return await db.Users.DeleteUser(user);
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordModel model)
        {
            return await db.Users.ResetPassword(model.UserId, model.NewPassword);
        }
    }
}