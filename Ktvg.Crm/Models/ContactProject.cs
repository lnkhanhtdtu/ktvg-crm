using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ktvg.Crm.Models
{
    // Dự án tiếp xúc
    [Table("ContactProjects", Schema = "dbo")]
    public class ContactProject : BaseEntity
    {
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Icon { get; set; }
        public string? Remark { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
