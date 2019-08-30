using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;

namespace AudioBook.Infrastructure.Repositories
{
    public class BookReaderRepository : Repository<BookReader>, IBookReaderRepository
    {
        private readonly string _connectionString;

        public BookReaderRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }

    }
}