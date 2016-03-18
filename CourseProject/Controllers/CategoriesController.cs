using System.Collections.Generic;
using System.Web.Http;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Repositories;

namespace CourseProject.Controllers
{
    public class CategoriesController : ApiController
    {
        private IUnitOfWork db = new EfUnitOfWork();

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