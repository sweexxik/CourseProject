using System.Data.Entity;
using CourseProject.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.UserEntities
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
        }

        public DbSet<Creative> Creatives { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<CreativeCategory> Categories { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Medal> Medals { get; set; }
    }
}