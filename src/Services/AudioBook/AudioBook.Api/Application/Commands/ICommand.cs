using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.Api.Application.Commands
{
    public abstract class ICommand
    {
        [JsonIgnore]
        public string RequestBy { get; set; }

        [JsonIgnore]
        public DateTime RequestAt { get; set; }
    }
}
