using File.Api.Services.Interfaces;
using File.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace File.Api.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Add a file
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <param name="file">File upload</param>
        /// <returns>FileView Model</returns>
        public async Task<FileEntity> Save(string pathFile, IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    Guid identity = Guid.NewGuid();

                    var fileUploadResult = new FileEntity()
                    {
                        Id = identity,
                        OriginalName = file.FileName,
                        Type = file.ContentType,
                        Length = file.Length,
                        Extension = Path.GetExtension(file.FileName),
                        Path = pathFile
                    };

                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return fileUploadResult;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
