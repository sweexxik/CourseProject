using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class TagsViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Count { get; set; }

        public int?  CreativeId { get; set; }
    }
}