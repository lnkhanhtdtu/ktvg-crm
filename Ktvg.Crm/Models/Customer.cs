using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ktvg.Crm.Models
{
    [Table("Customers", Schema = "dbo")]
    public class Customer : BaseEntity
    {
        [Display(Name = "Ngày đăng ký")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Loại xe")]
        public string? VehicleType { get; set; }

        [Display(Name = "Số khung/Bảng số xe")]
        public string? VehicleNumber { get; set; }

        [Display(Name = "Nguồn KH")]
        public string? CustomerSource { get; set; }

        [Display(Name = "Mã KH")]
        public string? CustomerCode { get; set; }

        [Display(Name = "Tên KH")]
        public string CustomerName { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "SĐT")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hoạt động")]
        public bool? IsActive { get; set; } = true;

        [Display(Name = "Số tiền thanh toán")]
        public decimal? PaymentAmount { get; set; }

        public bool? HasZalo { get; set; }

        [Display(Name = "Thiết bị đang lắp")]
        public string? DeviceInstalled { get; set; }

        [Display(Name = "Lắp đặt GPS hay Cam Nghị định 10")]
        public string? InstallationType { get; set; }

        public LocateType? LocateType { get; set; }

        public bool? IsSendZalo { get; set; }

        public bool? IsSendSms { get; set; }
    }

    public enum LocateType
    {
        NewRegistration = 1, // Đăng ký mới
        Renewal = 2, // Gia hạn
        RenewalReminder = 3 // Nhắc nhở gia hạn
    }
}
