using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstapi.Models;
using firstapi.Repositories;

namespace firstapi.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AirportController(IRepository repository) : ControllerBase
    {
        IRepository _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<AirportsJay>> GetAirports()
        {
            return await _repository.GetAllAirports();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AirportsJay>> GetAirport(string id)
        {
            AirportsJay? airport = await _repository.GetAirport(id);

            if (airport == null)
            {
                return NotFound();
            }

            return airport;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAirport(string id, AirportsJay airportsJay)
        {
            if (id != airportsJay.AirportCode)
            {
                return BadRequest();
            }

            try
            {
                await _repository.EditEntity<AirportsJay>(airportsJay);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.AirportExists(id))
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

        [HttpPost]
        public async Task<ActionResult<AirportsJay>> AddAirport(AirportsJay airportsJay)
        {
            try
            {
                await _repository.AddEntity<AirportsJay>(airportsJay);
            }
            catch (DbUpdateException)
            {
                if (_repository.AirportExists(airportsJay.AirportCode))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(string id)
        {
            AirportsJay? airport = await _repository.GetAirport(id);
            if (airport == null)
            {
                return NotFound();
            }

            await _repository.DeleteEntity(airport);

            return NoContent();
        }
    }
}
