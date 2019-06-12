using File.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File.Api.Services.Interfaces
{
    public interface IFileService
    {
        Task<FileEntity> Save(string path, IFormFile file);
    }
}
