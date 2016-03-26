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

namespace CourseProject.Services
{
    public class AccountService : IAccountService
    {

        private readonly IUnitOfWork db;

        public string WorkingFolder => HttpRuntime.AppDomainAppPath + @"\Uploads";

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

        public async Task<UserViewModel> UploadFile(CustomMultipartFormDataStreamProvider provider)
        {
            var user = await db.FindUser(provider.FormData.Get("username"));

            var result = CloudinaryUpload(provider);

            user.AvatarUri = result.Uri.AbsoluteUri;

            await db.UpdateUser(user);

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