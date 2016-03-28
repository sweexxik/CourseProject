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
        IEnumerable<NewRatingModel> GetRatings();
        IEnumerable<NewCommentModel> GetComments();

        Task<IEnumerable<UserViewModel>> SaveUserData(UserViewModel model);
        Task<IdentityResult> DeleteUser (UserViewModel model);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);

        Task<IEnumerable<Tag>> SaveTag(TagsViewModel model);
        Task<IEnumerable<Tag>> DeleteTag(int id);

        Task<IEnumerable<Rating>> DeleteRating(int id);
        Task<IEnumerable<NewRatingModel>> SaveRating(NewRatingModel model);

        Task<IEnumerable<NewCommentModel>> SaveComment(NewCommentModel model);
        Task<IEnumerable<NewCommentModel>> DeleteComment(int id);
    }
}