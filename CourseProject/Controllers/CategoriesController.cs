using System.Collections.Generic;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.Repositories;

namespace CourseProject.Controllers
{
    [AllowAnonymous]
    public class CategoriesController : ApiController
    {
        private readonly IUnitOfWork db;

        public CategoriesController()
        {
            db = new EfUnitOfWork();
        }

        // GET: api/Categories
        public IEnumerable<CreativeCategory> GetCategories()
        {
            return db.Categories.GetAll();
        }
        
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