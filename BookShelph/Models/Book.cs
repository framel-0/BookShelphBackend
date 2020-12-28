using System;
using System.Collections.Generic;

#nullable disable

namespace BookShelph.Models
{
    public partial class Book
    {
        public long Id { get; set; }
        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int CategoryId { get; set; }
        public long PublisherId { get; set; }
        public long AuthorId { get; set; }
        public int LanguageId { get; set; }
        public long? NarratorId { get; set; }
        public long? AudioId { get; set; }
        public bool IsActive { get; set; }
        public int? NumberOfDownload { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual AudioFile Audio { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
        public virtual Narrator Narrator { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
