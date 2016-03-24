using System;
using System.Collections.Generic;
using System.Globalization;
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

        [HttpGet]
        [Route("api/chapters/{chapterId}")]
        public IHttpActionResult GetChapter(int chapterId)
        {
            var result = InitChapterViewModel(db.Chapters.Find(x => x.Id == chapterId));

            return Ok(result);
        }

        [HttpPost]
        [Route("api/chapters")]
        public IHttpActionResult AddOrUpdateChapter(NewChapterModel model)
        {
            var chapter = InitChapter(model);

            AddOrUpdateChapter(model, chapter);

            db.Save();

            return Ok(model);
        }

        [Route("api/chapters/all")]
        public async Task<IHttpActionResult> PostAllChapters([FromBody] List<Chapter> model)
        {
            foreach (var chapter in model)
            {
                var ch = await db.Chapters.Get(chapter.Id);
                ch.Number = chapter.Number;
            }

            db.Save();

            return Ok(new {status = "200"});
        }

        [HttpPost]
        [Route("api/chapters/delete")]
        public async Task<IHttpActionResult> DeleteChapter([FromBody] Chapter model)
        {
            var item = await db.Chapters.Remove(model.Id);

            if (item == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();

            return Ok(new {status = "200"});
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
                Body = model.Text,
                Number = model.Number,
                CreativeId = model.CreativeId,
                Created = DateTime.Parse(model.CreatedOn)
            };
        }

        private static List<NewChapterModel> InitChapterViewModel(IEnumerable<Chapter> chapters)
        {
            var result = new List<NewChapterModel>();

            foreach (var chapter in chapters)
            {
                result.Add(new NewChapterModel
                {
                    Id = chapter.Id,
                    CreativeId = chapter.CreativeId,
                    Name = chapter.Name,
                    Number = chapter.Number,
                    Text = chapter.Body,
                    CreatedOn = chapter.Created.ToString(CultureInfo.CurrentCulture),
                    Edit = false
                });
            }

            return result;
        }
    }
}