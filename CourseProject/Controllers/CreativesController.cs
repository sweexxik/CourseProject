using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Services;

namespace CourseProject.Controllers
{
    public class CreativesController : ApiController
    {
    
        private readonly IMedalService medalService;
        private readonly ICreativeService creativeService;
       

        public CreativesController()
        {
            medalService = new MedalService();
            creativeService = new CreativeService();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/creatives/search")]
        public async Task<IHttpActionResult> Search(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await creativeService.SearchCreatives(model.Pattern));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/creatives/getall")]
        public IHttpActionResult GetAllCreatives()
        {
           return Ok(creativeService.GetAllCreatives());
        }

        [HttpGet]
        [Authorize]
        [Route("api/creatives/getall/{username}")]
        public async Task<IHttpActionResult> GetCreatives(string userName)
        {
            return Ok(await creativeService.GetUsersCreatives(userName));
        }


        [HttpGet]
        [Route("api/creatives/{id}")]
        public async Task<IHttpActionResult> GetCreative(int id)
        { 
            return Ok(await creativeService.GetCreativeById(id));
        }

        [HttpPost]
        [Authorize]
        [Route("api/creatives/delete/{id}")]
        public async Task<IHttpActionResult> DeleteCreative(int id)
        {
            return Ok(await creativeService.DeleteCreative(id));
        }

        [HttpPost]
        [Authorize]
        [Route("api/creatives")]
        public async Task<IHttpActionResult> CreateCreative(NewCreativeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await creativeService.CreateCreative(model));
        }

        [HttpPost]
        [Route("api/creatives/update")]
        public async Task<IHttpActionResult> UpdateCreative(NewCreativeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await creativeService.UpdateCreative(model));
        }

        protected override void Dispose(bool disposing)
        {
            creativeService.Dispose(disposing);

            base.Dispose(disposing);
        }

        

        
    }
}