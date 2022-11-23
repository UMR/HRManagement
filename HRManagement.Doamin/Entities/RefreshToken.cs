using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Domain.Entities
{
    public class RefreshToken
    {  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Token { get; set; } 

        public string JwtId { get; set; } 

        public DateTime CreatedDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
