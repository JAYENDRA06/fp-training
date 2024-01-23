using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstapi.Models;

namespace firstapi.Controllers_UserControllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public AirportController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Airport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportsJay>>> GetAirportsJays()
        {
            return await _context.AirportsJays.ToListAsync();
        }

        // GET: api/Airport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportsJay>> GetAirportsJay(string id)
        {
            var airportsJay = await _context.AirportsJays.FindAsync(id);

            if (airportsJay == null)
            {
                return NotFound();
            }

            return airportsJay;
        }

        // PUT: api/Airport/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirportsJay(string id, AirportsJay airportsJay)
        {
            if (id != airportsJay.AirportCode)
            {
                return BadRequest();
            }

            _context.Entry(airportsJay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportsJayExists(id))
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

        // POST: api/Airport
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirportsJay>> PostAirportsJay(AirportsJay airportsJay)
        {
            _context.AirportsJays.Add(airportsJay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AirportsJayExists(airportsJay.AirportCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAirportsJay", new { id = airportsJay.AirportCode }, airportsJay);
        }

        // DELETE: api/Airport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirportsJay(string id)
        {
            var airportsJay = await _context.AirportsJays.FindAsync(id);
            if (airportsJay == null)
            {
                return NotFound();
            }

            _context.AirportsJays.Remove(airportsJay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirportsJayExists(string id)
        {
            return _context.AirportsJays.Any(e => e.AirportCode == id);
        }
    }
}
