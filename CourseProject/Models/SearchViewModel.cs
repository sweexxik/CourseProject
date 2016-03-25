using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class SearchViewModel
    {
        [Required]
        public string  Pattern { get; set; }
    }
}