using AudioBook.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Queries.ẠudioBookQueries.Paging
{
    public class AudioBookPagingQueryHandler : IRequestHandler<AudioBookPagingQuery, PagedData<AudioBookPagingDto>>
    {
        public Task<PagedData<AudioBookPagingDto>> Handle(AudioBookPagingQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
