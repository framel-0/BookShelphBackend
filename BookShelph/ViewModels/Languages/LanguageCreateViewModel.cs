using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Languages
{
    public class LanguageCreateViewModel : CreateViewModelBase
    {
        public LanguageCreateViewModel() : base() { }
        [Required]
        public IFormFile ImageFile { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
