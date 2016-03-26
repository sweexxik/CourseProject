using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ILikesService
    {
        Task<IEnumerable<NewLikeModel>> GetLikes(int commentId);
        Task<NewCommentModel> AddLike(NewLikeModel model);
    }
}
