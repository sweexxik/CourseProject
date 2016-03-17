using System.Collections.Generic;

namespace CourseProject.Domain
{
    public class Comment
    {
        public Comment()
        {
           Likes = new List<Like>();
        }

        public int Id { get; set; }

        public int  UserId { get; set; }

        public string Text { get; set; }

        public List<Like> Likes { get; set; }


    }
}