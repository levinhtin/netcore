using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBook.Core.Entities
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        public Author(string name, string description, DateTime? dateOfBirth)
        {
            this.Name = name;
            this.Description = description;
            this.DateOfBirth = dateOfBirth;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}