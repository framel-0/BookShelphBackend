﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Books
{
    public class BookEditViewModel : EditViewModelBase
    {

        public BookEditViewModel() : base() { }

        public long Id { get; set; }
        public string ExistingCoverImage { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Cover Image")]
        public IFormFile CoverImageFile { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(450, MinimumLength = 50, ErrorMessage = "Summary cannot be less than 50 or longer than 450  characters.")]
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
