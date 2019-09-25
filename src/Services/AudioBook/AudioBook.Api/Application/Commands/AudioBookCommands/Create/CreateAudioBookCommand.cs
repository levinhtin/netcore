using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands.AudioBookCommands.Create
{
    public class CreateAudioBookCommand : ICommand, IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBackground { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int BookReaderId { get; set; }
        public ICollection<CreateAudioBookTrackDto> Tracks { get; set; }
    }

    public class CreateAudioBookTrackDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathFile { get; set; }
        public int Duration { get; set; }
    }
}
