using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TRAVELS.Models
{
    public class Travel
    {
        [Key]
        public int TravelId { get; set; }

        [Required]
        [StringLength(200)]
        public string Destination { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public int GuideId { get; set; }
        public Guide Guide { get; set; }

        public ICollection<User> User { get; set; } = new List<User>();

    }
}
