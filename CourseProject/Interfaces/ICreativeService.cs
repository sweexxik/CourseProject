﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entities;
using CourseProject.Models;

namespace CourseProject.Interfaces
{
    public interface ICreativeService
    {
        Task<IEnumerable<NewCreativeModel>> UpdateCreative(NewCreativeModel model);

        Task<IEnumerable<NewCreativeModel>> CreateCreative(NewCreativeModel model);

        Task<IEnumerable<NewCreativeModel>> DeleteCreative(int id);

        Task<IEnumerable<NewCreativeModel>> SearchCreatives(string pattern);

        Task<Creative> GetCreativeById(int id);

        Task<IEnumerable<NewCreativeModel>> GetUsersCreatives(string userName);

        IEnumerable<NewCreativeModel> GetAllCreatives();

        void Dispose(bool disposing);

    }
}