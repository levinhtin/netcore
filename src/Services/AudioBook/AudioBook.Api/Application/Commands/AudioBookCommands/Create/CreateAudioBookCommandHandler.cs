using AudioBook.Core.Entities;
using AudioBook.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AudioBook.Api.Application.Commands.AudioBookCommands.Create
{
    public class CreateAudioBookCommandHandler : IRequestHandler<CreateAudioBookCommand, int>
    {
        private readonly IAudioBookRepository _audioBookRepository;
        private readonly IAudioBookTrackRepository _audioBookTrackRepository;

        public CreateAudioBookCommandHandler(IAudioBookRepository audioBookRepository, IAudioBookTrackRepository audioBookTrackRepository)
        {
            _audioBookRepository = audioBookRepository;
            _audioBookTrackRepository = audioBookTrackRepository;
        }

        public async Task<int> Handle(CreateAudioBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var audioBook = new AudioBookInfo()
                        {
                            Name = request.Name,
                            Description = request.Description,
                            ImageBackground = request.ImageBackground,
                            Views = 0,
                            Rate = 0,
                            Duration = 0,
                            CreatedAt = request.RequestAt,
                            CreatedBy = request.RequestBy
                        };

                        var id = await _audioBookRepository.InsertAsync(audioBook);

                        foreach (var track in request.Tracks)
                        {
                            var audioBookTrack = new AudioBookTrack()
                            {
                                Name = track.Name,
                                Description = track.Description,
                                PathFile = track.PathFile,
                                Duration = track.Duration,
                                AudioBookId = id,
                                CreatedAt = request.RequestAt,
                                CreatedBy = request.RequestBy
                            };

                            await _audioBookTrackRepository.InsertAsync(audioBookTrack);
                        }

                        scope.Complete();
                        scope.Dispose();

                        return await Task.FromResult<int>(id);
                    }
                    catch
                    {
                        scope.Dispose();
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
