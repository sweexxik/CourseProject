using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class SearchViewModel
    {
        [Required]
        [MinLength(2)]
        public string  Pattern { get; set; }
    }
}