using System.Web.Http;
using CourseProject.Filters;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    [Authorize]
    public class AdminController : BaseApiController
    {
        private readonly IAdminService service;

        public AdminController(IAdminService serv)
        {
            service = serv;
        }

        [HttpGet]
        [Route("api/admin/users")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(service.GetUsers());
        }

        [HttpGet]
        [Route("api/admin/medals")]
        public IHttpActionResult GetAllMedals()
        {
            return Ok(service.GetMedals());
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/save")]
        public IHttpActionResult SaveUserData(UserViewModel model)
        {
            return Ok(service.SaveUserData(model));
        }

    }
}
