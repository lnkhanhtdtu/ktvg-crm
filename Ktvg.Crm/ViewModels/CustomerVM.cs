using Ktvg.Crm.Models;
using System.ComponentModel.DataAnnotations;

namespace Ktvg.Crm.ViewModels
{
    public class CustomerVM
    {
        public int Id { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Ngày đăng ký")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string? ProductName { get; set; }

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

        [Display(Name = "Số tiền thanh toán")]
        public decimal? PaymentAmount { get; set; }

        [Display(Name = "Thiết bị đang lắp")]
        public string? DeviceInstalled { get; set; }

        [Display(Name = "Lắp đặt GPS hay Cam Nghị định 10")]
        public string? InstallationType { get; set; }

        public LocateType? LocateType { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Remark { get; set; }

        public bool SendZaloConfirmation { get; set; }
    }
}
