using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ICreativeService
    {
        Task<IEnumerable<NewCreativeModel>> UpdateCreative(NewCreativeModel model);

        Task<IEnumerable<NewCreativeModel>> CreateCreative(NewCreativeModel model);

        Task<IEnumerable<NewCreativeModel>> DeleteCreative(int id);

        Task<IEnumerable<NewCreativeModel>> SearchCreatives(SearchViewModel model);

        Task<NewCreativeModel> GetCreativeById(int id);

        Task<IEnumerable<NewCreativeModel>> GetUsersCreatives(string userName);

        IEnumerable<NewCreativeModel> GetAllCreatives();

        void Dispose(bool disposing);

    }
}