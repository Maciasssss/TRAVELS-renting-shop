using System.ComponentModel.DataAnnotations;

namespace TRAVELS.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }


        public Travel Travel { get; set; }

        public User User { get; set; }


        public DateTime ReservationDate { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

        [StringLength(500)]
        public string AdditionalInfo { get; set; }
    }
}
