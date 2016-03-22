using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
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
        public IHttpActionResult PostCreative(AddOrUpdateChapterModel model)
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
            var item = await db.Chapters.Delete(model.Id);

            if (item == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();

            return Ok(new { status = "200" });
        }


        private void AddOrUpdateChapter(AddOrUpdateChapterModel model, Chapter chapter)
        {
            if (model.Id == 0)
            {
                db.Chapters.Create(chapter);
            }
            else
            {
                db.Chapters.Update(chapter);
            }

            db.Save();
        }

        private static Chapter InitChapter(AddOrUpdateChapterModel model)
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