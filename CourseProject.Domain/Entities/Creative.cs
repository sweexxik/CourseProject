using System.Collections.Generic;

namespace CourseProject.Domain.Entities
{
    public class Creative
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  string Category { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }

        public virtual List<Chapter> Chapters { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
