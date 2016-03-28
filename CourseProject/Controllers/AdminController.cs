using System.Threading.Tasks;
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
        private readonly ICreativeService creativeService;

        public AdminController(IAdminService serv, ICreativeService creativeServ)
        {
            service = serv;
            creativeService = creativeServ;
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
        public async Task<IHttpActionResult> SaveUserData(UserViewModel model)
        {
            return Ok(await service.SaveUserData(model));
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/delete")]
        public async Task<IHttpActionResult> DeleteUser(UserViewModel model)
        {
            var result = GetErrorResult(await service.DeleteUser(model));

            return result ?? Ok(service.GetUsers());
        }

        [HttpPost]
        [ValidateViewModel]
        [Route("api/admin/reset")]
        public async Task<IHttpActionResult> ResetPasword(ResetPasswordModel model)
        {
            var result = GetErrorResult(await service.ResetPassword(model));

            return result ?? Ok(service.GetUsers());
        }

        [HttpPost]
        [Route("api/admin/deleteCreative/{id}")]
        public async Task<IHttpActionResult> DelteCreative(int id)
        {
            await creativeService.DeleteCreative(id);

            return Ok(creativeService.GetAllCreatives());
        }

        [HttpPost]
        [Route("api/admin/updateCreative")]
        public async Task<IHttpActionResult> UpdateCreative(NewCreativeModel model)
        {
            await creativeService.UpdateCreative(model);

            return Ok(creativeService.GetAllCreatives());
        }

    }
}
