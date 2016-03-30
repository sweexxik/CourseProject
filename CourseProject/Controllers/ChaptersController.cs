using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Filters;
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
        [ValidateViewModel]
        [Route("api/chapters")]
        public IHttpActionResult AddOrUpdateChapter(NewChapterModel model)
        {
            service.AddOrUpdateChapter(model);

            return Ok(model);
        }
        
        [HttpPost]
        [ValidateViewModel]
        [Route("api/chapters/all")]
        public async Task<IHttpActionResult> PostAllChapters([FromBody] List<NewChapterModel> model)
        {
            //todo test it

            await service.SetChaptersPositions(model);

            return Ok(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/chapters/delete/{chapterId}")]
        public async Task<IHttpActionResult> DeleteChapter(int chapterId)
        {
            var result = await service.DeleteChapter(chapterId);

            return result != null ? Ok(result) : GetErrorResult(false);
        }
    }
}