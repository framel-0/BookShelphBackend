﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Categories
{
    public class CategoryCreateViewModel : CreateViewModelBase
    {
        public CategoryCreateViewModel() : base() { }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
