using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int CreativeId { get; set; }
        public string Text { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}