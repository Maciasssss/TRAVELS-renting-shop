using FluentValidation;

namespace TRAVELS.ViewModels
{
    public class GuideViewModel
    {
        public int GuideId { get; set; }
        public string Name { get; set; }
        public List<string> TravelDestinations { get; set; }
    }
    public class GuideViewModelValidator : AbstractValidator<GuideViewModel>
    {
        public GuideViewModelValidator()
        {
            RuleFor(guide => guide.Name)
                .NotEmpty().WithMessage("Guide's name is required.")
                .Length(2, 100).WithMessage("Guide's name must be between 2 and 100 characters.");

            RuleFor(guide => guide.TravelDestinations)
                .NotNull().WithMessage("Travel destinations cannot be null.")
                .Must(destinations => destinations.Count > 0).WithMessage("At least one travel destination is required.")
                .ForEach(destination => destination
                    .NotEmpty().WithMessage("Each travel destination must be a non-empty string."));
        }
    }
}