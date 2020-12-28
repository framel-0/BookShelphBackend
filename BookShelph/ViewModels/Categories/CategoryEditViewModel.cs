using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Categories
{
    public class CategoryEditViewModel : EditViewModelBase
    {
        public CategoryEditViewModel() : base() { }
        public int Id { get; set; }
        public string ExistingImage { get; set; }

        public IFormFile ImageFile { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
