using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;

namespace CourseProject.Domain.Context
{
    class CreativeContext : DbContext
    {
        public CreativeContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Creative> Creatives { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

    }
}
