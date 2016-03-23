using System;
using System.Collections.Generic;

namespace CourseProject.Models
{
    public class NewCommentModel
    {
        public int  Id { get; set; }

        public string  Text { get; set; }

        public int  CreativeId { get; set; }

        public string UserName { get; set; }

        public DateTime PostDate { get; set; }

        public ICollection<NewLikeModel> Likes { get; set; }

    }
}