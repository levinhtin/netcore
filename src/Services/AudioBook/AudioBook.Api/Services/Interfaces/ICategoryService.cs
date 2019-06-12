﻿using AudioBook.Core.DTO.Request;
using AudioBook.Core.DTO.Response;
using AudioBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDetailResponse> GetById(int id);

        Task Insert(CategoryCreateRequest entity);
    }
}