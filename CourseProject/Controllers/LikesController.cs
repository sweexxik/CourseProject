using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    public class LikesController : ApiController
    {
        private IUnitOfWork db = new EfUnitOfWork();

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
                return BadRequest("Duplicate like not allowed");
            }

            db.Likes.Create(new Like { CommentId = model.CommentId, User = user});

            db.Save();
           
            return Ok(new { status = "200" });
        }

        [HttpPost]
        [Route("api/likes/delete/{id}")]
        public async Task<IHttpActionResult> DeleteLike(int id)
        {
            var result = await db.Likes.Delete(id);

            if (result == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();

            return Ok(new { status = "200" });
        }
    }
}
