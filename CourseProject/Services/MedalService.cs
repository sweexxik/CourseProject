using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.Repositories;
using CourseProject.Interfaces;

namespace CourseProject.Services
{
    public class MedalService : IMedalService
    {
        private readonly IUnitOfWork db;

        public MedalService()
        {
            db = new EfUnitOfWork();
        }

        public async Task CheckMedals(string userName)
        {
            var user = await db.Users.FindUser(userName);

            if (user == null) return;

            var userMedals = user.Medals ?? new List<Medal>();

            await CheckUserMedals(user, userMedals);

            user.Medals = userMedals;

            await db.Users.UpdateUser(user);
          
        }

        private async Task CheckUserMedals(ApplicationUser user, ICollection<Medal> userMedals)
        {
            await CheckLikesMedal(user, userMedals);

            await CheckCommentsMedal(user, userMedals);

            await CheckCreativesMedal(user, userMedals);

        }

        private async Task CheckLikesMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var likesCount = db.Likes.Find(x => x.User.Id == user.Id).Count();

            var likeMedal = await db.Medals.Get(1);

            if (likesCount >= 10 && !userMedals.Contains(likeMedal))
            {
                userMedals.Add(likeMedal);
            }
            else if (likesCount < 10 && userMedals.Contains(likeMedal))
            {
                userMedals.Remove(likeMedal);
            }
        }

        private async Task CheckCommentsMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var commentsMedal = await db.Medals.Get(2);

            var commentsCount = db.Comments.Find(x => x.User.Id == user.Id).Count();

            if (commentsCount >= 10 && !userMedals.Contains(commentsMedal))
            {
                userMedals.Add(commentsMedal);
            }
            else if (commentsCount < 10 && userMedals.Contains(commentsMedal))
            {
                userMedals.Remove(commentsMedal);
            }
        }

        private async Task CheckCreativesMedal(ApplicationUser user, ICollection<Medal> userMedals)
        {
            var creativesMedal = await db.Medals.Get(3);

            var creativeCount = db.Creatives.Find(x => x.User.Id == user.Id).Count();

            if (creativeCount >= 10 && !userMedals.Contains(creativesMedal))
            {
                userMedals.Add(creativesMedal);
            }
            else if (creativeCount < 10 && userMedals.Contains(creativesMedal))
            {
                userMedals.Remove(creativesMedal);
            }
        }
    }
}
