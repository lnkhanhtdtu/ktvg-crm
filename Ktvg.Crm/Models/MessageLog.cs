using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ktvg.Crm.Models
{
    [Table("MessageLogs", Schema = "dbo")]
    public class MessageLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID tự động tăng, khóa chính

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public MessageType Type { get; set; } // Loại tin nhắn (Facebook Messenger hoặc SMS)
        public string Recipient { get; set; } // Người nhận tin nhắn
        public string Content { get; set; } // Nội dung tin nhắn
        public DateTime SentTime { get; set; } // Thời gian gửi tin nhắn
        public string RequestPayload { get; set; } // Payload của request gửi tin nhắn
        public string ResponsePayload { get; set; } // Payload của response từ dịch vụ gửi tin nhắn
        public bool IsSuccessful { get; set; } // Trạng thái thành công hay thất bại
        public string ErrorMessage { get; set; } // Thông báo lỗi nếu có
    }

    public enum MessageType
    {
        Zalo = 1,
        Sms = 2
    }
}
