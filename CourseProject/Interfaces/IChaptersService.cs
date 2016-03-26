using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface IChaptersService
    {
        Task<NewChapterModel> GetChapter(int chapterId);

        Task<bool> DeleteChapter(int chapterId);

        Task SetChaptersPositions(IEnumerable<NewChapterModel> chapters);

        bool AddOrUpdateChapter(NewChapterModel model);
    }
}
