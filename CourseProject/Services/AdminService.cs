using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;

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
            return db.GetAllUsers().Select(x => service.InitUserViewModel(x)).ToList();
        }

        public IEnumerable<Medal> GetMedals()
        {
            return db.Medals.GetAll().ToList();
        }

        public async Task<IEnumerable<UserViewModel>> SaveUserData(UserViewModel model)
        {
            var user = await db.FindUserById(model.Id);
            var result = await service.InitApplicatonUser(model, user);

            try
            {
                await db.UpdateUser(result);
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
       

            return db.GetAllUsers().Select(x => service.InitUserViewModel(x)).ToList();
        }
    }
}