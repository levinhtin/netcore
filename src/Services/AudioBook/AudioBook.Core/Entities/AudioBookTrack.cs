using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AudioBook.Core.Entities
{
    [Table("AudioBookTrack")]
    public class AudioBookTrack : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathFile { get; set; }
        public int Duration { get; set; }
        public int AudioBookId { get; set; }
        public int? ReaderId { get; set; }
    }
}
