using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

        private IMemoryCache _cache;
        public BookingsController(UtopiaToursContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {

            if (!_cache.TryGetValue("BookingData", out IEnumerable<Booking> bookingData))
            {

                var rng = new Random();
                bookingData =  await _context.Bookings
                    .Include(b => b.Schedule)
                    .ThenInclude(d => d.Destination)
                    .Include(b => b.Schedule)
                    .ThenInclude(f => f.Fleet)
                    .Include(p => p.Passenger)
                    .ToListAsync();


            }

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            };

            Console.WriteLine(bookingData);

            _cache.Set("BookingData", bookingData, cacheOptions);


            return Ok(bookingData);

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
        public async Task<ActionResult<BookingDTO>> GetBooking(int id)
        {
            string bookingCacheKey = $"BookingRecord-{id}";
           
            if (!_cache.TryGetValue(bookingCacheKey, out Booking booking))
            {
                booking = await _context.Bookings.Include(b => b.Schedule)
                .ThenInclude(d => d.Destination)
                .Include(b => b.Schedule)
                .ThenInclude(f => f.Fleet)
                .Include(p => p.Passenger).Where(x=>x.Id==id).FirstOrDefaultAsync();
               
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };

                _cache.Set("bookingKey", bookingCacheKey, cacheOptions);
            }
                

            if (booking == null)
            {
                return NotFound();
            }

            return _mapper.Map<BookingDTO>(booking);


            

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
