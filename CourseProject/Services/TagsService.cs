using System.Collections.Generic;
using System.Linq;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Services
{
    public class TagsService : ITagsService
    {
        private readonly IUnitOfWork db;

        public TagsService()
        {
            db = new EfUnitOfWork();
        }

        public IEnumerable<TagsViewModel> GetTags()
        {
            var tagCollection = db.Tags.GetAll().ToList();

            var result = new List<TagsViewModel>();

            var tagGroups = tagCollection.GroupBy(x => x.Name);

            foreach (var tagGroup in tagGroups)
            {
                var firstTag = tagGroup.First();

                result.Add(new TagsViewModel
                {
                    Id = firstTag.Id,
                    Name = firstTag.Name,
                    Count = tagGroup.Count()
                });
            }

            return result;
        }

        public IEnumerable<TagsViewModel> GetTags(List<Tag> inputTags)
        {
            var result = new List<TagsViewModel>();

            var allTags = db.Tags.GetAll().ToList();

            var tagGroups = inputTags.GroupBy(x => x.Name);

            foreach (var tagGroup in tagGroups)
            {
                var firstTag = tagGroup.First();

                result.Add(new TagsViewModel
                {
                    Id = firstTag.Id,
                    Name = firstTag.Name,
                    Count = allTags.Count(x=>x.Name == firstTag.Name)
                });
            }

            return result;
        }
    }

    class Compare : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            return x.Name == y.Name;
        }
        public int GetHashCode(Tag codeh)
        {
            return codeh.GetHashCode();
        }
    }
}