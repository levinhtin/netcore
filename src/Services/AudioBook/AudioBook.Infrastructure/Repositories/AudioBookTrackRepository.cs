using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;

namespace AudioBook.Infrastructure.Repositories
{
    public class AudioBookTrackRepository : Repository<AudioBookTrack>, IAudioBookTrackRepository
    {
        private readonly string _connectionString;

        public AudioBookTrackRepository(AppConnections appConnection) : base(appConnection.DbContext)
        {
            this._connectionString = appConnection.DbContext;
        }
    }
}
