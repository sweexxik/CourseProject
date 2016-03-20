using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    public class CommentsController : ApiController
    {
        private IUnitOfWork db = new EfUnitOfWork();

        [HttpGet]
        [Route("api/comments/{id}")]
        public IHttpActionResult GetComments(int id)
        {
            var list = db.Comments.Find(x=>x.CreativeId == id).ToList();

            var comments = InitCommentsModel(list);

            return Ok(comments);
        }


        [HttpPost]
        public async Task<IHttpActionResult> AddComment(NewCommentModel model)
        {
            var comment =  await InitNewComment(model);

            db.Comments.Create(comment);

            db.Save();

            return Ok(new { status = "200" });
        }

        [HttpPost]
        [Route("api/comments/delete/{id}")]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            var result = await db.Comments.Delete(id);

            if (result == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();

            return Ok(new { status = "200" });
        }

        private async Task<Comment> InitNewComment(NewCommentModel model)
        {
            return new Comment
            {
              CreativeId = model.CreativeId,
              Text = model.Text,
              User = await db.FindUser(model.UserName)
        };
        }

        private List<NewCommentModel> InitCommentsModel(List<Comment> list)
        {
           
            var comments = new List<NewCommentModel>();

            foreach (var comment in list)
            {
                var likes = new List<NewLikeModel>();

                foreach (var like in comment.Likes)
                {
                    likes.Add(new NewLikeModel
                    {
                        Id = like.Id,
                        UserName = like.User.UserName,
                        CommentId = like.CommentId
                    });
                }

                comments.Add(new NewCommentModel
                {
                    UserName = comment.User.UserName,
                    Id = comment.Id,
                    CreativeId = comment.CreativeId,
                    Text = comment.Text,
                    Likes = likes
                });
            }
            return comments;
        }
    }
}
