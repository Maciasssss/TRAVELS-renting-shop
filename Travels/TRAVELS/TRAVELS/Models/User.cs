using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TRAVELS.Models
{
    public class User: IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        
        public int UserPhone { get; set; }
        public ICollection<Travel> UserTravels { get; set; } = new List<Travel>();
    }
}
