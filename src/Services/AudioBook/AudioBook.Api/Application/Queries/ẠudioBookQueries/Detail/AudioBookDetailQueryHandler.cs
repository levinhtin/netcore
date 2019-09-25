using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AudioBook.Api.Application.Queries.ẠudioBookQueries.Detail
{
    public class AudioBookDetailQueryHandler : IRequestHandler<AudioBookDetailQuery, AudioBookDetailDto>
    {
        public Task<AudioBookDetailDto> Handle(AudioBookDetailQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
