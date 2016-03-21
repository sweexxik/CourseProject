using System.Collections.Generic;
using CourseProject.Domain.Entities;

namespace CourseProject.Models
{
    public class NewCreativeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string UserName { get; set; }

        public List<Chapter> Chapters { get; set; }
        public List<Comment> Comments { get; set; }
        public CreativeCategory Category { get; set; }
        public List<Rating> Rating { get; set; }
    }
}