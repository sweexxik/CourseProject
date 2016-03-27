using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<UserViewModel> GetUsers();
        IEnumerable<Medal> GetMedals();
        Task<IEnumerable<UserViewModel>> SaveUserData(UserViewModel model);
    }
}