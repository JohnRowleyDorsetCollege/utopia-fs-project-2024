using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtopiaTours.Domain
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; } = new Passenger();

       
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public DateTime BookingDt { get; set; }
    }

    public class Passenger
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        //   public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

        public int FleetId { get; set; }
        public Fleet Fleet { get; set; }

        public DateTime ScheduleDt { get; set; }

       
        //   public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public class Destination
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
    }

    public class Fleet
    {
        [Key]
        public int Id { get; set; }
        public string Registration { get; set; }

        public int CoachTypeId { get; set; }    
        public CoachType CoachType { get; set; }
    }

    public class CoachType
    {
        [Key]
        public int Id { get; set; }
        public int Capacity { get; set; }
    }
}
