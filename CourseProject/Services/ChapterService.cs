using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Services
{
    public class ChapterService : IChaptersService
    {
        private readonly IUnitOfWork db;

        public ChapterService(IUnitOfWork repo)
        {
            db = repo;
        }

        public async Task<bool> DeleteChapter(int chapterId)
        {
            var result = await db.Chapters.Remove(chapterId);

            if (result)
            {
                db.Save();
                return true;
            }

            return false;


        }

        public async Task SetChaptersPositions(IEnumerable<NewChapterModel> chapters)
        {
            //todo check if one of chapters has null field

            foreach (var chapter in chapters)
            {
                var ch = await db.Chapters.Get(chapter.Id);
                ch.Number = chapter.Number;
            }

            db.Save();


        }

        public bool AddOrUpdateChapter(NewChapterModel model)
        {
            var chapter = InitChapter(model);

            AddOrUpdateChapter(model, chapter);

            db.Save();

            return true;

        }

        public async Task<NewChapterModel> GetChapter(int id)
        {
            return InitViewModel(await db.Chapters.Get(id));
        }

        private void AddOrUpdateChapter(NewChapterModel model, Chapter chapter)
        {
            if (model.Id == 0)
            {
                db.Chapters.Add(chapter);
            }
            else
            {
                db.Chapters.Update(chapter);
            }

            db.Save();
        }


        private static Chapter InitChapter(NewChapterModel model)
        {

            return new Chapter
            {
                Id = model.Id,
                Name = model.Name,
                Body = model.Text,
                Number = model.Number,
                CreativeId = model.CreativeId,
                Created = DateTime.Now
            };
        }

        private static NewChapterModel InitViewModel(Chapter chapter)
        {
            if (chapter != null)
            {
                return new NewChapterModel
                {
                    Id = chapter.Id,
                    CreativeId = chapter.CreativeId,
                    Name = chapter.Name,
                    Number = chapter.Number,
                    Text = chapter.Body,
                    CreatedOn = chapter.Created.ToString(CultureInfo.CurrentCulture),
                    Edit = false
                };
            }

            return new NewChapterModel();
            
        }
    }
}
