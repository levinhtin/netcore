using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AudioBook.Api.Application
{
    public abstract class RequestAudit
    {
        [BindNever]
        public string RequestBy { get; set; }

        [BindNever]
        public DateTime RequestAt { get; set; }
    }
}
