using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Categories
{
    public class CategoryEditViewModel : EditViewModelBase
    {
        public CategoryEditViewModel() : base() { }
        public int Id { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
