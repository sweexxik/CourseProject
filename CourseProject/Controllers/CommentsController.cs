using System;
using System.Collections.Generic;
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
    public class CommentsController : ApiController
    {
        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;

        public CommentsController()
        {
            db = new EfUnitOfWork();
            medalService = new MedalService();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/comments/{id}")]
        public IHttpActionResult GetComments(int id)
        {
            var list = db.Comments.Find(x => x.CreativeId == id).ToList();

            var comments = InitCommentsModel(list);

            return Ok(comments);
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> AddComment(NewCommentModel model)
        {
          
            var comment = await InitNewComment(model);
           
            db.Comments.Create(comment);

            db.Save();

            await medalService.CheckMedals(model.UserName);

            return Ok(InitCommentsModel(db.Comments.Find(x=>x.CreativeId == comment.CreativeId)));
        }

        [Authorize]
        [HttpPost]
        [Route("api/comments/delete/{id}")]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            var comm = await db.Comments.Get(id);

            var userName = comm.User.UserName;

            var comment = await db.Comments.Delete(id);

            if (comment == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();
            
            await medalService.CheckMedals(userName);

            var comments = db.Comments.Find(x => x.CreativeId == comment.CreativeId);

            var result = InitCommentsModel(comments);

            return Ok(result);
        }

        private async Task<Comment> InitNewComment(NewCommentModel model)
        {
            return new Comment
            {
              CreativeId = model.CreativeId,
              Text = model.Text,
              User = await db.FindUser(model.UserName),
              PostDate = DateTime.Now
            };
        }

        public static List<NewCommentModel> InitCommentsModel(IEnumerable<Comment> list)
        {
            try
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
                    Likes = likes,
                    PostDate = comment.PostDate
                });
            }
            return comments;
            }
            catch (Exception e )
            {
                throw new Exception();
               
            }
        }
    }
}
