using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface IChaptersService
    {
        Task<NewChapterModel> GetChapter(int chapterId);

        Task<IEnumerable<NewChapterModel>> DeleteChapter(int chapterId);
        
        bool AddOrUpdateChapter(NewChapterModel model);
    }
}
