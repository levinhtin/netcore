using AudioBook.Core.Entities;
using AudioBook.Infrastructure;
using AudioBook.Infrastructure.Repositories.Interfaces;

namespace AudioBook.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }
    }
}
