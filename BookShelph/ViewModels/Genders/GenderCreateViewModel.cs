using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.ViewModels.Genders
{
    public class GenderCreateViewModel : CreateViewModelBase
    {
        public GenderCreateViewModel() : base() { }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
