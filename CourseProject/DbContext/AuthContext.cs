﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using CourseProject.Domain.Entities;
using Fissoft.EntityFramework.Fts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.DbContext
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
            DbInterception.Add(new FtsInterceptor());
        }

        public DbSet<Creative> Creatives { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<CreativeCategory> Categories { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Medal> Medals { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}