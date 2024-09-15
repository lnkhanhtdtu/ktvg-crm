using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ktvg.Crm.Models
{
    [Table("LoginHistories", Schema = "dbo")]
    public class LoginHistory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // ID tự động tăng, khóa chính trong cơ sở dữ liệu
        public string UserName { get; set; } // Tên người dùng
        public DateTime LoginTime { get; set; } // Thời gian đăng nhập
        public string IpAddress { get; set; } // Địa chỉ IP của người dùng
        public string Device { get; set; } // Thông tin về thiết bị (nếu cần)
    }
}
