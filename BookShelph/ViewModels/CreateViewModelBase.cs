using System;

namespace BookShelph.ViewModels
{
    public class CreateViewModelBase
    {

        public CreateViewModelBase()
        {

        }
        public CreateViewModelBase(int createdBy, int modifiedBy)
        {
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
        }

        private static DateTime _dateTimeNow = DateTime.Now;
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; } = _dateTimeNow;
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; } = _dateTimeNow;
        public bool IsActive { get; } = true;
    }
}
