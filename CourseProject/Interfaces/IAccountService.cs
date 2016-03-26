using System.Threading.Tasks;
using CourseProject.Domain.Models;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Interfaces
{
    public interface IAccountService
    {
        Task<UserViewModel> GetUserInfo(string userName);
        Task<IdentityResult> SaveUserData(UserViewModel viewModel);
        Task<IdentityResult> CreateUser(UserModel model);
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        void Dispose(bool disposing);
    }
}