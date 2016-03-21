using System;
using System.Collections.Generic;
using CourseProject.Domain.Entities;

namespace CourseProject.Models
{
    public class NewCommentModel
    {
        public int  Id { get; set; }
        public string  Text { get; set; }
        public int  CreativeId { get; set; }
        public string UserName { get; set; }
        public DateTime PostDate { get; set; }
        public List<NewLikeModel> Likes { get; set; }

    }
}