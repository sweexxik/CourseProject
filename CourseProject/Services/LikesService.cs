﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Services
{
    public class LikesService : ILikesService
    {
        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;
        private readonly ICommentsService commentsService;

        public LikesService(IUnitOfWork repo, IMedalService service, ICommentsService commentsServ)
        {
            db = repo;
            medalService = service;
            commentsService = commentsServ;
        }

        public async Task<IEnumerable<NewLikeModel>> GetLikes(int commentId)
        {
            return InitLikesModel(await db.Comments.Get(commentId));
        }

        public async Task<NewCommentModel> AddLike(NewLikeModel model)
        {
            var user = await db.FindUser(model.UserName);

            if (db.Likes.GetAll().ToList().Any(like => like.CommentId == model.CommentId && like.User == user))
            {
                RemoveLike(model, user);
            }
            else
            {
                db.Likes.Add(new Like { CommentId = model.CommentId, User = user });
            }

            db.Save();

            var comment = db.Comments.Find(x => x.Id == model.CommentId);

            await medalService.CheckMedals(user.UserName);

            return commentsService.InitCommentsModel(comment).First();
        }

        private async void RemoveLike(NewLikeModel model, ApplicationUser user)
        {
            var likes = db.Likes.Find(x => x.CommentId == model.CommentId);

            if (likes == null) return;
            
            foreach (var like in likes.Where(like => like.User.Id == user.Id))
            {
                await db.Likes.Remove(like.Id);
            }
        }

        private IEnumerable<NewLikeModel> InitLikesModel(Comment comment)
        {
            if (comment == null) return new List<NewLikeModel>();

            return comment.Likes.Select(like => new NewLikeModel
            {
                Id = like.Id,
                UserName = like.User.UserName,
                CommentId = like.CommentId

            }).ToList();
        }
    }
}