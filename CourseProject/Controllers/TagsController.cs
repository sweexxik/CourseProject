using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Repositories;
using CourseProject.Services;

namespace CourseProject.Controllers
{
    [Authorize]
    public class TagsController : ApiController
    {
        private readonly IUnitOfWork db;
        private readonly ITagsService tagService;

        public TagsController()
        {
            db = new EfUnitOfWork();

            tagService = new TagsService();
        }

        [HttpGet]
        [Route("api/tags")]
        public IHttpActionResult GetAllTags()
        {
            var tags = tagService.GetTags();

            return Ok(tags);
        }

        [HttpGet]
        [Route("api/tags/{id}")]
        public async Task<IHttpActionResult> GetTags(int id)
        {
            var creative = await db.Creatives.Get(id);

            var result = tagService.GetTags(creative.Tags.ToList());

            return Ok(result);
        }

        [HttpPost]
        [Route("api/tags/{creativeId}")]
        public IHttpActionResult SaveTags(int creativeId, List<Tag> model)
        {
            db.Tags.RemoveRange(db.Tags.GetAll());
            db.Tags.AddRange(model);  

            db.Save();

            return Ok(db.Tags.Find(x=>x.CreativeId == creativeId));
        }
    }
}
