﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure;
using AudioBook.Infrastructure.Repositories.Interfaces;

namespace AudioBook.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Count all Category paging
        /// </summary>
        /// <param name="search">Username to get devices</param>
        /// <returns>A collection of device info</returns>
       Task<int> CountAllAsync(string search);

        /// <summary>
        /// Get all Category paging
        /// </summary>
        /// <param name="page">Username to get devices</param>
        /// <param name="limit">Username to get devices</param>
        /// <param name="search">Username to get devices</param>
        /// <returns>A collection of device info</returns>
        Task<IEnumerable<Category>> GetAllPagingAsync(int page, int limit, string search);
    }
}
