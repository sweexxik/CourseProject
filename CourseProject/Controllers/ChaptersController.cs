using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    public class ChaptersController : ApiController
    {
        private IUnitOfWork db = new EfUnitOfWork();

        // GET: api/Chapters
        public IEnumerable<Chapter> GetCategories()
        {
            return db.Chapters.GetAll();
        }

        [ResponseType(typeof(Chapter))]
        public async Task<IHttpActionResult> PostCreative(AddOrUpdateChapterModel model)
        {
            var chapter = new Chapter
            {
                Id = model.Id,
                Name = model.Name,
                Body = model.Body,
                Number = model.Number,
                CreativeId = model.CreativeId
            };
            
            db.Chapters.Update(chapter);

            db.Save();

            return Ok(new { status = "200" });
        }


    }
}