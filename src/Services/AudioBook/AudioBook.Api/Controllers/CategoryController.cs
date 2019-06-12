using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;

namespace AudioBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IAudioBookRepository _audioBookRepo;

        public CategoryController(ICategoryRepository categoryRepo, IAudioBookRepository audioBookRepo)
        {
            this._categoryRepo = categoryRepo;
            this._audioBookRepo = audioBookRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var category = new Category()
                        {
                            Id = 16,
                            Name = "Test 1",
                            Description = "Test description",
                            CreatedAt = DateTime.Now,
                            CreatedBy = "levinhtin@gmail.com",
                            ModifiedAt = DateTime.Now,
                            ModifiedBy = 1
                        };

                        var audioBook = new AudioBookInfo()
                        {
                            Id = 12,
                            Name = "Hành trình về phương Đông",
                            ImageBackground = string.Empty,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "levinhtin@gmail.com",
                            ModifiedAt = DateTime.Now,
                            ModifiedBy = 1
                        };

                        await this._categoryRepo.InsertAsync(category);
                        await this._audioBookRepo.InsertAsync(audioBook);

                        scope.Complete();
                        scope.Dispose();
                    }
                    catch
                    {
                        scope.Dispose();
                    }

                    return this.Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}