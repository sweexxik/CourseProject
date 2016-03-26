using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.Models;
using CourseProject.Interfaces;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork db;
   
        public AccountService(IUnitOfWork repo)
        {
            db = repo;
        }

        public async Task<UserViewModel> GetUserInfo(string userName)
        {
            var user = await db.FindUser(userName);
            return InitUserViewModel(user);
        }

        public async Task<IdentityResult> CreateUser(UserModel model)
        {
            return await db.RegisterUser(model);
        }

        public async Task<IdentityResult> SaveUserData(UserViewModel viewModel)
        {
            return await db.UpdateUser(await InitApplicatonUser(viewModel));
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model)
        {
            var user = await db.FindUser(model.UserName);

            return await db.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        private UserViewModel InitUserViewModel(ApplicationUser user)
        {
            return new UserViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Medals = user.Medals,
                AvatarUri = user.AvatarUri,
                Roles = user.Roles.Select(x=>x.UserId)
            };
        }

        private async Task<ApplicationUser> InitApplicatonUser(UserViewModel model)
        {
            var user = await db.FindUser(model.UserName);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            return user;
        }
    }
}