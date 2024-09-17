using static Ktvg.Crm.Models.ContactHistory;

namespace Ktvg.Crm.ViewModels
{
    public class ContactHistoryVM
    {
        public int ContactProjectId { get; set; }
        public string? ContactProjectName { get; set; }

        public int ContactPurposeId { get; set; }

        public int CustomerId { get; set; }
        public int Customer { get; set; }

        public string? Reason { get; set; }
        public DateTime StartDate { get; set; } // Bắt đầu
        public DateTime? RescheduleDate { get; set; } // Lịch hẹn
        public ContactStatus? Status { get; set; }

        public string? Result { get; set; }
        public string? Remark { get; set; }
    }
}
