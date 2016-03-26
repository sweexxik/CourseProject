using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<NewRatingModel>> AddRating(NewRatingModel model);
    }
}