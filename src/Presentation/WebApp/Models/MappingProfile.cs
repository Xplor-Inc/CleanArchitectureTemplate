namespace CleanArchitectureTemplate.WebApp.Models;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Enquiry,              EnquiryDto>()
            .ReverseMap();

        CreateMap<User,                 UserDto>()
            .ReverseMap();


        CreateMap<Counter,              CounterDto>()
            .ReverseMap();

        CreateMap<Notification,         NotificationDto>()
            .ReverseMap();

    }
}
