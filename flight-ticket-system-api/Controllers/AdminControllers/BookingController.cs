using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstapi.Models;

namespace firstapi.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public BookingController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingsJay>>> GetBookingsJays()
        {
            return await _context.BookingsJays.ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingsJay>> GetBookingsJay(int id)
        {
            var bookingsJay = await _context.BookingsJays.FindAsync(id);

            if (bookingsJay == null)
            {
                return NotFound();
            }

            return bookingsJay;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingsJay(int id, BookingsJay bookingsJay)
        {
            if (id != bookingsJay.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(bookingsJay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingsJayExists(id))
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

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingsJay>> PostBookingsJay(BookingsJay bookingsJay)
        {
            _context.BookingsJays.Add(bookingsJay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingsJay", new { id = bookingsJay.BookingId }, bookingsJay);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingsJay(int id)
        {
            var bookingsJay = await _context.BookingsJays.FindAsync(id);
            if (bookingsJay == null)
            {
                return NotFound();
            }

            _context.BookingsJays.Remove(bookingsJay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingsJayExists(int id)
        {
            return _context.BookingsJays.Any(e => e.BookingId == id);
        }
    }
}
