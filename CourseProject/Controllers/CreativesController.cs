using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;



namespace CourseProject.Controllers
{
    public class CreativesController : ApiController
    {
        private IUnitOfWork db = new EfUnitOfWork();

        [HttpGet]
        [Route("api/creatives/getall")]
        public IHttpActionResult GetAllCreatives()
        {
            return Ok(db.Creatives.GetAll());
        }

        [HttpGet]
        [Route("api/creatives/getall/{username}")]
        public async Task<IHttpActionResult> GetCreatives(string userName)
        {
            var user = await db.FindUser(userName);

            return Ok(db.Creatives.Find(x => x.UserId == user.Id.ToString()));
        }


        [HttpGet]
        [Route("api/creatives/{id}")]
        public async Task<IHttpActionResult> GetCreative(int id)
        {
            var creative = await db.Creatives.Get(id);

            return Ok(creative);
        }

        [HttpPost]
        [Route("api/creatives/delete/{id}")]
        public async Task<IHttpActionResult> DeleteCreative(int id)
        {
            var item = await db.Creatives.Delete(id);

            if (item == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();

            return Ok(new { status = "200" });
        }

        
        public async Task<IHttpActionResult> PostCreative(NewCreativeModel model)
        {
            var creative = await InitNewCreative(model);

            db.Creatives.Create(creative);

            db.Save();

            return Ok(new {status = "200"} );
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<Creative> InitNewCreative(NewCreativeModel model)
        {
            var creative = new Creative
            {
                Name = model.Name,
                Rating = 0,
                Category = await db.Categories.Get(model.CategoryId)
            };

            var user = await db.FindUser(model.UserName);

            creative.UserId = user.Id;

            return creative;
        }
    }
}