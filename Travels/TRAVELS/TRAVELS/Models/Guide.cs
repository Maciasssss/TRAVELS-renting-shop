using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TRAVELS.Models
{
    public class Guide
    {
        [Key]
        public int GuideId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Travel> Travels { get; set; }
    }
}
