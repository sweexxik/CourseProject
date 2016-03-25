using System;
using System.Threading.Tasks;
using CourseProject.DbContext;
using CourseProject.Domain.Entities;
using CourseProject.Interfaces;
using CourseProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext db;

        private CreativesRepository creativesRepository;
        private CommentsRepository commentsRepository;
        private LikesRepository likesRepository;
        private ChaptersRepository chaptersRepository;
        private CreativeCategoryRepository categoryRepository;
        private RatingsRepository ratingsRepository;
        private MedalsRepository medalsRepository;
        private TagsRepository tagsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private bool isDisposed;

        public EfUnitOfWork()
        {
            db = new DatabaseContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public IRepository<Creative> Creatives
        {
            get { return creativesRepository ?? (creativesRepository = new CreativesRepository(db)); }
        }

        public IRepository<CreativeCategory> Categories
        {
            get { return categoryRepository ?? (categoryRepository = new CreativeCategoryRepository(db)); }
        }

        public IRepository<Rating> Ratings
        {
            get { return ratingsRepository ?? (ratingsRepository = new RatingsRepository(db)); }
        }

        public IRepository<Medal> Medals
        {
            get { return medalsRepository ?? (medalsRepository = new MedalsRepository(db)); }
        }

        public IRepository<Comment> Comments
        {
            get { return commentsRepository ?? (commentsRepository = new CommentsRepository(db)); }
        }

        public IRepository<Chapter> Chapters
        {
            get { return chaptersRepository ?? (chaptersRepository = new ChaptersRepository(db)); }
        }

        public IRepository<Like> Likes
        {
            get { return likesRepository ?? (likesRepository = new LikesRepository(db)); }
        }

        public IRepository<Tag> Tags
        {
            get { return tagsRepository ?? (tagsRepository = new TagsRepository(db)); }
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(string userId, string pass, string newPass)
        {
            return await userManager.ChangePasswordAsync(userId, pass, newPass);
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {

            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                JoinDate = DateTime.Now
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            return await userManager.FindAsync(userName, password);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task<bool> CheckUserRole(string userId)
        {
            return await userManager.IsInRoleAsync(userId, "Admin");
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