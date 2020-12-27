using System;

namespace BookShelph.ViewModels
{
    public class EditViewModelBase
    {
        public EditViewModelBase()
        {

        }
        public EditViewModelBase(int modifiedBy)
        {
            ModifiedBy = modifiedBy;
        }

        private static DateTime _dateTimeNow = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; } = _dateTimeNow;
        public bool IsActive { get; set; }
    }
}
