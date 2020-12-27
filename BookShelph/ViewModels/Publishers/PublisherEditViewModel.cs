using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Publishers
{
    public class PublisherEditViewModel : EditViewModelBase
    {
        public PublisherEditViewModel() : base() { }
        public long Id { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
