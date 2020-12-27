using BookShelph.Dtos.Gender;

namespace BookShelph.Dtos.Authors
{
    public class AuthorDto
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public short GenderId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public virtual GenderDto Gender { get; set; }
    }
}
