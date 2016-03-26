using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    [Authorize]
    public class ChaptersController : BaseApiController
    {
        private readonly IChaptersService service;

        public ChaptersController(IChaptersService serv)
        {
            service = serv;
        }

        [HttpGet]
        [Route("api/chapters/{chapterId}")]
        public async Task<IHttpActionResult> GetChapter(int chapterId)
        {
            return Ok(await service.GetChapter(chapterId));
        }

        [HttpPost]
        [Route("api/chapters")]
        public IHttpActionResult AddOrUpdateChapter(NewChapterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.AddOrUpdateChapter(model);

            return Ok(model);
        }

        [Route("api/chapters/all")]
        public async Task<IHttpActionResult> PostAllChapters([FromBody] List<NewChapterModel> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //todo test it

            await service.SetChaptersPositions(model);

            return Ok(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/chapters/delete/{chapterId}")]
        public async Task<IHttpActionResult> DeleteChapter(int chapterId)
        {
            return await service.DeleteChapter(chapterId) ? Ok(HttpStatusCode.OK) : GetErrorResult(false);
        }
    }
}