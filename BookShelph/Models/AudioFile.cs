using System;
using System.Collections.Generic;

#nullable disable

namespace BookShelph.Models
{
    public partial class AudioFile
    {
        public AudioFile()
        {
            Books = new HashSet<Book>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal FileSize { get; set; }
        public string FilePath { get; set; }
        public TimeSpan? Duration { get; set; }
        public string NormalizedName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
