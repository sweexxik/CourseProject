using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly ICommentsService service;

        public CommentsController(ICommentsService serv)
        {
            service = serv;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/comments/{creativeId}")]
        public IHttpActionResult GetComments(int creativeId)
        {
            if (creativeId == 0)
            {
                return BadRequest("Creative Id is 0");
            }

            return Ok(service.GetComments(creativeId));
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> AddComment(NewCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await service.AddComment(model));
        }

        [Authorize]
        [HttpPost]
        [Route("api/comments/delete/{id}")]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            return await service.DeleteComment(id) ?  Ok(HttpStatusCode.OK) : GetErrorResult(false);
        }
    }
}

