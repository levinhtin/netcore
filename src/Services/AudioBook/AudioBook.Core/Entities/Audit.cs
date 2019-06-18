﻿using System;

namespace AudioBook.Core.Entities
{
    public class Audit
    {
        // A new SessionId that will be used to link an entire
        // users "Session" of Audit Logs together to help
        // identifier patterns involving erratic behavior
        public string SessionID { get; set; }

        public Guid AuditID { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string URLAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }

        // A new Data property that is going to store JSON
        // string objects that will later be able to be
        // deserialized into objects if necessary to view
        // details about a Request
        public string Data { get; set; }
    }
}