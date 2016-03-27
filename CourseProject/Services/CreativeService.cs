using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.LuceneEngine;
using CourseProject.Domain.LuceneEntities;
using CourseProject.Interfaces;
using CourseProject.Models;
namespace CourseProject.Services
{
    public class CreativeService : ICreativeService
    {
        private string dataFolder = @"C:\Temp\LuceneWrapper";

        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;

        public CreativeService(IUnitOfWork repo, IMedalService medal)
        {
            db = repo;
            medalService = medal;
        }

        public async Task<IEnumerable<NewCreativeModel>> UpdateCreative(NewCreativeModel model)
        {
            var creative = await db.Creatives.Get(model.Id);

            creative.Name = model.Name;

            creative.Description = model.Description;

            db.Creatives.Update(creative);

            db.Save();

            return InitCreativesModel(db.Creatives.Find(c => c.User.UserName == model.UserName));
        }

        public async Task<IEnumerable<NewCreativeModel>> CreateCreative(NewCreativeModel model)
        {
            var creative = await InitNewCreative(model);

            db.Creatives.Add(creative);

            db.Save();

            await medalService.CheckMedals(creative.User.UserName);

            return InitCreativesModel(db.Creatives.Find(c => c.User.UserName == model.UserName));
        }
     

        public async Task<IEnumerable<NewCreativeModel>> DeleteCreative(int id)
        {
            var currentCreative = await db.Creatives.Get(id);

            if (currentCreative == null)
            {
                return null;
            }

            var userId = currentCreative.User.Id;

            db.Tags.RemoveRange(currentCreative.Tags);

            var result = await db.Creatives.Remove(id);

            if (!result) return null;

            db.Save();

            return InitCreativesModel(db.Creatives.Find(x=>x.User.Id == userId)) ;
        }

        public async Task<IEnumerable<NewCreativeModel>> SearchCreatives(SearchViewModel model)
        {
            var results = new List<Creative>();

            try
            {
                var writer = new CreativeWriter(dataFolder);

                var searcher = new CreativeSearcher(dataFolder);

                writer.AddUpdateCreativesToIndex(db.Creatives.GetAll().ToList());

                var searchResults = GetSearchResults(model, searcher);

                foreach (var searchResult in searchResults)
                {
                    foreach (var item in searchResult.SearchResultItems)
                    {
                        var creative = await db.Creatives.Get(item.Id);

                        if (creative != null && !results.Contains(creative))
                        {
                            results.Add(creative);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
           

            return InitCreativesModel(results);
        }

        public async Task<Creative> GetCreativeById(int id)
        {
            var result = await db.Creatives.Get(id);

            return result;
        }

        public async Task<IEnumerable<NewCreativeModel>> GetUsersCreatives(string userName)
        {
            var user = await db.FindUser(userName);

            if (user == null) return null;

            var list = db.Creatives.Find(x => x.User == user).ToList();

            return InitCreativesModel(list);
        }

        public IEnumerable<NewCreativeModel> GetAllCreatives()
        {
            return InitCreativesModel(db.Creatives.GetAll().ToList());
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }


        private async Task<Creative> InitNewCreative(NewCreativeModel model)
        {
            var creative = new Creative
            {
                Name = model.Name,
                Description = model.Description,
                Category = await db.Categories.Get(model.CategoryId),
                Tags = model.Tags
            };

            var user = await db.FindUser(model.UserName);

            creative.User = user;

            return creative;
        }

        public NewCreativeModel InitCreativeModel(Creative creative)
        {
          return new NewCreativeModel
            {
                Id = creative.Id,
                Chapters = creative.Chapters,
                Comments = creative.Comments,
                UserName = creative.User.UserName,
                Name = creative.Name,
                Description = creative.Description,
                Category = creative.Category,
                Rating = creative.Rating,
                Tags = creative.Tags
            };
        }

        private IEnumerable<NewCreativeModel> InitCreativesModel(IEnumerable<Creative> list)
        {
            return list.Select(creative => new NewCreativeModel
            {
                Id = creative.Id,
                Chapters = creative.Chapters,
                Comments = creative.Comments,
                UserName = creative.User.UserName,
                Name = creative.Name,
                Description = creative.Description,
                Category = creative.Category,
                Rating = creative.Rating,
                Tags = creative.Tags
            }).ToList();
        }

        private IEnumerable<SearchResult> GetSearchResults(SearchViewModel model, CreativeSearcher searcher)
        {
            var searchResults = new List<SearchResult>();

            if (model.ChapterText && model.CommentAuthor && model.CommentText && model.CreativeAuthor
               && model.CreativeDescription && model.CreativeName && model.TagName && model.ChapterName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, string.Empty));
                return searchResults;
            }

            if (model.ChapterName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "ChapterNames"));
            }

            if (model.ChapterText)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "ChapterBodies"));
            }

            if (model.CommentAuthor)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "CommentsUserName"));
            }

            if (model.CommentText)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Comments"));
            }

            if (model.CreativeAuthor)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "UserName"));
            }

            if (model.CreativeDescription)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Description"));
            }

            if (model.CreativeName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Name"));
            }

            if (model.TagName)
            {
                searchResults.Add(searcher.SearchCreative(model.Pattern, "Tags"));
            }

            return searchResults;
            
        } 


    }
}