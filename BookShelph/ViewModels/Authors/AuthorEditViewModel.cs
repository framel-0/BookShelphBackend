using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookShelph.ViewModels.Authors
{
    public class AuthorEditViewModel : EditViewModelBase
    {
        public AuthorEditViewModel() : base()
        {

        }
        public long Id { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Other Name cannot be longer than 50 characters.")]
        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public short GenderId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string HouseAddress { get; set; }
    }
}
