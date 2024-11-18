using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UtopiaTours.API;
using UtopiaTours.API.DTOs;
using UtopiaTours.Domain;

namespace UtopiaTours.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly UtopiaToursContext _context;

        private readonly IMapper _mapper;

        public BookingsController(UtopiaToursContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
           // return await _context.Bookings.Include(b=>b.Schedule).Include(p=>p.Passenger).ToListAsync();
            return await _context.Bookings
                .Include(b => b.Schedule)
                .ThenInclude(d => d.Destination)
                .Include(b => b.Schedule)
                .ThenInclude(f => f.Fleet)
                .Include(p => p.Passenger)
                .ToListAsync();
        }

        // GET: api/Bookings
        [HttpGet("BookingDTOs")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> BookingDTOs()
        {
            // return await _context.Bookings.Include(b=>b.Schedule).Include(p=>p.Passenger).ToListAsync();
            var bookings =  await _context.Bookings
                .Include(b => b.Schedule)
                .ThenInclude(d => d.Destination)
                .Include(b => b.Schedule)
                .ThenInclude(f => f.Fleet)

                .Include(p => p.Passenger)
                .ToListAsync();

            return _mapper.Map<List<BookingDTO>>(bookings);
        }


        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
