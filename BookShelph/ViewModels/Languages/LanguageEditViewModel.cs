using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Languages
{
    public class LanguageEditViewModel : EditViewModelBase
    {
        public LanguageEditViewModel() : base() { }
        public int Id { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
