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
        private IUnitOfWork db;

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

            IdentityResult result = await db.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
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
            var user = await db.FindUser(model.UserName);

            if (user != null)
            {
                var updUser = SetUserData(user, model);

                await db.UpdateUser(updUser);

                return Ok(user);
            }
            return BadRequest("User not found");
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