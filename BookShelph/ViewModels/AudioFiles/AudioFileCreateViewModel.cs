using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.AudioFiles
{
    public class AudioFileCreateViewModel : CreateViewModelBase
    {
        public AudioFileCreateViewModel() : base() { }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile AudioFile { get; set; }

        public string Description { get; set; }
    }
}
