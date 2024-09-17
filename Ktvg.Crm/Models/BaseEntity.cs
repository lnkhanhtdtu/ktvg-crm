using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ktvg.Crm.Models
{
    public abstract class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool? IsDeleted { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Remark { get; set; }
        public string? Result { get; set; }

        [ForeignKey(nameof(CreatedByEmployee))]
        public int? CreatedById { get; set; }

        [ForeignKey(nameof(ModifiedByEmployee))]
        public int? ModifiedById { get; set; }

        [ForeignKey(nameof(DeletedByEmployee))]
        public int? DeletedById { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public Employee? DeletedByEmployee { get; set; }

        protected BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
