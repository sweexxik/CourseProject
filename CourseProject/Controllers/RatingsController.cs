using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    public class RatingsController : ApiController
    {
        private readonly IUnitOfWork db;

        public RatingsController()
        {
            db = new EfUnitOfWork();
        }
       
        [HttpPost]
        [Authorize]
        [Route("api/rating")]
        public async Task<IHttpActionResult> AddRating(NewRatingModel model)
        {
            var user = await db.FindUser(model.UserName);

            if (db.Ratings.GetAll().ToList().Any(x => x.User == user && x.CreativeId == model.CreativeId))
            {
                return BadRequest("Duplicate rating assignment");
            }

            var rating = new Rating
            {
                CreativeId = model.CreativeId,
                Value = model.Value,
                User = await db.FindUser(model.UserName)
            };

            db.Ratings.Create(rating);

            db.Save();
            
            return Ok(db.Ratings.Find(x=>x.CreativeId == model.CreativeId));
        }
    }
}
