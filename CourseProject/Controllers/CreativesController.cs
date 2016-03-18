using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.Repositories;
using static System.String;


namespace CourseProject.Controllers
{
    public class CreativesController : ApiController
    {
        private IUnitOfWork db = new EfUnitOfWork();

        // GET: api/Creatives/5
        [ResponseType(typeof(Creative))]
        public async Task<IHttpActionResult> GetCreative()
        {
            var queryString = Request.GetQueryNameValuePairs();
            var userName = Empty;
            var creativeId = Empty;

            foreach (var pair in queryString)
            {
                if (pair.Key == "username")
                {
                    userName = pair.Value;
                }

                if (pair.Key == "id")
                {
                    creativeId = pair.Value;
                }
            }
            
            if (IsNullOrEmpty(creativeId))
            {
                var user = await db.FindUser(userName);
                return Ok(db.Creatives.Find(x => x.UserId == user.Id.ToString()));
            }

            return Ok(await db.Creatives.Get(int.Parse(creativeId)));
        }



        // POST: api/Creatives
        [ResponseType(typeof(Creative))]
        public async Task<IHttpActionResult> PostCreative(NewCreativeModel model)
        {
            var creative = new Creative();

            creative.Name = model.Name;
            creative.Rating = 0;
            creative.Category = await db.Categories.Get(model.CategoryId);
           

            var user = await db.FindUser(model.UserName);

            creative.UserId = user.Id;

            db.Creatives.Create(creative);
            db.Save();



            return Ok(); // CreatedAtRoute("DefaultApi", new { id = creative.Id }, creative);
        }

        // DELETE: api/Creatives/5
        //[ResponseType(typeof(Creative))]
        //public Task<IHttpActionResult> DeleteCreative(int id)
        //{
        //    Creative creative = db.Creatives.Find(x=>x.Id == id);
        //    if (creative == null)
        //    {
        //        return NotFound();
        //    }

        //    //db.Creatives.Remove(creative);
        //    //await db.SaveChangesAsync();

        //    return Ok(creative);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }


   
}