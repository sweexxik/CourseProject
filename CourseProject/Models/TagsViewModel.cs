using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class TagsViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Count { get; set; }
    }
}