using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.Repositories;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Services
{
    public class TagsService : ITagsService
    {
        private readonly IUnitOfWork db;

        public TagsService()
        {
            db = new EfUnitOfWork();
        }

        public IEnumerable<TagsViewModel> GetAllTags()
        {
            return InitTagsViewModel(db.Tags.GetAll().ToList()); 
        }

        public async Task<IEnumerable<TagsViewModel>> GetCreativeTags(int creativeId)
        {
            var creative = await db.Creatives.Get(creativeId);

            return creative != null ? InitTagsViewModel(creative.Tags) : new List<TagsViewModel>();
        }

        public IEnumerable<TagsViewModel> SaveTags(int creativeId, IEnumerable<Tag> tags)
        {
            db.Tags.RemoveRange(db.Tags.GetAll());

            db.Tags.AddRange(tags);

            db.Save();

            return InitTagsViewModel(db.Tags.Find(x => x.CreativeId == creativeId));
        }

        private IEnumerable<TagsViewModel> InitTagsViewModel(IEnumerable<Tag> inputTags)
        {
            var allTags = db.Tags.GetAll().ToList();

            var result = new List<TagsViewModel>();

            var tagGroups = inputTags.GroupBy(x => x.Name);

            foreach (var tagGroup in tagGroups)
            {
                var firstTag = tagGroup.First();

                result.Add(new TagsViewModel
                {
                    Id = firstTag.Id,
                    Name = firstTag.Name,
                    Count = allTags.Count(x => x.Name == firstTag.Name)
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