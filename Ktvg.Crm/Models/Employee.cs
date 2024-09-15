using System.ComponentModel.DataAnnotations.Schema;

namespace Ktvg.Crm.Models
{
    [Table("Employees", Schema = "dbo")]
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? Sex { get; set; }
        public string? PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
