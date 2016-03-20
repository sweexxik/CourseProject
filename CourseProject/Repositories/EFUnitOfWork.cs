using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CourseProject.Domain;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using CourseProject.UserEntities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CourseProject.Repositories
{
    //todo apiController???
    public class EfUnitOfWork : ApiController, IUnitOfWork
    {
        private readonly AuthContext db;

        private CreativesRepository creativesRepository;
        private CommentsRepository commentsRepository;
        private LikesRepository likesRepository;
        private ChaptersRepository chaptersRepository;
        private CreativeCategoryRepository categoryRepository;
        private readonly UserManager<IdentityUser> userManager;
     

        private bool isDisposed;

        public EfUnitOfWork()
        {
            db = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
        }
        
        public IRepository<Creative> Creatives
        {
            get
            {
                return creativesRepository ?? (creativesRepository = new CreativesRepository(db));
            }
        }

        public IRepository<CreativeCategory> Categories
        {
            get
            {
                return categoryRepository ?? (categoryRepository = new CreativeCategoryRepository(db));
            }
        }


        public IRepository<Comment> Comments
        {
            get
            {
                return commentsRepository ?? (commentsRepository = new CommentsRepository(db));
            }
        }

        public IRepository<Chapter> Chapters
        {
            get
            {
                return chaptersRepository ?? (chaptersRepository = new ChaptersRepository(db));
            }
        }

        public IRepository<Like> Likes
        {
            get
            {
                return likesRepository ?? (likesRepository = new LikesRepository(db));
            }
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            return await userManager.CreateAsync(user, userModel.Password);

           
        }

        public async Task<IdentityUser> FindUser(string userName)
        {
            try
            {
             var user =  await userManager.FindByNameAsync(userName);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            return await userManager.FindAsync(userName, password);
           
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                db.Dispose();
                userManager.Dispose();
            }

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}