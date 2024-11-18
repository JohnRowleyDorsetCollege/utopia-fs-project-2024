using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int ScheduleId { get; set; }
        public DateTime BookingDt { get; set; }
    }
}
