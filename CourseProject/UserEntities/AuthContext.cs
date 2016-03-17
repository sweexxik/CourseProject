using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.UserEntities
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}