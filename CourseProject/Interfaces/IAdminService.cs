using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;
using Microsoft.AspNet.Identity;

namespace CourseProject.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<UserViewModel> GetUsers();
        IEnumerable<Medal> GetMedals();
        Task<IEnumerable<UserViewModel>> SaveUserData(UserViewModel model);
        Task<IdentityResult> DeleteUser (UserViewModel model);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);
    }
}