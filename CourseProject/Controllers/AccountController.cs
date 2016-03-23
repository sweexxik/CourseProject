using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;
using Microsoft.AspNet.Identity;

namespace CourseProject.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUnitOfWork db;

        public AccountController()
        {
            db = new EfUnitOfWork();
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await db.RegisterUser(userModel);

            var errorResult = GetErrorResult(result);

            return errorResult ?? Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("info/{userName}")]
        public async Task<IHttpActionResult> GetUserInfo(string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await db.FindUser(userName);

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        [Route("saveInfo")]
        public async Task<IHttpActionResult> SaveUserInfo(UpdateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await db.FindUser(model.UserName);

            var updUser = SetUserData(user, model);

            var result = await db.UpdateUser(updUser);

            var errorResult = GetErrorResult(result);

            if (model.OldPassword != null)
            {
                var resultChange = await db.ChangePassword(user.Id, model.OldPassword, model.NewPassword);

                if (errorResult == null)
                {
                    errorResult = GetErrorResult(resultChange);
                }
            }

            return errorResult ?? Ok(user);


        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private ApplicationUser SetUserData(ApplicationUser user, UpdateUserModel model)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            return user;
        }
    }
}