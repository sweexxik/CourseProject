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
        private readonly IRatingService ratingService;

        public AdminService(IUnitOfWork repo, IAccountService serv, IRatingService ratingServ)
        {
            db = repo;
            service = serv;
            ratingService = ratingServ;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            return db.Users.GetAllUsers().Select(x => service.InitUserViewModel(x)).ToList();
        }

        public IEnumerable<Medal> GetMedals()
        {
            return db.Medals.GetAll().ToList();
        }

        public IEnumerable<NewRatingModel> GetRatings()
        {
            var result = db.Ratings.GetAll();

            return ratingService.InitRatingModel(result);
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

        public async Task<IEnumerable<Tag>> SaveTag(TagsViewModel model)
        {
            if (model.Id == 0)
            {
                db.Tags.Add(new Tag { Name = model.Name });
            }
            else
            {
                var editTag = await db.Tags.Get(model.Id);

                editTag.Name = model.Name;
            }

            db.Save();

            return db.Tags.GetAll();
        }

        public async Task<IEnumerable<NewRatingModel>> SaveRating(NewRatingModel model)
        {

            var editRating = await db.Ratings.Get(model.Id);

            editRating.Value = model.Value;

            db.Save();

            return ratingService.InitRatingModel(db.Ratings.GetAll());
        }

        public async Task<IEnumerable<Tag>> DeleteTag(int id)
        {
            if (await db.Tags.Remove(id))
            {
                db.Save();

                return db.Tags.GetAll();
            }

            return null;
        }

        public async Task<IEnumerable<Rating>> DeleteRating(int id)
        {
            if (await db.Ratings.Remove(id))
            {
                db.Save();

                return db.Ratings.GetAll();
            }

            return null;
        }
    }
}