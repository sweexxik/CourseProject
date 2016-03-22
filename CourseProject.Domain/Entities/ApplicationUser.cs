using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime JoinDate { get; set; }

        public string AvatarUri { get; set; }

    }
}