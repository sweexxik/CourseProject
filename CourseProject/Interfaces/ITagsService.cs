using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ITagsService
    {
        IEnumerable<TagsViewModel> GetAllTags();

        Task<IEnumerable<TagsViewModel>> GetCreativeTags(int creativeId);

        IEnumerable<TagsViewModel> SaveTags(int creativeId, IEnumerable<Tag> tags);

        IEnumerable<TagsViewModel> InitTagsViewModel(IEnumerable<Tag> inputTags, List<Tag> allTags);

        TagsViewModel InitTagViewModel(Tag tag);

    }
}