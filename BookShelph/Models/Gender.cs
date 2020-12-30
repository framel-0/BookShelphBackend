using System;
using System.Collections.Generic;

#nullable disable

namespace BookShelph.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Authors = new HashSet<Author>();
            Narrators = new HashSet<Narrator>();
            Users = new HashSet<User>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Narrator> Narrators { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
