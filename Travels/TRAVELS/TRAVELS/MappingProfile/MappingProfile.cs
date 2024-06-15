using AutoMapper;
using TRAVELS.Iservices;
using TRAVELS.Models;
using TRAVELS.ViewModels;

public class ApplicationMappingProfile : Profile
{
   
    public ApplicationMappingProfile()
    {

        CreateMap<Guide, GuideViewModel>().ReverseMap();
        CreateMap<ReservationViewModel, Reservation>()
             .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User { UserName = src.Username }))
             .ReverseMap();
        CreateMap<Travel, TravelViewModel>()
     .ForMember(dest => dest.Description, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Description) ? "Default description" : src.Description))
     .ReverseMap();

        CreateMap<User, UserViewModel>().ReverseMap();
    }

  


}
