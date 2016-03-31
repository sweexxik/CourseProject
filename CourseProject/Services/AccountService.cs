using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.Models;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Services
{
    public class AccountService : IAccountService
    {

        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;


        public string WorkingFolder => HttpRuntime.AppDomainAppPath + @"\Uploads";

        public AccountService(IUnitOfWork repo, IMedalService medal)
        {
            db = repo;
            medalService = medal;
        }
         
        public async Task<UserViewModel> GetUserInfo(string userName)
        {
            await medalService.CheckMedals(userName);

            var user = await db.Users.FindUser(userName);

            return user != null ? InitUserViewModel(user) : null;
        }

        public async Task<IdentityResult> CreateUser(UserModel model)
        {
            return await db.Users.RegisterUser(model);
        }

        public async Task<IdentityResult> SaveUserData(UserViewModel viewModel)
        {
            var user = await db.Users.FindUserById(viewModel.Id);
            return await db.Users.UpdateUser(await InitApplicatonUser(viewModel, user));
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model)
        {
            var user = await db.Users.FindUser(model.UserName);

            return await db.Users.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
        }

        public async Task<UserViewModel> UploadFile(CustomMultipartFormDataStreamProvider provider)
        {
            var user = await db.Users.FindUser(provider.FormData.Get("username"));

            var result = CloudinaryUpload(provider);

            user.AvatarUri = result.Uri.AbsoluteUri;

            await db.Users.UpdateUser(user);

            return InitUserViewModel(user);
        }

        private ImageUploadResult CloudinaryUpload(CustomMultipartFormDataStreamProvider provider)
        {
            var account = new Account("ddttiy9ko", "799681156658259", "_A8bJk28HFotHtOJCMPFKrb1rII");

            var cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams();

            uploadParams.File = new FileDescription($"{provider.FileData[0].LocalFileName}");

            return cloudinary.Upload(uploadParams);
        }

        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(WorkingFolder, fileName)
              .FirstOrDefault();

            return file != null;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        public UserViewModel InitUserViewModel(ApplicationUser user)
        {
            var isAdmin = user.Roles.Count > 0 && user.Roles.First().RoleId == "4c05d228-9442-4df9-9bcb-11ee9c1da16e";

            return new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Medals = SetUserMedalsModel(user.Medals, user),
                AvatarUri = user.AvatarUri,
                IsAdmin = isAdmin
            };
        }

        private IEnumerable<MedalViewModel> SetUserMedalsModel(IEnumerable<Medal> model, ApplicationUser user)
        {
            return model.Select(x => new MedalViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Selected = user.Medals.Contains(x),
                ImageUri = x.ImageUri
            }).ToList();
        }

        public async Task<ApplicationUser> InitApplicatonUser(UserViewModel model, ApplicationUser user)
        {
            user.FirstName = model.FirstName;

            user.LastName = model.LastName;

            user.Email = model.Email;

            user.UserName = model.UserName;

            user.PhoneNumber = model.PhoneNumber;

            if (model.IsAdmin)
            {
                if (user.Roles.Count == 0)
                {
                    user.Roles.Add(new IdentityUserRole
                    {
                        UserId = user.Id,
                        RoleId = "4c05d228-9442-4df9-9bcb-11ee9c1da16e"
                    });
                }
            }

            user.Medals = new List<Medal>();

            foreach (var medal in model.Medals)
            {
                switch (medal.Id)
                {
                    case 1: 
                        user.Medals.Add(await db.Medals.Get(1));
                        break;
                    case 2: user.Medals.Add(await db.Medals.Get(2));
                        break;
                    case 3: user.Medals.Add(await db.Medals.Get(3));
                        break;
                }
            }

            return user;
        }
    }
}