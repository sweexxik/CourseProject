using System.Collections.Generic;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ITagsService
    {
        IEnumerable<TagsViewModel> GetTags();
        IEnumerable<TagsViewModel> GetTags(List<Tag> tags);
    }
}