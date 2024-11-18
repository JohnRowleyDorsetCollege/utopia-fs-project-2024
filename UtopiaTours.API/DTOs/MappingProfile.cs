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
        public string Destination { get; set; }
        public DateTime ScheduleDt { get; set; }
        public string RegistrationId { get; set; }
    }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Booking, BookingDTO>()
                    .ForMember(dest=>dest.FirstName, opt=>opt.MapFrom(src=>src.Passenger.FirstName))
                    .ForMember(dest=>dest.LastName, opt=>opt.MapFrom(src=>src.Passenger.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Passenger.Email))
                    .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Id ))
                    .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Schedule.Destination.title ))
                    .ForMember(dest => dest.RegistrationId, opt => opt.MapFrom(src => src.Schedule.Fleet.Registration ))
                    .ForMember(dest => dest.ScheduleDt, opt => opt.MapFrom(src => src.Schedule.ScheduleDt ))

                    ;
            }
        }
    }

