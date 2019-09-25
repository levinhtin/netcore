using MediatR;

namespace AudioBook.Api.Application.Queries.ẠudioBookQueries.Detail
{
    public class AudioBookDetailQuery : IRequest<AudioBookDetailDto>
    {
        public int Id { get; set; }

        public AudioBookDetailQuery(int id)
        {
            this.Id = id;
        }
    }
}
