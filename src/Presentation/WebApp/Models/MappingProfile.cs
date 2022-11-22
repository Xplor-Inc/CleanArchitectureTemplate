namespace GenogramSystem.WebApp.Models;
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
    }
}
