﻿using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Domain.Entities
{
    public class Creative
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual CreativeCategory Category { get; set; }

        public virtual List<Chapter> Chapters { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
