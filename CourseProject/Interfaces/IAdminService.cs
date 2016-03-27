using System.Collections.Generic;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<UserViewModel> GetUsers();
        IEnumerable<Medal> GetMedals();
        IEnumerable<UserViewModel> SaveUserData(UserViewModel model);
    }
}