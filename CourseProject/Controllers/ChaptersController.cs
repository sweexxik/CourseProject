using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    [Authorize]
    public class ChaptersController : ApiController
    {
        private readonly IUnitOfWork db;

        public ChaptersController()
        {
            db = new EfUnitOfWork();
        }

        public IEnumerable<Chapter> GetCategories()
        {
            return db.Chapters.GetAll();
        }


        [Route("api/chapters")]
        public IHttpActionResult PostCreative(NewChapterModel model)
        {
            var chapter = InitChapter(model);

            AddOrUpdateChapter(model, chapter);

            return Ok(new { status = "200" });
        }

        [Route("api/chapters/all")]
        public async Task<IHttpActionResult> PostAllChapters([FromBody]List<Chapter> model)
        {
            foreach (var chapter in model)
            {
                var ch = await db.Chapters.Get(chapter.Id);
                ch.Number = chapter.Number;
            }

            db.Save();

            return Ok(new { status = "200" });
        }

        [HttpPost]
        [Route("api/chapters/delete")]
        public async Task<IHttpActionResult> DeleteChapter([FromBody]Chapter model)
        {
            var item = await db.Chapters.Remove(model.Id);

            if (item == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();

            return Ok(new { status = "200" });
        }


        private void AddOrUpdateChapter(NewChapterModel model, Chapter chapter)
        {
            if (model.Id == 0)
            {
                db.Chapters.Add(chapter);
            }
            else
            {
                db.Chapters.Update(chapter);
            }

            db.Save();
        }

        private static Chapter InitChapter(NewChapterModel model)
        {
            return new Chapter
            {
                Id = model.Id,
                Name = model.Name,
                Body = model.Body,
                Number = model.Number,
                CreativeId = model.CreativeId
            };
        }


    }
}