using System.Collections.Generic;
using CourseProject.Domain.Entities;

namespace CourseProject.Models
{
    public class NewTagsModel
    {
        public ICollection<Tag> Tags { get; set; }

        public int CreativeId { get; set; }
    }
}