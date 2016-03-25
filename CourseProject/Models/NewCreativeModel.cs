using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseProject.Domain.Entities;

namespace CourseProject.Models
{
    public class NewCreativeModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public CreativeCategory Category { get; set; }

        public ICollection<Chapter> Chapters { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Rating> Rating { get; set; }

        public ICollection<Tag> Tags { get; set; } 
    }
}