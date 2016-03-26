using System;
using System.Reflection;
using CourseProject.Domain.Interfaces;
using CourseProject.Domain.Repositories;
using CourseProject.Interfaces;
using CourseProject.Services;
using Ninject;

namespace CourseProject
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            StandardKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterServices(KernelBase kernel)
        {
            kernel.Bind<IUnitOfWork>().To<EfUnitOfWork>();
            kernel.Bind<ICreativeService>().To<CreativeService>();
            kernel.Bind<IMedalService>().To<MedalService>();
            kernel.Bind<ITagsService>().To<TagsService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<ICommentsService>().To<CommentsService>();
            kernel.Bind<IChaptersService>().To<ChapterService>();
            kernel.Bind<ILikesService>().To<LikesService>();
            kernel.Bind<IRatingService>().To<RatingService>();
            

        }
    }
}