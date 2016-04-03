﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Interfaces;
using CourseProject.Interfaces;
using CourseProject.Models;

namespace CourseProject.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IUnitOfWork db;
        private readonly IMedalService medalService;

        public CommentsService(IUnitOfWork repo, IMedalService service)
        {
            db = repo;
            medalService = service;
        }

        public async Task<IEnumerable<NewCommentModel>> DeleteComment(int id)
        {
            var comm = await db.Comments.Get(id);

            if (comm == null) return null;

            var userName = comm.User.UserName;    

            var creativeId = comm.CreativeId;

            var result = await db.Comments.Remove(id);

            if (result)
            {
                db.Save();

                return InitCommentsModel(db.Comments.Find(x=>x.CreativeId == creativeId));
            }

            return null;

        }

        public async Task<IEnumerable<NewCommentModel>> AddComment(NewCommentModel model)
        {
            var comment = await InitNewComment(model);

            db.Comments.Add(comment);

            db.Save();

            //await medalService.CheckMedals(model.UserName);

            return InitCommentsModel(db.Comments.Find(x => x.CreativeId == comment.CreativeId));
        } 


        public IEnumerable<NewCommentModel> GetComments(int creativeId)
        {
           return InitCommentsModel(db.Comments.Find(x => x.CreativeId == creativeId).ToList());
        }


        private async Task<Comment> InitNewComment(NewCommentModel model)
        {
            return new Comment
            {
                CreativeId = model.CreativeId,
                Text = model.Text,
                User = await db.Users.FindUser(model.UserName),
                PostDate = DateTime.Now
            };
        }

        public IEnumerable<NewCommentModel> InitCommentsModel(IEnumerable<Comment> commentsList)
        {
            var comments = new List<NewCommentModel>();

            if (commentsList == null) return comments;

            foreach (var comment in commentsList)
            {
                var likes = new List<NewLikeModel>();

                if (comment.Likes != null)
                {
                    likes.AddRange(comment.Likes.Select(like => new NewLikeModel
                    {
                        Id = like.Id,
                        UserName = like.User.UserName,
                        CommentId = like.CommentId
                    }));
                }

                comments.Add(new NewCommentModel
                {
                    UserName = comment.User.UserName,
                    Id = comment.Id,
                    CreativeId = comment.CreativeId,
                    Text = comment.Text,
                    Likes = likes,
                    PostDate = comment.PostDate,
                    AvatarUri = comment.User.AvatarUri
                    
                });
            }
            return comments;

        }
    }
}