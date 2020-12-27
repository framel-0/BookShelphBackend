using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.ViewModels.Genders
{
    public class GenderEditViewModel : EditViewModelBase
    {
        public GenderEditViewModel() : base() { }
        public short Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
