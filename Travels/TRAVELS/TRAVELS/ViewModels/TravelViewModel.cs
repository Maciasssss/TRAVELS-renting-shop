using FluentValidation;

namespace TRAVELS.ViewModels
{
    public class TravelViewModel
    {
        public int TravelId { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        private string _description;
        public string Description
        {
            get => _description;
            set => _description = value ?? "Default description"; 
        }
        public string GuideName { get; set; }
    }
    public class TravelViewModelValidator : AbstractValidator<TravelViewModel>
    {
        public TravelViewModelValidator()
        {
            RuleFor(travel => travel.Destination)
                .NotEmpty().WithMessage("Destination is required.");

            RuleFor(travel => travel.DepartureDate)
                .GreaterThan(DateTime.Now).WithMessage("Departure date must be in the future.");

            RuleFor(travel => travel.ReturnDate)
                .GreaterThan(travel => travel.DepartureDate)
                .WithMessage("Return date must be after the departure date.");

            RuleFor(travel => travel.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(travel => travel.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must be less than 1000 characters.");

            RuleFor(travel => travel.GuideName)
                .NotEmpty().WithMessage("Guide name is required.");

        }
    }
}