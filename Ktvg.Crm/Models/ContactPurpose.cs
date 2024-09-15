using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ktvg.Crm.Models
{
    // Mục đích tiếp xúc
    [Table("ContactPurposes", Schema = "dbo")]
    public class ContactPurpose : BaseEntity
    {
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public bool? IsDeleted { get; set; }
    }
}
