using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CourseProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.UserEntities
{
    public class AuthRepository : IDisposable
    {
        private readonly AuthContext db;

        private readonly UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            db = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            db.Dispose();
            userManager.Dispose();

        }
    }
}