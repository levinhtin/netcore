using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBook.Core.Entities
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        public Category(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}