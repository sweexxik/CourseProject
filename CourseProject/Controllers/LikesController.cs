using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;
using CourseProject.Services;

namespace CourseProject.Controllers
{
    [Authorize]
    public class LikesController : ApiController
    {
        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;

        public LikesController()
        {
            db = new EfUnitOfWork();
            medalService = new MedalService();
        }

        [HttpGet]
        [Route("api/likes/{id}")]
        public async Task<IHttpActionResult> GetLikes(int id)
        {
            var comment = await db.Comments.Get(id);
            
            return Ok(comment.Likes);
        }

        [HttpPost]
        [Route("api/likes")]
        public async Task<IHttpActionResult> AddLike(NewLikeModel model)
        {
            var user = await db.FindUser(model.UserName);

            if (db.Likes.GetAll().ToList().Any(like => like.CommentId == model.CommentId && like.User == user))
            {
                RemoveLike(model, user);
            }
            else
            {
                db.Likes.Add(new Like { CommentId = model.CommentId, User = user });
            }

            db.Save();

            var comment = db.Comments.Find(x=>x.Id == model.CommentId);

            await medalService.CheckMedals(user.UserName);

            var result = CommentsController.InitCommentsModel(comment).First();

            return Ok(result);
        }
        
        private async void RemoveLike(NewLikeModel model, ApplicationUser user)
        {
            var likes = db.Likes.Find(x => x.CommentId == model.CommentId);

            foreach (var like in likes)
            {
                if (like.User.Id == user.Id)
                {
                    await db.Likes.Remove(like.Id);
                }
            }
        }
    }
}
