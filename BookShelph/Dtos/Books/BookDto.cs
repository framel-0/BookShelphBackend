using BookShelph.Dtos.AudioFiles;
using BookShelph.Dtos.Authors;
using BookShelph.Dtos.Category;
using BookShelph.Dtos.Languages;
using BookShelph.Dtos.Narrators;
using BookShelph.Dtos.Publishers;
using System;

namespace BookShelph.Dtos.Books
{
    public class BookDto
    {
        public long Id { get; set; }
        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public virtual AuthorDto Author { get; set; }
        public virtual CategoryDto Category { get; set; }
        public virtual LanguageDto Language { get; set; }
        public virtual NarratorDto Narrator { get; set; }
        public virtual PublisherDto Publisher { get; set; }
        public virtual AudioFileDto Audio { get; set; }
    }
}
