using AutoMapper;
using UtopiaTours.Domain;

namespace UtopiaTours.API.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Booking, BookingDTO>()
                    .ForMember(dest=>dest.FirstName, opt=>opt.MapFrom(src=>src.Passenger.FirstName))
                    .ForMember(dest=>dest.LastName, opt=>opt.MapFrom(src=>src.Passenger.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Passenger.Email))
                    .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Id ))

                    ;
            }
        }
    }
}
