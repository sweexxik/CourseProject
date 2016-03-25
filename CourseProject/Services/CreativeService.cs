using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.LuceneEngine;
using CourseProject.Domain.LuceneEntities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;

namespace CourseProject.Services
{
    public class CreativeService : ICreativeService
    {
        private string dataFolder = @"C:\Temp\LuceneWrapper";

        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;

        public CreativeService()
        {
            db = new EfUnitOfWork();
            medalService = new MedalService();
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

            var user = currentCreative.User;

            db.Tags.RemoveRange(currentCreative.Tags);

            await db.Creatives.Remove(id);

            db.Save();

            return InitCreativesModel(db.Creatives.Find(x => x.User.Id == user.Id)));
        }

        public async Task<IEnumerable<NewCreativeModel>> SearchCreatives(string pattern)
        {
            var writer = new CreativeWriter(dataFolder);
            var searcher = new CreativeSearcher(dataFolder);

            writer.AddUpdateCreativesToIndex(db.Creatives.GetAll().ToList());

            SearchResult res = searcher.SearchCreative(pattern, string.Empty);

            var results = new List<Creative>();

            foreach (var item in res.SearchResultItems)
            {
                var creative = await db.Creatives.Get(item.Id);

                if (creative != null)
                {
                    results.Add(creative);
                }
            }

            return InitCreativesModel(results);
        }

        public async Task<Creative> GetCreativeById(int id)
        {
           return await db.Creatives.Get(id);
        }

        public async Task<IEnumerable<NewCreativeModel>> GetUsersCreatives(string userName)
        {
            var user = await db.FindUser(userName);

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


    }
}