using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ktvg.Crm.Models
{
    // Lịch sử tiếp xúc
    [Table("ContactHistories", Schema = "dbo")]
    public class ContactHistory : BaseEntity
    {
        [ForeignKey("ContactProject")]
        public int? ContactProjectId { get; set; }
        public ContactProject? ContactProject { get; set; }

        [ForeignKey("ContactPurpose")]
        public int? ContactPurposeId { get; set; }
        public ContactPurpose? ContactPurpose { get; set; }

        public string? Reason { get; set; }
        public DateTime StartDate { get; set; } // Bắt đầu
        public DateTime? RescheduleDate { get; set; } // Lịch hẹn
        public ContactStatus? Status { get; set; }

        public enum ContactStatus
        {
            Start = 1,
            Complete
        }
    }
}
