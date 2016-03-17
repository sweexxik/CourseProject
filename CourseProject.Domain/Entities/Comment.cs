using System.Collections.Generic;

namespace CourseProject.Domain.Entities
{
    public class Comment
    {
        public Comment()
        {
           Likes = new List<Like>();
        }

        public int Id { get; set; }

        public string  UserId { get; set; }

        public int CreativeId { get; set; }

        public string Text { get; set; }

        public List<Like> Likes { get; set; }


    }
}