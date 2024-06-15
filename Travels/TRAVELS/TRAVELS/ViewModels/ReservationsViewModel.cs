using FluentValidation;

namespace TRAVELS.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }
      
        public string TravelDestination { get; set; }
        public string Username { get; set; }
        public string GuideName { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string AdditionalInfo { get; set; }
    }

    public class ReservationViewModelValidator : AbstractValidator<ReservationViewModel>
    {
        public ReservationViewModelValidator()
        {

            RuleFor(reservation => reservation.ReservationDate)
                .GreaterThan(DateTime.Now).WithMessage("Reservation date must be in the future.");

            RuleFor(reservation => reservation.NumberOfPeople)
                .GreaterThan(0).WithMessage("Number of people must be greater than 0.");

            RuleFor(reservation => reservation.AdditionalInfo)
                .MaximumLength(500).WithMessage("Additional information must be less than 500 characters.");

        }
    }
}