using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Publishers
{
    public class PublisherCreateViewModel : CreateViewModelBase
    {
        public PublisherCreateViewModel() : base() { }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
