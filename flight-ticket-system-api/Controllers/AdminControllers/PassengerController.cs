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
    public class PassengerController(Ace52024Context context) : ControllerBase
    {
        private readonly Ace52024Context _context = context;

        // GET: api/Passenger
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassengersJay>>> GetPassengersJays()
        {
            return await _context.PassengersJays.ToListAsync();
        }

        // GET: api/Passenger/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PassengersJay>> GetPassengersJay(int id)
        {
            var passengersJay = await _context.PassengersJays.FindAsync(id);

            if (passengersJay == null)
            {
                return NotFound();
            }

            return passengersJay;
        }

        // PUT: api/Passenger/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassengersJay(int id, PassengersJay passengersJay)
        {
            if (id != passengersJay.PassengerId)
            {
                return BadRequest();
            }

            _context.Entry(passengersJay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengersJayExists(id))
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

        // POST: api/Passenger
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PassengersJay>> PostPassengersJay(PassengersJay passengersJay)
        {
            _context.PassengersJays.Add(passengersJay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassengersJay", new { id = passengersJay.PassengerId }, passengersJay);
        }

        // DELETE: api/Passenger/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassengersJay(int id)
        {
            var passengersJay = await _context.PassengersJays.FindAsync(id);
            if (passengersJay == null)
            {
                return NotFound();
            }

            _context.PassengersJays.Remove(passengersJay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengersJayExists(int id)
        {
            return _context.PassengersJays.Any(e => e.PassengerId == id);
        }
    }
}
