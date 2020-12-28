using BookShelph.Dtos.Gender;

namespace BookShelph.Dtos.Narrators
{
    public class NarratorDto
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }

        public virtual GenderDto Gender { get; set; }
    }
}
