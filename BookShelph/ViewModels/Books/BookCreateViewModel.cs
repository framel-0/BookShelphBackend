using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Books
{
    public class BookCreateViewModel
    {
        [DataType(DataType.Upload)]
        public IFormFile CoverImageFile { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Publisher")]
        public long PublisherId { get; set; }

        [Required]
        [Display(Name = "Author")]
        public long AuthorId { get; set; }

        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Display(Name = "Narrator")]
        public long? NarratorId { get; set; }

        [Display(Name = "Audio")]
        public long? AudioId { get; set; }
    }
}
