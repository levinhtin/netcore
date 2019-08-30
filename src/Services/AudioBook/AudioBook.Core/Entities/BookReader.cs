using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AudioBook.Core.Entities
{
    [Table("BookReader")]
    public class BookReader : BaseEntity
    {
        public BookReader(string firstName, string lastName, string displayName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DisplayName = displayName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
    }
}
