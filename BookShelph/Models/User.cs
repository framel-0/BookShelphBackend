using System;
using System.Collections.Generic;

#nullable disable

namespace BookShelph.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public short GenderId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirebaseToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
