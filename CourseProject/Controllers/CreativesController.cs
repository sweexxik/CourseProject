﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;
using CourseProject.Services;

namespace CourseProject.Controllers
{
    public class CreativesController : ApiController
    {
        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;

        public CreativesController()
        {
            db = new EfUnitOfWork();
            medalService = new MedalService();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/creatives/search")]
        public async Task<IHttpActionResult> Search(SearchViewModel model)
        {
            var list1 = await db.Creatives.Search(model.Pattern);
            var list2 = await db.Chapters.Search(model.Pattern);
            var list3 = await db.Comments.Search(model.Pattern);
            var list4 = await db.Tags.Search(model.Pattern);

            var l1 = list1.ToList();
            var l2 = list2.ToList();
            var l3 = list3.ToList();
            var l4 = list4.ToList();

            var result1 = l1.ToList().Intersect(l2).Intersect(l3);

            var result2 = l1.Union(l2).Union(l3).Union(l4);

            var result3 = result2.Distinct();



            return Ok(result3);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/creatives/getall")]
        public IHttpActionResult GetAllCreatives(){

            var creatives = InitCreativeModel(db.Creatives.GetAll().ToList());

            return Ok(creatives);
        }

        [HttpGet]
        [Authorize]
        [Route("api/creatives/getall/{username}")]
        public async Task<IHttpActionResult> GetCreatives(string userName)
        {
            var user = await db.FindUser(userName);

            var list = db.Creatives.Find(x => x.User == user).ToList();

            var creatives = InitCreativeModel(list);

            return Ok(creatives);
        }


        [HttpGet]
        [Route("api/creatives/{id}")]
        public async Task<IHttpActionResult> GetCreative(int id)
        {
            var creative = await db.Creatives.Get(id);

            var result = new NewCreativeModel
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

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("api/creatives/delete/{id}")]
        public async Task<IHttpActionResult> DeleteCreative(int id)
        {
            var currentCreative = await db.Creatives.Get(id);

            db.Tags.RemoveRange(currentCreative.Tags);

            var userId = currentCreative.User.Id;

            var userName = currentCreative.User.UserName;

            var item = await db.Creatives.Remove(id);

            if (item == null)
            {
                return BadRequest("Null reference");
            }

            db.Save();

            await medalService.CheckMedals(userName);

            var result = db.Creatives.Find(x => x.User.Id == userId);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("api/creatives")]
        public async Task<IHttpActionResult> CreateCreative(NewCreativeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creative = await InitNewCreative(model);

            db.Creatives.Add(creative);


           db.Save();

           await medalService.CheckMedals(creative.User.UserName);

           return Ok(new {status = "200"} );
        }

        [HttpPost]
        [Route("api/creatives/update")]
        public async Task<IHttpActionResult> UpdateCreative(NewCreativeModel model)
        {
            var creative = await db.Creatives.Get(model.Id);

            creative.Name = model.Name;

            creative.Description = model.Description;

            db.Creatives.Update(creative);
           
            db.Save();

            return Ok(new { status = "200" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
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

        private List<NewCreativeModel> InitCreativeModel(List<Creative> list)
        {
            var creatives = new List<NewCreativeModel>();

            foreach (var creative in list)
            {
                creatives.Add(new NewCreativeModel
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
                });
            }

            return creatives;
        }
    }
}