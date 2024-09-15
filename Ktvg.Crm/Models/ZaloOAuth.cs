using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ktvg.Crm.Models
{
    [Table("ZaloOAuths", Schema = "dbo")]
    public class ZaloOAuth
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ExpireIn { get; set; }

        public DateTime ExpireAt()
        {
            return DateTime.Now.AddSeconds(Int32.Parse(ExpireIn));
        }

        public bool IsExpire()
        {
            return ExpireAt() < DateTime.Now;
        }
    }
}
