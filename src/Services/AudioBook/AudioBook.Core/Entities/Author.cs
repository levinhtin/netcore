﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBook.Core.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
